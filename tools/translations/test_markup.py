import unittest
from translate import Engine, clean_prose

class FakeEngine(Engine):
    def __init__(self):
        pass
    def text(self, text):
        return clean_prose(text.replace('Open', '`Ouvrir`'), 'fr')

class MarkupTests(unittest.TestCase):
    def test_generated_delimiters_cannot_create_code(self):
        source = 'Open `file.exe` and `config.json`.\n'
        result = FakeEngine().markdown(source)
        self.assertIn('’Ouvrir’ `file.exe`', result)
    def test_code_inside_link_labels_is_not_translated(self):
        source = '[Open `Open.exe`](https://example.com/Open.exe)\n'
        result = FakeEngine().markdown(source)
        self.assertIn('`Open.exe`', result)
        self.assertIn('](https://example.com/Open.exe)', result)
    def test_fenced_commands_are_unchanged(self):
        source = 'Open application\n```sh\nOpen `file.exe`\n```\n'
        self.assertIn('```sh\nOpen `file.exe`\n```', FakeEngine().markdown(source))

if __name__ == '__main__':
    unittest.main()
