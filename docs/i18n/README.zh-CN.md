>  这是完整 README 的自动翻译。 英文原作是参考文献。 应用程序界面标签和命令被精确保存 。

<p align="center">
  <img src="../../deckpilotbanner.png#gh-light-mode-only" alt="DeckPilot 横幅" width="760">
  <img src="../../deckpilotlogo.png#gh-dark-mode-only" alt="DeckPilot 标志" width="112">
</p>

<h1 align="center">DeckPilot</h1>

<p align="center">
   您的视窗同伴 12- 关键DIY宏板。<br>
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
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest"><img src="https://img.shields.io/github/v/release/lolo34dr/deckpilot?style=flat-square&color=78b7a0" alt=" 最近发布"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe"><img src="https://img.shields.io/badge/Download-Windows_x64-0078D4?style=flat-square&logo=windows" alt=" 为 Windows x64 下载"></a>
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

DeckPilot 配置已发送的Arduino Leonardo 兼容宏盘 **F13 - F24 (英语).**  作为 USB 键盘输入,排列在 **3  行数 4 密钥**。物理密钥顺序可以是任意的:将每个按钮都按到其位置上。

**不需要额外的驱动程序或固件更改 。** 应用程序接口目前为法文。 选择上面的一款语言来进行完整的翻译 README, 包括技术参考和版本历史。 翻译是自动生成的; 此英文原文是参考文献 。

<a id="features"></a>
## 特征

- **引导校正 :** 识别所有 12 物理按钮,不重接您的宏板。
- **已安装的应用程序拾取器 :** 搜索 Start 菜单快捷键和 Windows 应用程序目录,包括 Microsoft Store 条目。手动文件选择仍然可用。
- **自定义动作 :** 启动应用程序,打开文件夹或网站,发送快捷键,并控制媒体.
- **未分配按键:** 保留F13-F24,用于在游戏或另一应用程序中直接配置的快捷键.
- **本地简介 :** 创建、复制、重命名、删除和选择配置文件。校准在配置文件之间共享。
- **已准备好的配置图 :** 以音乐、生产力或创建模板开始,然后自定义它们。从物理密钥切换到特定的、下一个或以前的配置 。
- **快速编辑 :** 复制/剪接/粘贴/重置完整的密钥,记录键盘快捷键,选择密钥重音颜色,并撤销更改.
- **窗口工具 :** 打开声音、显示、蓝牙和网络设置、任务管理器或锁定会话。
- **压缩暗界面 :** 矢量图标,应用程序图标(如果有的话),以及尊重Windows动画偏好的微妙动画.
- **背景操作 :** 保留 DeckPilot  在通知区域中,默认启用在 Windows 登录中启动(可禁用)。
- **自动更新 :** 批准新的发行,然后让 DeckPilot 下载、验证、替换和重新启动。

<a id="getting-started"></a>
## 开始

<a id="requirements"></a>
### 所需经费

- 窗口 10 或 窗口 11, **x64 (韩语).**.
- Arduino Leonardo 兼容的宏盘发送 12 区分密钥 **F13改为F24**.
- USB数据连接.

已发布的下载内容包括.NET运行时间. **您不需要安装. NET来使用 DeckPilot.**

<a id="install-and-launch"></a>
### 安装和发射

1. 下载 **[DeckPilot-Setup.exe (英语).](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe)** 并运行安装器。 Windows 请求管理员允许安装到 **Program Files\DeckPilot**.
2. 安装器添加 DeckPilot 转到窗口 **已安装的应用程序**,创建“启动”菜单快捷键,并默认选择桌面快捷键和Windows启动选项。您可以取消这些可选任务。
3. 连接您的宏盘并打开 DeckPilot  从它的捷径。
4.  按照从左到右的校正 **4个栏**,然后下一行,为 **三行线**。每个按钮必须发送不同的F13-F24密钥。

 当从前者升级时 4- 排行图, DeckPilot 保存剖面图/动作,并要求为已更正的位置作一新的校正。在现有的便携式设置上安装还保留了设置。 `%APPDATA%\DeckPilot`; 如果设置要您退出旧副本 。

