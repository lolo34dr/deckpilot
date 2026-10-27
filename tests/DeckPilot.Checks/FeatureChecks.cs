using System.Text.Json;
using System.Text.Json.Nodes;
using DeckPilot.Core;

public static class FeatureChecks
{
    public static int Run()
    {
        int passed = 0;
        void Check(string name, Action test) { test(); passed++; Console.WriteLine("PASS " + name); }
        void Expect(bool value) { if (!value) throw new Exception("Workspace feature assertion failed"); }
        void Reject(Action action)
        {
            try { action(); }
            catch (Exception ex) when (ex is InvalidDataException or InvalidOperationException) { return; }
            throw new Exception("Invalid workspace feature data was accepted");
        }

        Check("Existing settings retain action numbers, default accents and animation preferences", () =>
        {
            var settings = JsonSerializer.Deserialize<Settings>("{\"LayoutColumns\":4}")!;
            settings.Validate();
            Expect(!settings.ReduceMotion && settings.ActiveProfile.Buttons.All(action => action.Accent == ""));
            Expect((int)ActionKind.AudioOutput == 8 && (int)ActionKind.Profile == 9 && (int)ActionKind.System == 10);
            foreach (ActionKind kind in Enum.GetValues<ActionKind>()) Expect(!string.IsNullOrWhiteSpace(ActionSearch.KindNames[(int)kind]));
            var olderPackage = JsonNode.Parse(ProfileExchange.Export(settings.Profiles))!;
            foreach (var action in olderPackage["Profiles"]![0]!["Buttons"]!.AsArray()) action!.AsObject().Remove("Accent");
            Expect(ProfileExchange.Import(olderPackage.ToJsonString()).Profiles[0].Buttons.All(action => action.Accent == ""));
            settings.ReduceMotion = true; settings.ActiveProfile.Buttons[0].Accent = "#70A7E8";
            var persisted = JsonSerializer.Deserialize<Settings>(JsonSerializer.Serialize(settings))!;
            persisted.Validate();
            Expect(persisted.ReduceMotion && persisted.ActiveProfile.Buttons[0].Accent == "#70A7E8");
        });

        Check("Accents validate at import, include long presses and survive detached copies", () =>
        {
            var settings = new Settings();
            var action = new ButtonAction
            {
                Kind = ActionKind.Media, Key = 0xB3, Accent = "#70a7e8", LongPressEnabled = true,
                LongPressAction = new() { Kind = ActionKind.Microphone, Accent = "#DF8898" }
            };
            settings.ActiveProfile.Buttons[0] = action;
            settings.Validate();
            var copy = action.Copy();
            Expect(copy == action && !ReferenceEquals(copy.LongPressAction, action.LongPressAction));
            copy.Accent = "#AD91E8"; Expect(copy != action);
            var roundTrip = ProfileExchange.Import(ProfileExchange.Export(settings.Profiles)).Profiles[0].Buttons[0];
            Expect(roundTrip == action);
            foreach (string? invalid in new[] { "#000000", "red", "#70A7E800", "url(file)", null })
            {
                action.LongPressAction!.Accent = invalid!;
                Reject(settings.Validate);
                Reject(() => ProfileExchange.Export(settings.Profiles));
            }
        });

        Check("Profile navigation wraps, resolves stable identifiers and never changes the active profile", () =>
        {
            var settings = new Settings();
            var first = settings.ActiveProfile;
            var second = new Profile { Name = "Second" };
            var third = new Profile { Name = "Third" };
            settings.Profiles.AddRange([second, third]);
            // An absent active ID follows the same fallback as Settings.ActiveProfile.
            Expect(ProfileNavigation.Resolve(settings, "next") == second);
            Expect(ProfileNavigation.Resolve(settings, "previous") == third);
            Expect(settings.ActiveProfile == first && settings.ActiveProfileId == Guid.Empty);
            settings.ActiveProfileId = third.Id;
            Expect(ProfileNavigation.Resolve(settings, "next") == first);
            Expect(ProfileNavigation.Resolve(settings, second.Id.ToString()) == second);
            settings.Profiles.Reverse();
            Expect(ProfileNavigation.Resolve(settings, second.Id.ToString()) == second);
            settings.Profiles = [first]; settings.ActiveProfileId = first.Id;
            Expect(ProfileNavigation.Resolve(settings, "next") == first && ProfileNavigation.Resolve(settings, "previous") == first);
        });

        Check("Deleted or malformed profile destinations fail visibly without damaging settings", () =>
        {
            var settings = new Settings();
            string deleted = Guid.NewGuid().ToString();
            settings.ActiveProfile.Buttons[0] = new() { Kind = ActionKind.Profile, Target = deleted };
            settings.Validate();
            Reject(() => ProfileNavigation.Resolve(settings, deleted));
            Reject(() => ProfileNavigation.Resolve(settings, ""));
            Reject(() => ProfileNavigation.Resolve(settings, "0"));
            Reject(() => ProfileNavigation.Resolve(settings, Guid.Empty.ToString()));
            Expect(ProfileExchange.Import(ProfileExchange.Export(settings.Profiles)).Profiles[0].Buttons[0].Target == deleted);
        });

        Check("Import remaps short and long profile links while preserving outside references", () =>
        {
            var settings = new Settings();
            var first = new Profile { Name = "Imported" };
            var second = new Profile { Name = "Linked" };
            first.Buttons[0] = new() { Kind = ActionKind.Profile, Target = second.Id.ToString(), LongPressEnabled = true,
                LongPressAction = new() { Kind = ActionKind.Profile, Target = first.Id.ToString() } };
            second.Buttons[0] = new() { Kind = ActionKind.Profile, Target = settings.ActiveProfile.Id.ToString() };
            second.Buttons[1] = new() { Kind = ActionKind.Profile, Target = "next" };
            var package = ProfileExchange.Import(ProfileExchange.Export([first, second]));
            var imported = ProfileExchange.AddAsNew(settings, package);
            Expect(imported[0].Id != first.Id && imported[1].Id != second.Id);
            Expect(imported[0].Buttons[0].Target == imported[1].Id.ToString());
            Expect(imported[0].Buttons[0].LongPressAction!.Target == imported[0].Id.ToString());
            Expect(imported[1].Buttons[0].Target == settings.Profiles[0].Id.ToString() && imported[1].Buttons[1].Target == "next");
            Expect(first.Buttons[0].Target == second.Id.ToString() && package.Profiles[0].Id == first.Id);
            Expect(ProfileNavigation.Resolve(settings, imported[0].Buttons[0].Target) == imported[1]);
        });

        Check("Duplicating a profile rewires self links while backups keep the original identifiers", () =>
        {
            var original = new Profile();
            original.Buttons[0] = new() { Kind = ActionKind.Profile, Target = original.Id.ToString() };
            var duplicate = ProfileExchange.CopyAsNew([original]).Single();
            Expect(duplicate.Id != original.Id && duplicate.Buttons[0].Target == duplicate.Id.ToString());
            Expect(original.Buttons[0].Target == original.Id.ToString());
            var backup = ProfileExchange.Copy([original]).Single();
            Expect(backup.Id == original.Id && backup.Buttons[0].Target == original.Id.ToString());
        });

        Check("Over-limit profile imports fail before changing the destination", () =>
        {
            var settings = new Settings { Profiles = Enumerable.Range(0, 100).Select(i => new Profile { Name = "Profile " + i }).ToList() };
            var package = new ProfilePackage { Profiles = [new()] };
            Reject(() => ProfileExchange.AddAsNew(settings, package));
            Expect(settings.Profiles.Count == 100);
        });

        Check("Windows commands are allowlisted and searchable by meaningful terms", () =>
        {
            Expect(SystemActions.All.Count == 7 && SystemActions.All.Select(action => action.Id).Distinct().Count() == 7);
            foreach (var action in SystemActions.All) Expect(SystemActions.Get(action.Id) == action);
            Reject(() => SystemActions.Get("ms-settings:arbitrary"));
            Reject(() => SystemActions.Get("powershell.exe"));
            var volume = ActionSearch.Find("volume", []);
            Expect(volume.Any(action => action.Action.Kind == ActionKind.System && action.Action.Target == "sound-settings"));
            Expect(ActionSearch.Find("profil suivant", []).Any(action => action.Action.Kind == ActionKind.Profile && action.Action.Target == "next"));
        });

        Check("Templates create independent portable profiles with exactly twelve valid actions", () =>
        {
            foreach (var template in ProfileTemplates.All)
            {
                var profile = ProfileTemplates.Create(template.Id);
                var second = ProfileTemplates.Create(template.Id);
                Expect(profile.Id != second.Id && profile.Buttons.Count == 12);
                Expect(profile.Buttons.All(action => action.IsValid()));
                Expect(profile.Buttons.All(action => action.Kind is not (ActionKind.Application or ActionKind.AudioOutput or ActionKind.Folder)));
                Expect(profile.Buttons.Where(action => action.Kind == ActionKind.System).All(action => SystemActions.Get(action.Target).Id == action.Target));
                foreach (var action in profile.Buttons.Where(action => action.Kind == ActionKind.Profile)) Expect(action.Target is "next" or "previous");
                string originalLabel = second.Buttons[0].Label;
                profile.Buttons[0].Label = "Changed";
                Expect(second.Buttons[0].Label == originalLabel);
                var roundTrip = ProfileExchange.Import(ProfileExchange.Export([profile])).Profiles[0];
                Expect(roundTrip.Id == profile.Id && roundTrip.Buttons.SequenceEqual(profile.Buttons));
            }
            Reject(() => ProfileTemplates.Create("unknown"));
        });

        Check("Accent undo and redo preserve the separate reduce-motion preference", () =>
        {
            var settings = new Settings();
            var history = new ProfileHistory(settings);
            settings.ActiveProfile.Buttons[0].Accent = "#AD91E8"; history.Record(settings);
            settings.ReduceMotion = true;
            Expect(history.Undo(settings) && settings.ActiveProfile.Buttons[0].Accent == "" && settings.ReduceMotion);
            Expect(history.Redo(settings) && settings.ActiveProfile.Buttons[0].Accent == "#AD91E8" && settings.ReduceMotion);
        });
        return passed;
    }
}
