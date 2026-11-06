> 这是完整 README 的自动翻译。 英文原件是参考文献。 应用程序接口标签和命令被保留到准确位置 。

<p align="center">
  <img src="../../deckpilotbanner.png#gh-light-mode-only" alt="DeckPilot 横幅" width="760">
  <img src="../../deckpilotlogo.png#gh-dark-mode-only" alt="DeckPilot 标志" width="112">
</p>

<h1 align="center">DeckPilot</h1>

<p align="center">
  您的窗口同伴 12- 关键DIY宏板。<br>
  配置您的密钥、 启动应用程序、 控制介质和切换配置 。
</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest">下载最新版本</a>
   · 
  <a href="#getting-started">开始</a>
   · 
  <a href="#development">发展</a>
</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest"><img src="https://img.shields.io/github/v/release/lolo34dr/deckpilot?style=flat-square&color=78b7a0" alt="最近发布"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe"><img src="https://img.shields.io/badge/Download-Windows_x64-0078D4?style=flat-square&logo=windows" alt="为 Windows x64 下载"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Atelier-Setup.exe"><img src="https://img.shields.io/badge/Download-DeckPilot_Atelier-16856A?style=flat-square&logo=windows" alt="下载 DeckPilot Atelier 数据"></a>
  <a href="https://github.com/lolo34dr/deckpilot/blob/main/LICENSE"><img src="https://img.shields.io/github/license/lolo34dr/deckpilot?style=flat-square" alt="麻省理工学院许可证"></a>
  <a href="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml"><img src="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml/badge.svg?branch=main" alt="窗口构建状态"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases"><img src="https://img.shields.io/github/downloads/lolo34dr/deckpilot/total?style=flat-square&color=70a7e8" alt="GitHub 发布下载"></a>
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

---

DeckPilot 配置已发送的Arduino Leonardo 兼容宏盘 **F13 - F24 (法语)** 作为 USB 键盘输入,排列在 **3 行数 4 密钥**。物理密钥顺序可以是任意的:将每个按钮按到其位置。

**F13–F24键盘特性不需要额外的驱动程序或固件更改. Soundboard 混合需要签名 VB-CABLE 音频驱动程序,缺失时由Setup安装.** 应用程序接口目前为法文。 上面选择一个语言来进行完整的 README 翻译, 包括技术参考和版本历史。 翻译是自动生成的; 此英文原文是参考 。

<a id="features"></a>
## 特征

- **引导校准 :** 识别全部 12 物理按钮而不重线您的宏板。
- **已安装的应用程序拾取器 :** 搜索 Start 菜单快捷键和 Windows 应用程序目录,包括 Microsoft Store 条目。手动文件选择仍然可用。
- **自定义动作 :** 启动应用程序,打开文件夹或网站,发送快捷键,以及控制媒体.
- **未分配按键:** 保留F13–F24,用于在游戏或其他应用程序中直接配置的快捷键.
- **本地简介 :** 创建、复制、重命名、删除和选择配置文件。校准在配置文件之间共享。
- **现成简介 :** 从音乐、生产力或创建模板开始,然后自定义。从物理键切换到特定的、下一个或以前的配置。
- **更快的编辑 :** 复制/剪切/粘贴/重置完整的密钥,记录键盘快捷键,选择密钥重音颜色,并撤销更改.
- **窗口工具 :** 打开声音、显示、蓝牙和网络设置、任务管理器或锁定会话。
- **压缩暗界面 :** 矢量图标,可用时的应用程序图标,以及尊重Windows动画偏好的微妙动画.
- **背景操作 :** 保留 DeckPilot 在通知区域中,默认启用在 Windows 登录中启动(可禁用)。
- **自动更新 :** 批准新的发行,然后让 DeckPilot 下载、验证、替换和重新启动。

<a id="getting-started"></a>
## 开始

<a id="requirements"></a>
### 所需经费

- 窗口 10 窗口 11, **x64 (韩语).**.
- Arduino Leonardo 兼容的宏盘发送 12 区分密钥从 **F13改为F24**.
- USB数据连接.

已发布的下载内容包括.NET运行时间. **您不需要安装. NET来使用 DeckPilot.**

<a id="install-and-launch"></a>
### 安装和发射