便携 [DeckPilot.exe (英语).](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot.exe) 和 [津巴布韦](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-win-x64.zip)  对于可移植用途,请将可执行文件保存在一个可写入的个人文件夹中。

 从窗口卸载 **已安装的应用程序**。解除安装可以移除程序及其快捷键,同时保留个人配置,以便后期重新安装可以重新使用。

单文件可执行性在第一次发布时将其捆绑的依赖性提取到用户的.NET缓存中,这需要时间稍长一点. 已发布的可执行性目前还没有签名,因此Windows可能会显示SmartScreen警告.

<a id="configure-a-key"></a>
### 配置密钥

1.  在网格中选择一个密钥。
2.  在右侧边栏选择动作 。
3. 配置网格下方的设置并保存。切换密钥或配置文件也会保存当前密钥的设置 。

| 行动 | 行为 |
| --- | --- |
| 未分配按键 | F13–F24 正常通过。在游戏或应用程序中直接指定。 |
| 应用 | 启动一个已安装的应用程序或手动选择的程序或快捷键。 |
| 文件夹 |  在 Windows 浏览器中打开文件夹。 |
| 网站 |  在您的默认浏览器中打开 HTTP 或 HTTPS 地址 。 |
| 键盘快捷键 | 向活动窗口发送密钥组合 。 |
| 媒体 | 控制回放,音轨,音量,或哑音. |

 对于应用程序动作,请打开应用程序拾取器并按名称搜索。手动文件选择器仍然可用,在高级选项下有可选的程序参数。根据目标应用程序的语法输入参数。

持有按钮只触发已配置的动作一次直至发布。 在自由键模式下, 普通键盘重复事件仍然可用 。

<a id="manage-profiles"></a>
### 管理配置文件

 在右侧栏打开配置管理部分以创建、复制、重命名或删除配置文件。使用密钥网格上方的选择器来选择活动配置文件。至少必须保留一个配置文件。

校正适用于所有剖面图,可以从侧边栏重复.

<a id="background-operation"></a>
### 背景操作

关闭窗口隐藏 DeckPilot 。双击时钟附近的通知区域图标以重新打开,或右击图标并选择退出命令以完全停止。

 为当前用户默认启用了 Windows 启动。 您可以用它来禁用 **Lancer avec Windows**; 后期的app更新保留了该选择。安装和随后的发射刷新了当前可执行路径的已启用条目 。

<a id="installed-applications"></a>
## 已安装的应用程序

采取者从当前用户的和全系统的 Start 菜单读取快捷键,然后用 `Get-StartApps`  来自 Windows 应用程序目录的结果。

搜索忽略了大小写和重音。 重复名称被分组, 优先使用“ 启动” 菜单项。 可移植应用程序和没有注册快捷键的程序仍然可以手动选择。 如果无法使用“ Windows” 目录, 可读“ 启动” 菜单快捷键仍然可用 。

Windows 启动选定的快捷键。 Catalog 条目使用 `shell:AppsFolder`。可选参数适用于基于文件的程序和快捷键;它们被隐藏在 AppsFolder 条目中。列表应用程序不会启动它们。

<a id="automatic-updates"></a>
## 自动更新

备查自 **0.2.0**,在校正后启动时自动更新检查,然后每6小时检查一次。可以在侧边栏中禁用这些检查。手动检查仍然可用。

 当找到较新的稳定 GitHub 释放时, DeckPilot 询问是否更新。 **已安装副本** 下载和SHA -256 校验 `DeckPilot-Setup.exe`,然后启动安装器。Windows请求管理员允许更新程序文件。安装器关闭 DeckPilot  在准备完毕后, 优雅地保存配置和启动首选项, 安装新版本并重新启动应用程序 。 取消 Windows 权限会让应用程序打开 。 设置拒绝中断一个打开的固件会话 。

**便携式副本** 保留现有的更新机制。在确认后,它:

1. 下载带有进度报告和取消支持的可执行文件。
2. 验证它的SHA...256 hash 反对 `SHA256SUMS.txt`  从同一个 GitHub 发布。
3. 保存您的设置并准备更新帮助。
4. 退出、替换可执行文件并重新启动。
5. 保留之前的可执行性,直到新进程确认成功初始化;如果启动失败,则恢复并重新启动上个版本.

选择稍后更新会推迟自动提示 24 手动检查绕过延迟。

下载使用 HTTPS, 重定向仅限于预期的 GitHub 主机。 临时文件存储在 `%LOCALAPPDATA%\DeckPilot\Updates`; 已清理了超过7天的下载。 Windows 启动条目在更新后继续使用相同的可执行路径。

 可移植副本需要一个可写入的个人文件夹。安装副本时使用安装器来保存目录; DeckPilot 应用程序本身继续运行,没有管理员权限 。

**升级从 0.1.x吗?**  从通知区域退出旧版本, 并手动将可执行文件替换为一次最新发布的版本。 这些旧版本不包含更新器。 以后的更新可以从内部安装 DeckPilot.

<a id="repository-rename-compatibility"></a>
### 仓库重命名相容性

版本 **1.0.1** 准备从 `lolo34dr/streamdeck-diy` 改为: `lolo34dr/deckpilot`。该寄存器现在 `lolo34dr/deckpilot`。更新检查使用新名称,并保留与原名的相容性。根据这两个精确的寄存器路径验证了发布下载。

使用 **1.0.1 或更晚时间** 与重新命名的寄存器相连接。 0.2.0 和 1.0.0 严格验证原始资产路径, 并可能需要手动更新。 如果旧版本无法更新, 请从发布页上下载当前可执行文件 。

<a id="settings-and-backups"></a>
## 设置和备份

配置和校正存储在应用程序文件夹外 :

```text
%APPDATA%\DeckPilot\settings.json
```

退出( L) DeckPilot  在复制此文件以备份或恢复您的配置之前。如果设置文件无法读取, DeckPilot 将副本保留为 `.backup-<date>` 使用新配置前的后缀。

不需要账户。 更新检查联系上 GitHub 并可在接口中禁用 。

<a id="development"></a>
## 发展

安装 **. NET 网络 8 SDK 软件**, 版本 8.0.415 或允许更新的相容版本 `global.json`。视觉工作室 2022 与 .NET 桌面开发工作量是可选的。

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

控制台对第一个失败的断言进行非零状态的退出检查。

- 密钥映射,被控键重复,发布,配置切换,以及动作复制.
- 设置验证、 持久性和旧配置的迁移 。
- 应用程序搜索与目标处理.
- 发布选择,校验和,截取或损坏的下载,重定向,并取消.
-  可执行文件替换、备份保存和回滚。

 文件锁检查只在Windows上运行.

GitHub 动作也运行 `tests/windows-update-smoke.ps1` 相对于独立的可执行文件。它启动更新帮助器,退出原始进程,验证替换和启动确认,检查设置保存,然后通过 UI 自动化打开已安装的应用程序拾取器并保存一个选择。它还练习了更名为可执行文件并包含空格的路径。此脚本保留给可支配的 GitHub 动作测试账户。 CI 也运行 `tests/windows-installer-smoke.ps1`: 真正的程序文件安装,桌面/起步菜单快捷键,默认启动,升级时应用打开,禁用启动保存,不删除配置文件而未安装.

<a id="publish-a-standalone-windows-folder"></a>
### 发布独立的窗口文件夹

```powershell
dotnet publish src/DeckPilot.Windows -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false -o artifacts/win-x64
Compress-Archive -Path artifacts/win-x64/* -DestinationPath artifacts/DeckPilot-win-x64.zip -Force
```

分发完整的归档。 Windows 构建工作流程也会生成此软件包 。

<a id="publish-a-github-release"></a>
### 发布 GitHub 发布

更新应用程序版本并 `.github/release-notes.md`,然后推动匹配 `v*` 标签,例如 `v1.0.0`。发布工作流程检查标签是否匹配项目版本并在发布前运行验证:

