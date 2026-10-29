<p align="center"><img src="../../deckpilotlogo.png" alt="DeckPilot" width="96"></p>

<h1 align="center">DeckPilot</h1>

<p align="center">تطبيق Windows للوحة اختصارات من 12 مفتاحًا.</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest"><img src="https://img.shields.io/github/v/release/lolo34dr/deckpilot?style=flat-square&amp;color=78b7a0" alt="Latest release"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe"><img src="https://img.shields.io/badge/Download-Windows_x64-0078D4?style=flat-square&amp;logo=windows" alt="Download for Windows x64"></a>
  <a href="https://github.com/lolo34dr/deckpilot/blob/main/LICENSE"><img src="https://img.shields.io/github/license/lolo34dr/deckpilot?style=flat-square" alt="MIT license"></a>
  <a href="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml"><img src="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml/badge.svg?branch=main" alt="Windows build status"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases"><img src="https://img.shields.io/github/downloads/lolo34dr/deckpilot/total?style=flat-square&amp;color=70a7e8" alt="GitHub release downloads"></a>
</p>

<p align="center">
  <a href="../../README.md">🇬🇧 English</a> ·
  <a href="README.fr.md">🇫🇷 Français</a> ·
  <a href="README.de.md">🇩🇪 Deutsch</a> ·
  <a href="README.es.md">🇪🇸 Español</a> ·
  <a href="README.it.md">🇮🇹 Italiano</a> ·
  <a href="README.pt-BR.md">🇧🇷 Português brasileiro</a> ·
  <a href="README.nl.md">🇳🇱 Nederlands</a> ·
  <a href="README.pl.md">🇵🇱 Polski</a> ·
  <a href="README.ru.md">🇷🇺 Русский</a> ·
  <a href="README.ja.md">🇯🇵 日本語</a> ·
  <a href="README.ko.md">🇰🇷 한국어</a> ·
  <a href="README.zh-CN.md">🇨🇳 简体中文</a> ·
  <a href="README.ar.md">🇸🇦 العربية</a>
</p>

<!-- Arabic prose; Markdown is kept outside HTML blocks for GitHub rendering. -->

يضبط DeckPilot مفاتيح F13–F24 في لوحات الاختصارات المتوافقة مع Arduino Leonardo، بترتيب 3 صفوف و4 أعمدة. تحدد المعايرة الموجّهة مواقع المفاتيح حتى عند اختلاف ترتيب الأسلاك.

## الميزات

- تشغيل التطبيقات المثبتة وفتح المجلدات والمواقع وإرسال اختصارات لوحة المفاتيح.
- التحكم في Spotify بشكل مستقل عن وسائط Windows، وكتم الميكروفونات وتبديل مخرج الصوت.
- إنشاء ملفات إعداد واستخدام قوالب للموسيقى والإنتاجية والإبداع والتبديل بينها بمفتاح.
- تعيين الضغط القصير والطويل وألوان المفاتيح وتسجيل اختصارات لوحة المفاتيح.
- السحب والإفلات والنسخ والقص واللصق والتراجع والإعادة والاستيراد والتصدير والنسخ الاحتياطي التلقائي.

## التثبيت

يتطلب Windows 10 الإصدار 2004 أو أحدث، أو Windows 11، بمعمارية x64 وكابل USB يدعم نقل البيانات. بيئة تشغيل .NET مضمّنة.

1. نزّل DeckPilot-Setup.exe من الرابط أعلاه وثبّته. يطلب Windows صلاحية المسؤول للتثبيت في Program Files. تُقترح افتراضيًا اختصارات سطح المكتب وقائمة ابدأ والتشغيل مع Windows.
2. صل اللوحة وعاير المفاتيح من اليسار إلى اليمين، أربعة مفاتيح في كل صف، لثلاثة صفوف. يجب أن يرسل كل مفتاح قيمة مختلفة من F13–F24.

## الاستخدام

اختر مفتاحًا ثم إجراءً من المكتبة واضبط الخيارات واحفظ. يمكنك التعديل والاختبار حتى دون توصيل اللوحة. تحتفظ المفاتيح الحرة بوظيفتها F13–F24 للألعاب والتطبيقات الأخرى.

يبقى DeckPilot قيد التشغيل في منطقة الإعلام عند إغلاق النافذة. استخدم قائمة الأيقونة للخروج الكامل. يتطلب Spotify تطبيق Windows مفتوحًا مع جلسة تشغيل؛ لا ينتقل التحكم إلى YouTube بدلًا منه.

## التحديثات والبرنامج الثابت

تُفحص التحديثات عند بدء التشغيل وكل ست ساعات. بعد موافقتك تُنزّل وتُتحقق سلامتها. قد يطلب المثبّت إذن Windows. تُحفظ ملفات الإعداد والمعايرة.

لا تحتاج إلى تعريف إضافي أو إعادة برمجة إذا كانت اللوحة ترسل F13–F24 بالفعل. يضيف البرنامج الثابت المتوافق لـDeckPilot التعرف على الجهاز والتشخيص؛ اقرأ دليل الأجهزة قبل البرمجة.

## التوثيق والترخيص

هذه الصفحة ترجمة لدليل المستخدم. واجهة التطبيق حاليًا بالفرنسية؛ المرجع التقني المفصل وسجل الإصدارات بالإنجليزية.

[English technical reference](../../README.md#development) · [Firmware & hardware](../../firmware/README.md) · [Release notes](https://github.com/lolo34dr/deckpilot/releases)

ترخيص MIT يسمح بالاستخدام والنسخ والتعديل وإعادة التوزيع مع الاحتفاظ بإشعار حقوق النشر ونص الترخيص.

[MIT LICENSE](../../LICENSE)