1. 下载 **[DeckPilot-Setup.exe(英语:Setup.](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe)** 运行安装器。 Windows 请求管理员允许安装到 **Program Files\DeckPilot**.
2. 安装器让您选择是否 DeckPilot 从 Windows 开始, 自动检查应用程序更新, 并减少动画。 默认情况下启动和更新检查; 减少的动画关闭。 它也检查签名的 VB-CABLE 驱动程序 Soundboard 如果缺少, 则自动安装。 需要互联网连接来检查驱动程序 。
3. 安装器添加 DeckPilot 切换到窗口 **已安装的应用程序** 并创建“启动”菜单快捷键。默认情况下将选择桌面快捷键。
4. 连接您的宏盘并打开 DeckPilot 从它的捷径。
5. 遵循从左到右的校正 **4个栏**,然后下一行,为 **3行线**。每个按钮必须发送不同的F13-F24密钥。

从前者升级时 4- 排队布局 DeckPilot 保存配置/动作,并请求为已更正的位置进行新的校准。在现有的便携式设置上安装还保留了设置。 `%APPDATA%\DeckPilot`; 如果设置要您退出旧副本 。

便携 [DeckPilotưμ㼯A](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot.exe) 财务报告和财务报告 [津巴布韦](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-win-x64.zip) 对于可移植用途,请将可执行文件保存在一个可写入的个人文件夹中。

从 Windows 卸载 **已安装的应用程序**。解除安装可以删除程序及其快捷键,同时保留个人配置,以便后期重新安装可以重新使用。

单文件可执行文件在第一次发布时将其捆绑的依赖性提取到用户的.NET缓存中,这需要时间稍长一点,已发布的可执行文件目前还没有签名,因此Windows可能会显示SmartScreen警告.

<a id="configure-a-key"></a>
### 配置密钥

1. 在网格中选择一个密钥。
2. 在右侧栏选择动作 。
3. 配置网格下方的设置并保存。 切换密钥或配置文件也会保存当前密钥的设置 。

| 行动 | 行为 |
| --- | --- |
| 未分配按键 | F13–F24 通常通过。在游戏或应用程序中直接指定它。 |
| 应用程序 | 启动已安装的应用程序或手动选择的程序或快捷键。 |
| 文件夹 | 在 Windows 浏览器中打开文件夹。 |
| 网站 | 在默认浏览器中打开 HTTP 或 HTTPS 地址 。 |
| 键盘快捷键 | 向活动窗口发送密钥组合 。 |
| 媒体 | 控制回放,音轨,音量,或哑音. |
| 点点 | Controls Spotify 回放并调整的只有Spotify的Windows音频会话音量. |
| 申请 - DeckPilot | 显示已存在的 DeckPilot 窗口,包括在通知区域运行时。 |

对于应用程序动作,请打开应用程序拾取器并按名称搜索。手动文件选择器仍然可用,高级选项下有可选的程序参数。根据目标应用程序的语法输入参数。

持有按钮只会触发已配置的动作一次直到发布。 在自由键模式下, 普通键盘重复事件仍然可用 。

<a id="manage-profiles"></a>
### 管理配置文件

在右侧栏打开配置管理区域以创建、复制、重命名或删除配置文件。使用密钥网格上方的选择器来选择活动配置文件。至少必须保留一个配置文件。

校准适用于所有剖面图,可以从侧边栏重复.

<a id="background-operation"></a>
### 背景操作

关闭窗口隐藏 DeckPilot 。双击时钟附近的通知区域图标以重新打开,或右击图标并选择退出命令以完全停止。

默认启用当前用户的 Windows 启动。 您可以禁用它 。 **Lancer avec Windows**; 后期的应用程序更新保留了这个选择。安装和随后的发射会刷新当前可执行路径的启用条目 。

<a id="installed-applications"></a>
## 已安装的应用程序

拾取者从当前用户的和全系统的 Start 菜单读取快捷键,然后用 `Get-StartApps` 由 Windows 应用程序目录生成的结果。

搜索忽略大小写和重音。 重复名称被分组, 优先使用“ 启动” 菜单条目。 手动仍然可以选择没有注册快捷键的可移植应用程序和程序。 如果无法使用“ Windows” 目录, 可读“ 启动” 菜单快捷键仍然可用 。

Windows 启动选定的快捷键。目录条目使用 `shell:AppsFolder`。可选参数适用于基于文件的程序和快捷键;它们被隐藏在 AppsFolder 条目中。列表应用程序不会启动它们。

<a id="automatic-updates"></a>
## 自动更新

备查自 **0.2.0**,在校准后启动时自动更新检查,然后每6小时检查一次。可以在侧边栏中禁用这些检查。手动检查仍然可用。

当找到较新的稳定 GitHub 释放时, DeckPilot 询问是否更新。 **已安装副本** 下载和SHA -256 校验 `DeckPilot-Setup.exe`,然后使用当前启动、自动更新和减少运动偏好预选启动安装器。您可以在更新时更改它们。Windows请求管理员允许更新程序文件。安装器关闭 DeckPilot 在准备好后优雅地保存配置图和配置,检查并安装签名 VB-CABLE 如果缺少驱动程序, 则重新启动应用程序。 取消安装器会打开应用程序。 设置拒绝中断打开的固件会话 。

**便携式副本** 保留现有的更新机制。在确认后,它:

1. 下载带有进度报告和取消支持的可执行文件。
2. 验证它的SHA...256 反对hash `SHA256SUMS.txt` 从同一个 GitHub 发布。
3. 保存您的设置并准备更新帮助。
4. 退出、替换可执行文件并重新启动。
5. 保留之前的可执行性,直到新进程确认成功初始化;如果启动失败,则恢复并重新启动之前的版本.

选择稍后更新会推迟自动提示 24 手动检查 绕过推迟。

下载使用 HTTPS, 重定向仅限于预期的 GitHub 主机。 临时文件存储在 `%LOCALAPPDATA%\DeckPilot\Updates`; 超过7天的下载被清理。 Windows 启动条目在更新后继续使用相同的可执行路径。

便携式副本需要一个可写个人文件夹。安装副本时使用安装器来保存目录; DeckPilot 应用程序本身继续运行,没有管理员权限 。

**升级自 0.1.x吗?** 从通知区域退出旧版本, 并手动将可执行文件替换为一次最新发布的版本。 这些旧版本不含更新器。 以后的更新可以从内部安装 DeckPilot.

<a id="repository-rename-compatibility"></a>
### 仓库重命名兼容性

版本 **1.0.1** 准备从 `lolo34dr/streamdeck-diy` 改为: `lolo34dr/deckpilot`。该寄存器现在 `lolo34dr/deckpilot`。更新检查使用新名称,并保留与原名称的兼容性。根据这两个精确的寄存器路径验证了发布下载。

使用( U) **1.0.1 或更晚时间** 与重新命名的寄存器。版本 0.2.0 财务报告和财务报告 1.0.0 严格验证原始资产路径, 并可能需要手动更新 。 如果旧版本无法更新, 请从发布页上下载当前可执行文件 。

<a id="settings-and-backups"></a>
## 设置和备份

配置和校准保存在应用程序文件夹外 :

```text
%APPDATA%\DeckPilot\settings.json
```

退出( E) DeckPilot 在复制此文件以备份或恢复您的配置之前。如果设置文件无法读取, DeckPilot 保留副本为 `.backup-<date>` 使用新配置前的后缀。

不需要账户。 更新检查联系方式为 GitHub , 可以在界面中禁用 。

<a id="development"></a>
## 发展

安装 **净值 8 SDK 软件**时,该版本 8.0.415 或允许的较新的兼容版本 `global.json`。视觉工作室 2022 与 .NET 桌面开发工作量是可选的。

从寄存器根运行这些命令 :

```powershell
dotnet restore DeckPilot.sln
dotnet build DeckPilot.sln -c Release --no-restore
dotnet run --project tests/DeckPilot.Checks -c Release
dotnet run --project src/DeckPilot.Windows -c Release
```

核心库和控制台检查也在Linux上运行. WPF接口和Windows输入API需要Windows.

<a id="validation"></a>
### 验证

在第一个失败的断言上,控制台以非零状态检查退出。它们包括:

- 密钥映射,持有密钥重复,发布,配置切换,以及动作复制.
- 设置验证、 持久性和旧配置的迁移 。
- 应用程序搜索与目标处理.
- 发布选择,校验和,截断或损坏的下载,重定向,以及取消.
- 可执行文件替换、备份保存和回滚。

文件锁定检查只在Windows上运行.

GitHub 动作也运行 `tests/windows-update-smoke.ps1` 相对于独立的可执行文件。它启动更新帮助器,退出原始进程,验证替换和启动确认,检查设置保存,然后通过UI自动化打开已安装的应用程序拾取器,并保存一个选择。它还练习重新命名的可执行文件以及包含空格的路径。此脚本保留给可支配的 GitHub 动作测试账户。 CI 也运行 `tests/windows-installer-smoke.ps1`:真正的程序文件安装,桌面/开始菜单快捷键,默认启动,升级时应用打开,禁用启动保存,不删除配置文件而未安装.

<a id="publish-a-standalone-windows-folder"></a>
### 发布独立的窗口文件夹

```powershell
dotnet publish src/DeckPilot.Windows -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false -o artifacts/win-x64
Compress-Archive -Path artifacts/win-x64/* -DestinationPath artifacts/DeckPilot-win-x64.zip -Force
```

分发完整的归档。 Windows 构建工作流程也会生成此软件包 。

<a id="publish-a-github-release"></a>
### 发布 GitHub 发布

更新应用程序版本 `.github/release-notes.md`,然后按下匹配键 `v*` 标记,例如 `v1.0.0`。发布工作流程检查标记是否匹配项目版本,并在发布前运行验证:

| 下载 | 目录 |
| --- | --- |
| `DeckPilot.exe` | 自成一体的,单文件的Windows x64应用程序. |
| `DeckPilot-win-x64.zip` | 完整的自定义应用程序文件夹 。 |
| `SHA256SUMS.txt` | 嘘...256 两次下载的散列。 |

对于 Windows ARM64, 单独发布 `-r win-arm64` 并验证目标机器。

<a id="hardware-checks-on-windows"></a>
## 对 Windows 的硬件检查

连着莱昂纳多:

- 全部校准( T) 12 任意的 F13 – F24 命令中的位置 。 确认重复的密钥被拒绝, 并取消保存先前的映射 。
- 在游戏中指定一个免费的密钥,并检查游戏是否正常收到.
- 启动一个已配置的程序, 然后按住按钮 : 它应该发射一次直到发布。 包含包含空格和可选参数的路径 。
- 测试一个文件夹,一个网站,一个普通应用程序中的快捷键,以及媒体控制.
- 通过快捷键动作发送 F13-F24 并确认没有循环 DeckPilot 行动。
- 创建两个包含不同动作的配置, 在它们之间切换, 退出, 并重新启动。 配置和校准应该持续 。
- 关闭窗口, 触发动作, 然后通过通知区域图标重新打开并退出。 一次 DeckPilot 出入口,所有密钥应正常通过.
- 再次签名后启用 Windows 启动并测试; 如果不需要, 则在之后禁用 。

<a id="current-limitations"></a>
## 目前的限制

- 密钥监听对 Windows 会话是全球性的。 来自另一个键盘的物理 F13– F24 密钥遵循相同的配置。 软件注入的键盘事件被忽略以防止动作循环 。
- 某些游戏,反热系统,或使用直接设备输入的程序仍然可能收到被截取的密钥. Free-key模式仍然可用. 登录屏幕,安全桌面,以及其他会话都在应用程序的控制之外.
- Windows可能屏蔽发送给具有管理员权限运行的应用程序的快捷方式. DeckPilot 运行时没有管理员权限,也不绕过此限制。
- 在启动快捷键动作前释放 Ctrl, Alt, Shift, 和 Windows 。 Alt+Tab 等组合仍然受 Windows 行为的约束 。
- 断开连接检测、 自动每局配置以及一个签名的安装器是未来的改进。 配置文件目前是手动选择的 。

<a id="project-structure"></a>
## 项目结构

| 路径 | 目的 |
| --- | --- |
| `src/DeckPilot.Core` | 设置, 密钥状态, 搜索, GitHub 发布验证, 经核实的下载, 以及替换交易 。 |
| `src/DeckPilot.Windows` | WPF接口,应用程序目录,图标,Windows动作,通知区集成,以及更新帮助器. |
| `tests/DeckPilot.Checks` | 没有外部测试框架的可执行检查 。 |
| `tests/windows-update-smoke.ps1` | 原生Windows更新和CI使用的应用程序采集器烟雾测试. |
| `deckpilotlogo.png` | 应用标志. |
| `deckpilotbanner.png` | 项目横幅. |

<a id="license-and-credit"></a>
## 许可证和信贷

DeckPilot 将在 **[MIT 许可证](../../LICENSE)**。您可以使用、复制、修改和再分配,包括商业用途,只要您保留许可证所要求的版权和许可证通知。

如果你继续前进 DeckPilot,请提供少量可见的信用和与该存储库的链接。这些额外信用是可选的;仍然需要保存麻省理工学院的通知。

建议的信贷:

> 依据 [DeckPilot](https://github.com/lolo34dr/deckpilot) 由 [lolo34dr (英语).](https://github.com/lolo34dr).

<a id="macropad-firmware-updates"></a>
## Macropad 固件更新

设备准备和车间质量检查,安装单独的 [DeckPilot 爱特利尔](../../docs/FACTORY.md) 工具。它的安装器作为 **[DeckPilot- Atelier - Setup.exe (英语).](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Atelier-Setup.exe)** 它可以将包含的固件闪到Leonardo/Caterina 板上,然后检查所有的短和长按 12 物理密钥并导出报表。

DeckPilot 1.1.0 包括a **Firmware du macropad…** 对话框和固件 1.0.0 支持微型ATmega32U4 **5 第 五 条 国 家 16 兆赫( MHz)**,使用所有者上传日志确认的Leonardo/Caterina配置。捆绑的Windows闪存工具不需要Arduino IDE。设备选择和确认是强制性的;应用程序更新从未默默地闪烁过棋盘。现有的F13-F24映射、配置和校准仍然兼容。

**老板已经确认成功在应用中闪光,固件 1.0.0 身份认证和所有 12 按钮。** 保存原始草图并访问 RST/ GND 以供恢复。 固件闪烁不能自动回滚 。 见 [固件文档](FIRMWARE.zh-CN.md) 用于兼容性、验证、回收、构建步骤和第三方许可。按键 USB 模式和本地媒体控制不属于此固件发布的一部分。

<a id="building-the-windows-installer"></a>
### 构建窗口安装器

生产后 `artifacts/standalone/DeckPilot.exe` 与嵌入式固件捆绑, 安装 Inno 设置 6 运行 `./installer/build.ps1` 从寄存器根。输出为 `artifacts/installer/DeckPilot-Setup.exe`. CI 构建并测试此安装器, 然后再将其附加到发布器中。 安装器使用稳定的应用程序 ID, 因此未来的版本会升级同样的安装 。

<a id="connection-dashboard-and-firmware-checks-130"></a>
## 连接仪表板和固件检查(1.3.0)

那个... **Votre macropad** 卡片显示连接状态、检测到的固件和任何可用的捆绑更新。 **Connexion et diagnostic** 当多个 Leonardos 连接起来时, 显示 USB 链接状态、 物理按钮计数、 固件通知偏好和可复制的诊断摘要 。 诊断不复制配置文件动作或 USB 序列标识符 。 显示器在打开固件管理器时重新连接, 并自动放弃其序列端口 。 F13– F24 键盘动作继续使用固件工作 。 1.0.0 而且没有诊断联系。

启动固件检查是默认启用的。 应用程序更新检查和校准出局后, 已识别的旧固件可以启动安装; 延迟提醒 24 小时。未知的棋盘和多个未选中的候选人不会自动闪烁。固件发布会与来自 GitHub 的应用程序更新捆绑在一起,而不是从独立的固件种子中获取。

固件 **1.1.1** 添加会话识别、 相关心跳响应、 绑定消息和物理密钥状态事件。 这些事件激活了所映射的按钮并更新了诊断; 它们从未第二次引用动作 。 尽管早先的固件, 它的第一次物理板验证仍然是必要的 。 1.0.0 和闪存程序已被所有者确认。 [协议和兼容性文档](FIRMWARE.zh-CN.md).

<a id="keypad-interface-140"></a>
## 键盘接口( E)1.4.0)

大一点的 3 × 4 keypad 遵循物理封装和黑色密钥盖。每个密钥都显示其主图标、标签、F13-F24映射和一个小动作类型的图标;工具提示保留了完整的标签和位置。按下反馈、运行动作、成功的发送和错误有不同的外观,包括符号,而不仅仅是颜色。动画遵循了Windows动画的偏好。

选择要编辑的密钥 。 **Options avancées · fichier et arguments** 保存手动应用程序路径,启动参数默认崩溃,而必要的文件夹和网站目的地仍然在扩展。崩溃设置不会清除它们的值。 **Tester l’action** 保存并发送当前 PC 上的选定动作; 成功指示器确认发送, 而不是在另一个应用程序内完成。 键盘快捷键瞄准活动窗口。 免费 F13– F24 密钥没有测试动作 。

当宏板断开时, **Hors ligne** 和随附的提示解释,配置仍然可以编辑和测试动作。配置保存在PC上,并在整个断开和重联中保持可用。这些接口改进与现有的固件工作,不需要额外的闪存。

通知区域菜单现在直接切换配置文件 。 **Ctrl+S 调制器** 保存当前按钮配置。在使用新配置之前,已备份无效的配置文件。保留了 App 安装、桌面/启动菜单图标、启动首选项和现有的自动更新器。

对于手动Arduino IDE上传, **退出( E) DeckPilot 从其托盘菜单中** 以释放串行端口。关闭主窗口使其在背景中运行。

<a id="spotify-action-search-and-drag-and-drop-150"></a>
## 注意、动作搜索和拖放( E)1.5.0)

**点点** 与 **Multimédia**。它的播放/暂停, 下一条命令和先前的命令在 Spotify 桌面应用程序的 Windows 媒体会话中地址, 即使浏览器正在播放 YouTube 。 Spotify 卷 +/ - 调整 Spotify 的 Windows 音频会话在 5% steps, 保留主音量和其他播放器不变。 Spotify 必须打开, 并有可用的播放会话; 必要时启动一次音轨。 不支持在浏览器中Spotify 。 Spotify 会话缺失会产生一个错误, 无法返回全局媒体密钥或其他播放器。 不需要Spotify账户授权、 Premium 订阅或存储的令牌 。 DeckPilot窗口 10 版本 2004 或稍后,或视窗 11,这是该建筑要求的。

使用( U) **Rechercher une action ou une application** 查询,例如: `volume`, `skip`, `Spotify` 或已安装的应用程序名称。在第一次搜索时安装的应用程序索引负载将被缓存5分钟;手动文件选择仍然可用。单击结果可以配置所选的密钥,或者将其拖放到另一个密钥上。

从目录或搜索结果中拖动动作到密钥上指定它。将一个配置的密钥拖到另一个密钥上 **互换行动**; 物理校准不变。您也可以降低一个 `.exe`, `.lnk`, `.appref-ms`,文件夹或 HTTP(S) URL 在密钥上。只丢弃密钥而不执行动作。在编辑器中完成任何缺失的设置。

Windows 启动现在保留了明确的每个用户的偏好,修复一个 stale 或缺失的 Run 条目,并报告已知的 Windows 启动禁用旗。正常的启动尊重 Windows 禁用; 明确启用 **Lancer avec Windows** 清除已知的, DeckPilot 当前用户的禁用旗。 背景发射即使在校准前仍然隐藏, 重复背景发射则悄悄退出。 更新后, 打开 **Préférences de l’application**,检查启动状态,并在Windows屏蔽时重新启动选项。在用户的PC上真正的签出/签名仍然是对Windows政策或机器特定限制的最终检查。

<a id="sharp-keypad-rendering-profile-recovery-and-audio-controls-161"></a>
## 尖锐的键盘渲染、剖面恢复和音频控制(1.6.1)

键盘现在按照本地的WPF大小而不是通过 Viewbox 缩放文本。 父/ 键阴影效果和文本缩放动画被删除; 较小的窗口滚动而不是缩小键盘。 app 宣布了每个监视器的DPI 意识, 并为桌面应用程序使用更大的 Windows shell 图标 。

**Annuler / Rétablir** 恢复配置文件编辑,包括拖放交换、动作设置、导入和配置文件创建/删除。历史保存 50 在应用程序会话中编辑。 Ctrl+Z / Ctrl+Y 运行在文本字段以外的配置文件上; 文本字段保留自己的撤销/重做。 校准、 启动首选项和执行的动作不会被取消 。

下级 **Gérer les profils**,将当前配置文件或所有配置文件导出为 `.deckpilot.json`,将配置文件导入为新副本,或恢复所有配置文件的备份。导入时从未执行动作,也从未替换个人计算机的校准。配置文件包含动作路径、参数和音频端点ID,可能需要在另一个个人计算机上调整。未知格式、大小过大的文件和错误的手势配置被拒绝。

在保存更改的配置文件之前, DeckPilot 将上一个配置保存在 `%APPDATA%\DeckPilot\Backups` (20 版本, 内容复制 。 只有首选项保存不会创建配置备份。 备份失败无法覆盖当前设置。 在确认后恢复替换配置文件, 并可以自行撤销; 校准不变 。

**Microphone Windows** 切换当前所有激活的 Windows 捕获端点上的哑弹。持续状态栏和 Audio Windows 卡显示活动/muded/mixed/unopen性状态,每两秒钟并在应用程序命令之后刷新一次。一个失败的多端点更新尝试恢复先前的哑弹状态。这些指标描述的是Windows端点状态,而不是硬件开关或应用程序本身的哑弹按钮。

**Changer de sortie audio** 在Windows控制台、多媒体和通信默认情况下,两个选定的连接输出设备之间的替代设备。一个缺失的目标会出错,而不选择另一个设备。具有自己固定输出的应用程序可能不会跟随Windows的默认程序。导入的端点ID会被显示为无法使用,直到适应当前的PC。失败的路由更改试图恢复先前的默认。

要配置 **appui court / appui long**,首先选择一个已配置的动作,启用双动作选项,选择手势进行编辑,并从中设定阈值 200 改为: 3000 ms. 没有这个模式, 动作会保留直接的按键下行为。 有了它, 短动作会在阈值前发布; 长动作会在阈值前运行一次( 如果计时器尚未运行的话) 。 长动作也不会运行短动作 。 免费的 F13- F24 通过键不能使用双动作模式 。 在配置/ 配置变化、 校准、 固件操作、 会话变化和暂停时, 手势会被取消 。 不需要固件闪烁; 现有的键盘的按键下/ 键报告提供了手势 。

核心检查覆盖了重复/释放边界,同时持有,取消,配置历史/交换/备份以及使用假端点的音频回滚. Windows UI检查练习实际拖动取消/重做,配置一个长动作和阈值,重新大小后本地键盘尺寸和麦克风指示器. 实际的麦克风驱动程序,头盔/语音切换和物理控件仍然需要验证所有者的PC.

<a id="search-and-ci-reliability-fixes-164"></a>
## 搜索和CI可靠性修正(1.6.4)

等效的搜索结果保留其现有的 WPF 按钮。 安装的应用程序目录和调试计时器在抓取搜索按钮或拖曳时推迟结果替换。 这使得源按钮无法在互动中被移除 。

Windows 烟雾检查继续执行本地鼠标拖放并验证保存的任务、互换、撤销/重做以及两种按下手势。测试客户端使用物理光标坐标来匹配不管DPI的UI自动化边框,在测量前聚焦,并扫描所有滚动百分比。回归检查还验证等效搜索保留了相同的可访问按钮。背景启动断言等待WPF的装入处理器,并在应用程序还活着时需要连续五个隐藏窗口的观察。失败报告阶段、光标目标、DPI和最后一个滚动几何。

使用 Node.js 构建和发布工作流程 24 动作。Arduino 编译器仍然位于 CLI 。 1.2.2; CI 下载它的 Windows 存档时带有一个绑定的 SHA -256 校验和。固件及其工具链版本不变。

在Windows测试中读取的设置明确允许读/写/删除共享和解析UTF-8 关闭读取器后, 这使得测试本身无法拒绝应用的原子文件替换 。 历史测试在三个撤销/ 重做周期中打开了旧的快照, 并验证当活的设置改变时它是否仍然完整 。

在 Windows 上,已存在的设置文件被执行 `File.Replace` 而不是覆盖模式 `File.Move`。这支持在保留完整的旧快照的同时允许删除的阅读器。核心套件测试了三个被保存的阅读器替换的功能;Windows核心检查在固件编译和发布之前运行,从而及早检测到持久性失败。

测试快照阅读器可能要等待到 500 用于 Windows 共享/ 锁定违反时的 ms `ReplaceFile` 执行。等待仅适用于打开快照,绝不执行或重新执行应用程序动作。其他错误、错误的 JSON 和持续锁定仍然是失败。

<a id="runtime-and-editing-optimizations-170"></a>
## 运行时间和编辑优化( E)1.7.0)

保存相同的设置检查当前文件, 但跳过替换和备份工作。 外部编辑/ 删除和失败的写入仍然被检测到 。 `ButtonAction` 具有等值(包括长动作) ; 编辑器脏检查避免 JSON 序列化 。 History 保存离散的模型快照, 验证新状态, 并避免 JSON 在撤销/ 重写时解析 。 50- 编辑限制和 20 仍然保留了配置备份。

搜索和拾取器共享一个不可改变的安装应用程序目录,时间为5分钟(15 。 同步消费者共享一个扫描; 关闭一个拾取器不会取消另一个消费者。 刷新绕过缓存, 每个拾取器有自己的过滤/视图。 扫描和目录解析从 UI 线程中运行; 拾取器过滤器被解开 120 ms. 动作搜索检查此缓存在解开后和从拾取器返回时重新检查, 因此那里也有一个刷新目录 。

董事会 UI 更新现在跟随连接通知, 而不是每次唤醒 100 ms. 已订购的断开通知仍然取消等待搁置。 键盘勾结队列的视频工作, 而压制和手势计时仍然同步。 隐藏/ 最小化窗口停止反馈动画并使用10秒的音频投票间隔, 而不是2秒; 重新启动或发布音频命令请求新鲜状态 。 等待音频刷新时共享一个循环, 并包含在现有读取时到达的请求 。 未更改的音频快照和冷冻刷可以避免重复的 UI 分配 。 通过启动应用程序返回的处理包装器会被处理而不关闭已启动的程序 。

这些修改会减少指定的工作;它们不会建立机器独立的CPU/RAM百分比. 原生硬件音频切换和物理密钥行为仍然需要在Windows PC上与宏盘进行验证.

<a id="workspace-tools-and-refreshed-interface-180"></a>
## 工作空间工具和刷新接口(1.8.0)

木炭接口将动作库放在首位, 将配置控件与简洁的本地大小区分开来 3 × 4 keypad,和卡片中的组合动作设置。关键重音区分自己的类别;选择、键盘焦点和动作反馈仍然分开。 **Réduire les animations** 在首选项中补充 Windows 动画设置。较小的窗口保留滚动访问,而不是缩小密钥文本。

- **配置模板 :** 下级 **Gérer les profils**,添加音乐和音频,桌面和生产力,或创建。每个模板创建单独的可编辑配置文件,从不执行它的行动。保留现有的配置文件及物理校准。键盘快捷键使用主动应用程序,其约束可能有所不同。
- **配置键 :** 指定 **Changer de profil**,然后向前/后向选择一个命名的配置文件或循环。在导入一个集或复制自引用配置文件时,在配置文件之间重映射引用。缺失的目的地报告错误,可以在编辑器中替换。
- **视窗命令 :** 使用情况 **Commandes Windows** 打开普通设置或任务管理器,或明确锁定 PC。命令来自一个固定的列表,没有脚本可以输入。锁定动作需要签名返回到 Windows 中。
- **完整密钥剪贴板 :** 右键单击可以复制、剪切、粘贴或重置的密钥, 包括长动作和重音。 Ctrl+C/ X/V 在甲板键有键盘焦点时删除工作; 普通文本字段保留其编辑快捷键。 密钥剪贴板留在里面 。 DeckPilot ,然后在退出时清除。这些编辑支持撤销/重做。
- **快捷键记录器 :** 选择键盘动作并单击 **Enregistrer un raccourci**。按一个支持的组合,然后保存它。退出取消;F13-F24在录制时被保留给宏板。Windows保留组合仍然需要手动控制。
- **快速访问 :** Ctrl+K或库按钮聚焦动作搜索. **Activité récente** 保留最后一个 30 发送/ 失败此会话的内存动作, 不存储程序参数或目标路径。 它可以随时清除。 成功发送并不能保证目标应用程序的完成 。

坚固的遗物 **1.1.1**。接受自动固件提议现在正确打开固件管理器;前一个模式守护阻止了这种交接。现有的固件身份、心跳、重联和闪存检查仍然有效。新的工作空间特性不需要固件闪存。

<a id="soundboard-speaker-return-218"></a>
## Soundboard 发言者返回( E)2.1.8)

内 **Soundboard · micro virtuel**,启用 **声板之子** 通过您的耳机或扬声器以及虚拟麦克风听到剪辑。选择物理输出并设置单独的返回音量。 0 改为: 100% 。 此返回默认关闭, 并保存在这个 PC 上。 它会播放 **只有音板剪辑**;您的物理麦克风从未添加到扬声器输出中。虚拟麦克风仍然将您的声音和剪辑发送到 VB-CABLE.

更改音量不会打断虚拟麦克风。 如果您选中的扬声器断开, 返回会停止, 而虚拟麦克风会继续。 重新连接, 刷新设备列表, 或选择其他输出。 耳机会帮助您阻止物理麦克风在声学上接收剪辑 。

<a id="open-deckpilot-and-profile-notifications-217"></a>
## 打开 DeckPilot 和概况通知(续)2.1.7)

下级 **Ouvrir une 应用程序**,选择内置 **DeckPilot** 条目或单击 **施药者 Ouvrir DeckPilot »**。这一行动带来了现有的 DeckPilot 窗口到前面;其目标在应用程序更新或重新安装后仍然有效。它也可以在动作搜索中找到。

更改配置文件显示一个简短的、 点击的通知, 并带有活动配置文件名称 。 in **Préférences de l’application**,您可以禁用它,选择任何角落,或者设置自定义的横向和纵向位置。 **Enregistrer and prévisioniser 互联网档案馆的存檔,存档日期2013-12-02.** 立即显示结果。通知跟随包含前方应用程序的屏幕,并同时显示 DeckPilot 隐藏。它可以在无边框/窗口游戏上显示;独家全屏游戏可能覆盖普通的Windows覆盖。

<a id="deckpilot-210-fixed-signed-virtual-microphone-routing"></a>
## DeckPilot 2.1.0: 固定的签名虚拟麦克风路由

DeckPilot 现在默认使用深黑界面。 in **Préférences de l’application**,修改背景、面板和重音颜色 `#RRGGBB` 值; 您的主题被保存在这个 PC 上。

那个... **Soundboard · micro virtuel** 面板允许您选择物理麦克风和 MP3/WAV 剪辑。 DeckPilot 自动将混音向 VB- Audio 路径 VB-CABLE 回放结束点; 在 Discord 或游戏中, 选择 **`CABLE Output`** 在安装和更新过程中, DeckPilot 检查 VB-CABLE 并在缺失时安装。 它会在 HTTPS 上下载官方软件包, 并在运行驱动程序无声安装器前验证认证码发布器。 这需要互联网连接和管理员权限 。 DeckPilot 不捆绑或重新分配 VB-Audio 文件。如果驱动程序稍后丢失,您可以从 Soundboard 面板。音频引擎使用 NAudio (麻省理工学院;见 [第三方当事人通知](../../third-party/NAudio-LICENSE.txt)). VB-CABLE 是一个单独的许可,签名的Windows驱动程序,而不是MIT组件.

<a id="deckpilot-atelier-190"></a>
## DeckPilot Atelier( 发音 )1.9.0)

Atelier现在在闪烁前检查选中的棋盘和固件,比较安装和捆绑的版本,并在中断写入后提供经过核实的重试. **温柔的吃客们...** 分析并按顺序闪烁所有检测到的棋盘,并可以导出批量报告。 12- 钥匙测试 验证物理 01→12 ,记录F13–F24映射,并在质量报告中包含固件和每键结果.


单独的 **DeckPilot 爱特利尔** Windows安装器准备新的 DIY 宏板。 它检测Leonardo/ Caterina 板, 验证和闪烁捆绑的固件而不覆盖装货机, 然后引导一个物理 3×4 检查所有 12 按钮。每个按钮都必须通过一个短的按键和至少一个控件 700 ms. 工具记录F13–F24信号属于每个物理位置,标记多或不一致的信号,并输出一个时间标记的报告。下载 `DeckPilot-Atelier-Setup.exe` 从发布页中。 Caterina 启动器必须已经存在; 该工具不会安装丢失的启动器 。

DeckPilot Atelier 检查 GitHub 启动时和需求时的应用程序和捆绑的软件更新。更新需要确认并使用发布 SHA -256 显示。 固件永远不会自动被更新闪烁; 在 Atelier 更新后, 当您准备对连接的板进行程序化时, 请选择固件动作 。

爱特利尔 1.0.7 添加一个确认 **Effacer le 方案 DeckPilot** 动作,它清除 28 KiB 应用区,带有经过验证的安全停止指令,因此棋盘停止作为键盘作用,同时保留Caterina进行恢复和未来的闪烁,它使启动器,EEPROM和Windows剖面没有受到任何影响.

版本 1.9.1 通过在已签名的 Windows 会话中启动安装后修复启动 Atelier 。 它的更新器可以安装更新的 Atelier 软件包或固件捆绑,然后重新启动应用程序。 Windows 释放管道检查 , 首先为 Atelier 安装器启动、升级和卸载 。

版本 1.9.2 改进Atelier Windows包装步骤,并在CI中报告其准确的故障位置.

版本 1.9.3 校正Windows软件包如何读取包含的固件版本,因此Atelier的更新检查跟踪了安装器中运出的固件.

Atelier 发布检查现在报告,如果 Windows 无法打开其安装的窗口,则启动诊断。

DeckPilot 1.9.6 修正 Atelier 的 Windows DPI 显示, 所以安装的应用程序在 Windows 上发射正确 10 财务报告和财务报告 11.

DeckPilot 1.9.7 在 Atelier 运行时修正未安装的 Atelier 。 Windows 发布测试还验证了启动、 位置更新和未安装 。

DeckPilot 1.9.8 增加一个明确,经确认的Atelier重置,用经核实的空素描来取代棋盘的用户程序,并保留了Caterina rootloader.

验证涵盖遗留设置,重音验证,剖面导航和参考重映射,可移植模板,本地Windows模板/undo/redo和剖面切换,快捷键记录,复制/粘贴/重置,设置持续,安装/更新,布局调整大小和实际UI抓取. 硬件特定的行为仍然需要在Windows PC上使用宏编程器和音频设备.

<a id="profile-sound-files-and-workshop-safety"></a>
### 配置声音文件和车间安全

配置导出包括引用的 MP3/WAV 剪辑(最多 32 每套音频的 MiB 。 导入复制嵌入式音频 DeckPilot管理中的 Sounds 文件夹和重新映射标识符而不替换已有的剪辑。 自动配置备份使用相同的格式; 无法使用的音频被记录为缺失而不是静默替换。 如果您超过导出限制, 请保留源文件。 遗留配置文件仍然支持。 删除一个仍然被分配到短或长动作的剪辑会被屏蔽 。

Atelier 接受尚未回复的棋盘 DeckPilot 协议,同时在编程前仍然检查 USB 位置, Caterina rootloader 和 ATmega32U4 签名。每次测试会话以空结果开始,其报告仍与测试的棋盘相关。在闪存或重置时关闭应用程序会被屏蔽,直到写入和校验完成。

<a id="live-microphone-latency"></a>
### 现场麦克风潜伏

DeckPilot 2.1.3 进程在输出驱动的小块中实时音频。它请求 20 ms 事件驱动的麦克风抓取和 40 ms 回放缓冲器。 麦克风队列被绑定为 100 ms 和 丢弃 旧的 完整 帧 , 如果重播 摊位的话; 这样做可以避免构建秒的延迟语句。 这些是软件缓冲设置, 而不是一个测量的端到端的延迟性保证 。 Windows, 麦克风, VB-CABLE 而接收的app也会造成延迟. Discord 语音隔离设置没有变化,可能因为接收到混合信号而压制音板剪辑.

<a id="automatic-virtual-microphone-startup"></a>
### 自动虚拟麦克风启动

DeckPilot 2.1.4 当选定物理麦克风时自动激活虚拟麦克风 VB-CABLE 。这在 Windows 签名后的背景中也能工作,当 **从 Windows 开始** 启用 。 丢失的设备每5秒钟重新测试一次。 停止虚拟麦克风将自动启动明确保存为已禁用; 可以在 Soundboard 面板。只有在请求时才会播放剪贴板。