| 下载 |  目录 |
| --- | --- |
| `DeckPilot.exe` |  自成一体的,单文件的Windows x64应用程序. |
| `DeckPilot-win-x64.zip` | 完整的自定义应用程序文件夹 。 |
| `SHA256SUMS.txt` | 嘘...256 两次下载都用散列。 |

 对于Windows ARM64,请与 `-r win-arm64` 并验证目标机器。

<a id="hardware-checks-on-windows"></a>
## 对 Windows 的硬件检查

连通了莱昂纳多:

- 全部校正 12 任意的 F13 – F24 命令中的位置。 确认重复的密钥会被拒绝, 并取消保存先前的映射 。
-  在游戏中指定自由键并检查游戏是否正常收到.
- 启动一个已配置的程序, 然后按住按钮 : 它应该发射一次直到发布。 包含包含空格和可选参数的路径 。
-  在普通应用程序中测试一个文件夹,一个网站,一个快捷键,以及媒体控制.
-  通过快捷键动作发送 F13-F24 并确认没有循环 DeckPilot 行动。
- 创建两个有不同动作的配置文件, 在它们之间切换, 退出, 并重新启动。 配置和校正应该持续 。
- 关闭窗口, 触发动作, 然后通过通知区域图标重新打开并退出 。 DeckPilot 出入口,所有密钥应正常通过.
-  再次签名后启用 Windows 启动并测试; 如果不必要, 则在之后禁用 。

<a id="current-limitations"></a>
## 目前的限制

- 密钥监听对 Windows 会话是全局的。 来自另一个键盘的物理 F13– F24 密钥遵循相同的配置。 软件注入的键盘事件被忽略以防止动作循环 。
-  有些游戏,反热系统,或使用直接设备输入的程序,可能仍然收到被截取的密钥. Free-key模式仍然可用. 登录屏幕,安全桌面等会话不在应用程序的控制范围.
- Windows可能屏蔽发送给具有管理员权限运行的应用程序的快捷方式. DeckPilot 运行时没有管理员权限,也不绕过此限制。
-  在启动快捷键动作之前释放 Ctrl, Alt, Shift, 和 Windows 。 Alt+Tab 等组合仍然受 Windows 行为的约束 。
- 断接检测, 自动每局配置, 以及一个签名的安装器是未来的改进。 配置文件目前是手动选择的 。

<a id="project-structure"></a>
## 项目结构

| 路径 | 目的 |
| --- | --- |
| `src/DeckPilot.Core` | 设置, 密钥状态, 搜索, GitHub 发布验证, 经核实的下载, 以及替换交易 。 |
| `src/DeckPilot.Windows` | WPF接口,应用程序目录,图标,Windows动作,通知-区域集成,以及更新帮助器. |
| `tests/DeckPilot.Checks` |  没有外部测试框架的可执行性检查 。 |
| `tests/windows-update-smoke.ps1` | CI使用的原生Windows更新和取用应用程序的烟雾测试. |
| `deckpilotlogo.png` | 应用标志. |
| `deckpilotbanner.png` | 工程标语. |

<a id="license-and-credit"></a>
## 许可证和信贷

DeckPilot 将在 **[MIT 许可证](../../LICENSE)**。您可以使用、复制、修改和再分配,包括商业用途,只要您保留许可证所要求的版权和许可证通知。

 如果你继续发展下去 DeckPilot,请提供少量可见的信用和与该存储库的链接。这些额外信用是可选的;仍需要保存麻省理工学院的通知。

 建议的信贷:

> 依据 [双层助理](https://github.com/lolo34dr/deckpilot) 以 [(原始内容存档于2018-09-21) (英语). Lolo34dr](https://github.com/lolo34dr).

<a id="macropad-firmware-updates"></a>
## Macropad 固件更新

DeckPilot 1.1.0 包含a **Firmware du macropad…** 对话框和固件 1.0.0 支持微型ATmega32U4 **5  第 五 条 国家/领土 16 兆赫( MHz)**,使用所有者上传日志确认的Leonardo/Caterina配置. 捆绑的Windows闪存工具不需要Arduino IDE. 设备选择和确认是强制性的;应用更新从不默默地闪出棋盘. 现有的F13–F24映射,配置和校正仍然相容.

**店主已经确认在应用中成功闪出 固件 1.0.0 身份认证和所有 12 按钮。** 保留原始草图并访问 RST/ GND 以供恢复。 固件闪烁不能自动回滚 。 见 [固件文档](FIRMWARE.zh-CN.md) 用于兼容性、验证、回收、构建步骤和第三方许可证。按下 USB 模式和本地媒体控制不属于此固件发布的一部分。

<a id="building-the-windows-installer"></a>
### 构建 Windows 安装器

生产后 `artifacts/standalone/DeckPilot.exe` 与嵌入的固件捆绑, 安装 Inno 设置 6 运行 `./installer/build.ps1`  从寄存器根。输出为 `artifacts/installer/DeckPilot-Setup.exe`. CI 建立并测试此安装器, 然后再将其附加到发布器中。 安装器使用稳定的应用程序 ID, 所以未来的版本会升级同样的安装 。

<a id="connection-dashboard-and-firmware-checks-130"></a>
## 连接仪表板和固件检查(1.3.0)

密码 **Votre macropad**  卡片显示连接状态、检测到的固件和任何可用的捆绑更新。 **Connexion et diagnostic**  当多个 Leonardos 连接起来时, 显示 USB 链接状态、 物理按钮计数、 固件通知偏好和可复制的诊断摘要 。 诊断不复制配置文件动作或 USB 序列标识符。 显示器在打开固件管理器时重新连接并自动放弃其序列端口 。 F13- F24 键盘动作继续使用固件工作 。 1.0.0  而且没有诊断联系。

启动固件检查是默认启用的。 在应用程序更新检查和校正出局后, 一个已识别的更古老的固件可以启动安装; 延迟提醒 24 小时。未知的棋盘和多个未选中的候选人不会自动被闪出 。 公司软件的发布会与来自 GitHub 的应用程序更新相捆绑, 而不是从独立的公司软件种子中获取 。

固件 **1.1.1** 添加会话识别、 相关心跳响应、 边框消息和物理密钥状态事件。 这些事件激活了映射的按钮并更新了诊断; 它们从未第二次引用动作 。 尽管早先的固件, 它的首次物理板验证仍然是必要的 。 1.0.0 和闪存程序已被所有者确认。 [协议和兼容性文档](FIRMWARE.zh-CN.md).

<a id="keypad-interface-140"></a>
## 键盘接口( E)1.4.0)

 大一点的 3 × 4 keypad 遵循物理封装和黑色密钥盖。每个密钥都显示其主图标、标签、 F13-F24 映射和一个小动作类型的图标; 工具提示保留了完整的标签和位置。 新闻反馈、 运行动作、 成功发送和出错都有不同的外观, 包括符号, 而不仅仅是颜色。 动画遵循了 Windows 动画的偏好 。

选择要编辑的密钥 。 **Options avancées · fichier et arguments** 保存手动应用程序路径, 启动参数默认崩溃, 而必要的文件夹和网站目的地仍然在扩展。 崩溃设置不会清除它们的值 。 **Tester l’action** 保存和发送当前 PC 上选中的动作; 一个成功指示器确认发送, 而不是在另一个应用程序内完成。 键盘快捷键瞄准活动窗口。 免费的 F13– F24 密钥没有测试动作 。

 当宏板断开时, **Hors ligne** 和随附的提示解释,配置仍然可以编辑并测试动作。配置保存在PC上,并在整个断开和重联中保持可用。这些接口改进与现有的固件工作,不需要额外的闪存。

通知区域菜单现在直接切换配置 。 **Ctrl+S 组合键** 保存当前按钮配置。在使用新配置之前,已备份无效的配置文件。保留了 App 安装、桌面/启动菜单图标、启动首选项和现有的自动更新器。

 对于手动Arduino IDE上传, **退出( E) DeckPilot 从其托盘菜单中** 以释放串行端口。关闭主窗口使其在背景中运行。

<a id="spotify-action-search-and-drag-and-drop-150"></a>
## 注意、动作搜索和拖放( U)1.5.0)

**点点** 与 **Multimédia**。它的播放/暂停、下个和先前的命令在 Spotify 桌面应用程序的 Windows 媒体会话中地址, 即使一个浏览器正在播放 YouTube 。 Spotify 必须打开并有可用的播放会话; 必要时启动一次音轨。 不支持在浏览器中Spotify 。 Spotify 会话的丢失或拒绝产生错误, 并且不会回落到全局媒体密钥或另一个播放器。 不需要 Spotify 账户授权、 Premium 订阅或存储令牌 。 DeckPilot窗口 10 版本 2004 或稍后,或 Windows 11,这是该建筑的要求。

使用 **Rechercher une action ou une application** 查询,例如: `volume`, `skip`, `Spotify` 或已安装的应用程序名称。第一次搜索时安装的应用程序索引负载被缓存了5分钟;手动文件选择仍然可用。单击结果可以配置所选的密钥或将其拖到另一个密钥上。

将目录或搜索结果中的动作拖入密钥来指定它。将一个配置的密钥拖入另一个 **互换行动**; 物理校正不变。您也可以降低一个 `.exe`, `.lnk`, `.appref-ms`,文件夹或 HTTP(S) URL 在密钥上。仅将密钥放入其中,而不执行动作。完成编辑器中任何缺失的设置。

Windows 启动目前保留一个明确的每个用户的偏好,修复一个 Stale 或缺失的 Run 条目,并报告已知的 Windows 启动禁用旗。正常的启动会尊重 Windows 禁用; 明确启用 **Lancer avec Windows** 清除已知的 DeckPilot 禁用当前用户的旗子。 背景发射即使在校正前仍然隐藏, 重复背景发射则悄悄退出。 更新后, 打开 **Préférences de l’application**中,检查启动状态,并在Windows屏蔽时重新启动选项。用户个人电脑上的真正签出/签入仍然是对Windows政策或机器特定限制的最终检查。

<a id="sharp-keypad-rendering-profile-recovery-and-audio-controls-161"></a>
## 尖锐的键盘渲染、剖面恢复和音频控制(1.6.1)

键盘现在以本地的WPF 大小而不是通过 Viewbox 缩放文本。 父/ 键阴影效果和文本缩放动画被删除; 窗口滚动比缩放键盘小。 app 宣布了每个监视器DPI 的意识, 并为桌面应用程序使用更大的 Windows shell 图标 。

**Annuler / Rétablir** 恢复配置编辑,包括拖放互换、动作设置、导入和配置创建/删除。历史保存 50  在应用程序会话中编辑。 Ctrl+Z / Ctrl+Y 运行在文本字段外的配置文件上; 文本字段保留自己的撤销/重做。 校正、 启动偏好和执行的动作不会被取消 。

下级 **Gérer les profils**,将当前配置文件或所有配置文件导出为 `.deckpilot.json`,将配置文件导入为新副本,或恢复所有配置文件的备份。导入时从未执行动作,也从未替换 PC 校正。配置文件包含动作路径、参数和音频端点 ID,可能需要在另一个 PC 上调整。未知格式、大小过大的文件和错误的手势配置都被拒绝。

 在保存更改的配置文件之前, DeckPilot 将先前的配置文件保存在 `%APPDATA%\DeckPilot\Backups` (20 版本, 内容复制 。 首选项保存不会创建配置备份。 备份失败无法覆盖当前设置 。 在确认后恢复替换配置文件, 并可以自行撤销; 校正保持不变 。

**Microphone Windows** 切换当前所有激活的 Windows 捕获端点上的哑弹。持续状态栏和 Audio Windows 卡显示活动/被Muded/被混合/无法获取状态,每两秒钟并在应用程序命令后刷新一次。一个失败的多端点更新尝试恢复先前的哑弹状态。这些指标描述了Windows 端点状态,而不是硬件开关或应用程序本身的哑弹按钮。

**Changer de sortie audio**  在Windows控制台、多媒体和通信默认情况下,两个选定的连接输出设备之间的替代设备。一个缺失的目标会出错,而不会选择另一个设备。具有自己固定输出的应用程序可能不会跟随Windows的默认程序。导入的端点ID在适应当前 PC 之前显示为无法使用。失败的路由更改试图恢复先前的默认。

 要配置 **appui court / appui long**,首先选择一个已配置的动作,启用双动作选项,选择用于编辑的手势,并从中设定阈值 200 改为: 3000 ms. 没有这个模式, 动作会保留直接的按键下行为。 有了它, 短动作会在阈值前发布; 长动作会在阈值前运行一次( 如果计时器尚未运行, 则在发布时运行一次 ) 。 长动作也永远不会运行短动作 。 免费的 F13- F24 传出密钥不能使用双动作模式 。 在配置/ 配置变化、 校正、 固件操作、 会话变化和暂停时, 将取消手势 。 不需要固件闪出; 现有的键盘的按键下/ 键报告提供手势 。

核心检查覆盖了重复/释放边界,同时持有,取消,配置历史/交换/备份以及使用假端点的音频回滚. Windows UI检查活动实际拖取/重做,配置长动作和阈值,重新大小后本地键盘尺寸和麦克风指示器. 实际的麦克风驱动程序,耳机/语音切换和物理持有仍然需要验证所有者的PC.

<a id="search-and-ci-reliability-fixes-164"></a>
## 搜索和CI 可靠性修正(1.6.4)

等效的搜索结果保留其现有的 WPF 按钮。 安装的应用程序目录和调试计时器在抓取搜索按钮或拖曳过程中推迟结果替换。 这样可以防止源按钮在互动过程中被删除 。

Windows 烟雾检查继续执行本地鼠标拖放并验证已保存的任务、互换、撤销/重做以及两种按下手势。测试客户端使用物理光标坐标来匹配UI 自动化边框,无论是否为 DPI , 在测量和扫描所有滚动百分数之前, 将焦点放在一起。 回归检查还验证了等同的搜索保留了相同的可访问按钮。 背景启动断言等待 WPF 的装入处理器, 在应用程序存在时需要连续进行五个隐藏窗口的观察。 失败报告阶段、 光标目标、 DPI 和上个滚动几何等。

构建和发布工作流程使用节点.js 24 动作。Arduino 编译器仍然位于 CLI 中。 1.2.2; CI 下载它的 Windows 存档时带有被绑定的 SHA -256 校验和。固件及其工具链版本不变。

 在Windows测试中读取的设置明确允许读取/写入/删除共享并解析UTF-8 关闭读取器后 。 这使得测试本身无法拒绝应用的原子文件替换 。 历史测试在三个倒数/ 重排周期中打开了旧的快照, 并验证在直播设置改变时它是否仍然完好无损 。

 在 Windows 上,已有的设置文件被执行 `File.Replace` 而不是覆盖模式 `File.Move`。这支持在保留完整的旧快照的同时允许删除的阅读器。核心套接字组测试了三个被保存的读取器替换的功能;Windows核心检查在固件编译和发布之前运行,从而及早检测出持久性失败。

测试快照读取器可能等待 500 用于 Windows 共享/ 锁定违反时的 ms `ReplaceFile` 执行。等待仅适用于打开快照,绝不用于执行或重新执行应用程序动作。其他错误、错误的 JSON 和持续锁定仍然是失败。

<a id="runtime-and-editing-optimizations-170"></a>
## 运行时间和编辑优化( E)1.7.0)

保存相同的设置检查当前文件, 但跳过替换和备份工作。 外部编辑/ 删除和失败的写入仍然被检测到 。 `ButtonAction` 具有等值(包括长动作) ; 编辑器脏检查避免了 JSON 序列化 。 历史保存了脱去模型快照, 验证了新的状态, 并避免了 JSON 在撤销/重写时解析 。 50- 编辑限制和 20 仍然保留了配置备份。

搜索和拾取器共享一个不可改变的已安装应用程序目录,时间为5分钟(15 。 同步消费者共享一个扫描; 关闭一个拾取器不会取消另一个消费者。 刷新绕过缓存, 每个拾取器有自己的过滤/视图。 扫描和目录解析运行在 UI 线程上; 拾取器过滤器被解开 120 ms. 动作搜索检查此缓存在解开后和从拾取器返回时重新检查, 因此那里也有一个刷新目录 。

董事会 UI 更新现在跟随连接通知, 而不是每次被唤醒 100 ms. 订购的断开通知仍然取消待定。 键盘钩子队列视频工作, 而压制和手势计时仍然同步 。 隐藏/ 最小化窗口停止反馈动画并使用十秒的音频投票间隔, 而不是两秒; 立即重新打开或发布音频命令请求新鲜状态 。 待发音频刷新共享一个循环, 包括在现有读取时收到的请求 。 未更改的音频快照和被冻结的刷新避免了重复的 UI 分配 。 通过启动一个应用返回的处理包装器会被处理而不关闭启动的程序 。

 这些更改会减少指定的工作;它们不会建立机器独立的CPU/RAM百分比. 原生硬件音频切换和物理密钥行为仍需要用宏板在Windows PC上进行验证.

<a id="workspace-tools-and-refreshed-interface-180"></a>
## 工作空间工具和刷新界面 (中文(简体) ).1.8.0)

木炭接口将动作库放在第一位, 将配置控件与简洁的本地大小区分开来 3 × 4 keypad, 和卡片中的组合动作设置。 密钥重音区分了自己的类别; 选择、 键盘焦点和动作反馈仍然分开 。 **Réduire les animations**  在首选项中补充 Windows 动画设置。较小的窗口保留滚动访问,而不是缩小密钥文本。

- **配置模板 :** 下级 **Gérer les profils**,添加音乐和音频、桌面和生产率,或创建。每个模板创建单独的可编辑配置文件,从不执行它的行动。保留现有的配置文件及物理校正。键盘快捷键使用活动应用程序,其约束可能有所不同。
- **配置密钥 :** 指定 **Changer de profil**,然后选择一个命名的配置文件或向后/向后循环。在导入一个集或复制一个自我引用配置文件时,在配置文件之间重新映射引用。缺失的目的地报告错误,可以在编辑器中替换。
- **视窗命令 :** 使用情况 **Commandes Windows** 打开普通设置或任务管理器,或明确锁定个人计算机。命令来自一个固定的列表,没有脚本可以输入。锁定动作需要签入Windows。
- **完整密钥剪贴板 :** 右键单击一个键复制、剪切、粘贴或重置, 包括长动作和重音。 Ctrl+C/ X/V 和删除工作, 而甲板键有键盘焦点; 普通文本字段保留其编辑快捷键。 密钥剪贴板留在里面 DeckPilot 。这些编辑支持撤销/重做。
- **快捷键记录器 :** 选择键盘动作并单击 **Enregistrer un raccourci**。按一个支持的组合,然后保存它。退出取消;F13-F24在录制时保留给宏板。Windows保留组合仍然需要手动控制。
- **快速访问 :** Ctrl+K或库按钮聚焦动作搜索. **Activité récente** 保留最后一个 30 发送/ 失败此会话的内存动作, 不存储程序参数或目标路径。 它可以随时清除。 成功发送并不能保证目标应用程序的完成 。

坚固的遗物 **1.1.1**。接受自动固件提议后,现在正确打开了固件管理器;以前的模式守护阻止了这种交接。现有的固件身份、心跳、重新连接和闪存检查仍然有效。新的工作空间特性不需要固件闪存。

校验涵盖遗留设置,口音验证,剖析导航和参考重映射,可移植模板,本地Windows模板/undo/redo和剖析切换,快捷键记录,复制/粘贴/重置,设置持续,安装/更新,布局再大小和实际的UI捕获. 硬件特定行为仍然需要在Windows PC上使用宏编程和音频设备.
