> 这是完整固件文档的自动翻译。 英文原件是参考文献。 命令和标识符是准确保存的 。

<a id="deckpilot-firmware"></a>
# DeckPilot 固态软件

仅用于测试的硬件配置 : **Pro Micro ATmega32U4, 互联网档案馆的存檔,存档日期2013-03-02. 5 第 五 条 国 家 16 兆赫( MHz)**,使用 **阿尔杜伊诺·莱昂纳多** 董事会简介和a **卡特琳娜/AVR109** 启动器。 USB- C 描述连接器, 而非棋盘兼容性。 主人的原始草图已成功上传 。 1200- 鲍德重置和AVR109号机 57600 Baud. 车主也确认成功在应用中闪烁,固件 1.0.0 所有人的身份识别和运行 12 引用板上的按钮。

<a id="wiring-and-behavior"></a>
## 电线和行为

每个开关连接一个 Pin 到 GND 。 启用内部拉动 。 Pins **2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 15, 16** 发送 **F13通过F24** 依此顺序,一个稳定 30 ms debounce hands 独立地按下并发布. 键盘行为, Windows 侧配置和校准仍然兼容; USB 键盘翻转仍然受到Arduino 键盘库的限制. 固件尚未引入按键 USB 模式或本地媒体控制 。

键 **1** 在启动时为 GND 所持有会禁用键盘操作, 保存原始的故障安全性 。 USB 核心仍然可以支持启动器的输入 。 请不要将 VCC 连接到 GND 。

当前 USB CDC 序列界面的答案 `DECKPILOT?\n` 时间: 115200 baud 与 :

```
DECKPILOT|1|promicro-32u4-16mhz|1.1.1
```

正常键盘操作不需要序列连接。 身份是兼容性检查, 而不是加密硬件认证。 Windows 仍然报告 Arduino Leonardo USB 标识符; 许多其他的棋盘共享这些标识符 。

<a id="install-from-deckpilot"></a>
## 安装从 DeckPilot

1. 保留所提供的复原草图(`firmware/Original/Original.ino`)和工作中的Arduino上传设置作为恢复选项。保持 RST 和 GND 的物理可访问性。
2. 关闭Arduino IDE,串行监视器和其他串行工具. 断开其他Leonardo板.
3. 打开 **Firmware du macropad…**中,选择正确的设备,确认板和线条。只有在明确确认后,才能安装未识别的原始草图。
4. 确认安装并让 USB 电缆连接到完成。 键盘动作中止于 DeckPilot 而固件对话框是打开的;普通的F13–F24事件仍然可以到达Windows.

固件和视窗 AVRDUDE 8.0 被捆绑在已发布的应用中。没有Arduino IDE或单独的驱动程序安装由 DeckPilot。 Windows 已经曝光了棋盘的 USB 串行端口, 就像它对于所有者成功上传一样。 新的固件版本会用 GitHub 的应用程序更新来提供; 更新应用程序确实如此 **没有** 自动闪存硬件。每个闪存需要确认 。

DeckPilot 爱特利埃保留 **点菜精品** 需要输入。 `EFFACER`全部清除 28 KiB 的应用程序闪存, 有经过验证的重置循环, 并停止 F13 – F24 和正常的 USB 计数。 Caterina 仍然可用, 但是卡片在手动放入启动器模式之前不会在 Atelier 中再次出现 。 保持连接, 短暂连接 **转写** 改为: **两次快速的GND**,然后选择其临时 Arduino Leonardo / rootloader COM 端口,并使用 Atelier 的回收动作重新安装 DeckPilot。 Atelier 显示这些指令,并在擦除或中断的闪存后保持恢复功能。启动器和EEPROM 保存下来;这不是全芯片擦除。配置数据保留在PC上。

DeckPilot 检查SHA -256,英特尔HEX记录校验和,重置飞行器存在,重叠和 **28672- 字节** 重置前的应用程序边界。它匹配运行时的物理 USB 位置和启动器端口,使用 AVR109 。 57600 baud, 需要 AVRDUDE 的书写验证, 然后检查运行的固件的身份和版本。 它不写引信、 EEPROM 或 启动器。 只有 Leonardo IDs 。 `2341:8036` / `2A03:8036` 和卡特琳娜岛 `2341:0036` / `2A03:0036` 如果 Windows 无法提供匹配的物理连接,闪存会被屏蔽而不是猜测端口。

闪烁不是原子: 电缆断开或断电可以使应用程序固件不全。 没有自动固件备份或回滚。 如有必要, 用 Arduino IDE 设置重新装入原始草图 。 **阿尔杜伊诺·莱昂纳多**。根据棋盘的启动器,两次将 RST 与 GND 短暂连接可能会暴露一个临时启动器端口; 当该端口可用时上传。 损坏的启动器需要ISP 硬件, 无法由这个 USB 更新器修复。 请不要写入一个不同的启动器或时钟变体 。

主应用程序上一个完成的 AVRDUDE 日志存储于 `%LOCALAPPDATA%\DeckPilot\Firmware\last-flash.log`Atelier 继续安装和重置日志 `%LOCALAPPDATA%\DeckPilotFactory\last-flash.log` 财务报告和财务报告 `last-reset.log`.

<a id="build"></a>
## 构建

在Windows上,从寄存器根安装Arduino CLI **1.2.2** 和.NET SDK 8,然后运行 :

```powershell
./firmware/tools/build.ps1
dotnet build DeckPilot.sln -c Release -p:RequireFirmwareBundle=true
dotnet run --project tests/DeckPilot.Checks -c Release --no-build
```

脚本钉上Arduino AVR 核心 **1.8.6**键盘 **1.0.6**,和上游 AVRDUDE **8.0**. CLI管理软件包使用Arduino的软件包校验和验证;AVRDUDE二进制/源文件档案有明确的SHA-.256 在剧本中标注。 `artifacts/firmware/DeckPilot-firmware.zip` 包含 DeckPilot HEX,生成的应用程序区域重置HEX及其生成器,校验和,Windows工具,许可证和相应的上游AVRDUDE源归档. Publishing CI需要捆绑;普通开发商没有它就可以运行应用程序但不能闪烁或重置一个板.

原画 12- 扣子素描改自大卫·麦迪逊的 **流转便宜** 版权 2018麻省理工学院,其通知仍在 `DeckPilot.ino`. DeckPilot 添加使用寄存器MIT许可证. **AVRDUDE是GPL-下的单独软件.2.0- 或后来**,以其通知/许可证和来源捆绑;存储器MIT许可证不取代该许可证。 https://github.com/avrdudes/avrdude/tree/v8.0 用于上游建设指令和依赖性通知。

<a id="firmware-111-and-usb-protocol-2"></a>
## 固件 1.1.1 和USB协议 2

固件 1.1.1 增加只读的诊断链接,同时保留相同的F13-F24键盘映射和pin-1 启动故障安全 。 **车主的物理测试覆盖了坚固的软件 1.0.0. 固件 1.1.1 拥有自动协议测试和AVR构建,但仍需要在实际板上进行第一次测试.** 配置和校准仍然保留在PC上,不需要为这个固件更新重新创建.

新应用程序识别连接的卡片,保持其序列端口的开放,解插后重新连接,并在启动和重联后检查其固件版本. Firmware更新与应用发布: 启动检查将连接的固件与已安装的应用中包含的已验证的捆绑进行比较. App更新检查仍然从 GitHub 获得更新的释放. 没有任何单独的固件仅限下载通道.

每一次应用会话中检测到的较旧的固件提示,其中 24- 延迟时提醒延迟。通知可禁用 **Connexion et diagnostic**。未知的Arduino草图、断开的设备以及模棱两可的多卡选择不会触发自动闪烁或更新提示。固件安装总是需要确认。打开固件管理器会暂停显示器并等待序列端口的发布;在对话框关闭后会恢复监测。

**手动从Arduino IDE 上传, 退出 DeckPilot 首先从它的托盘图标。** 关闭主窗口只隐藏它,所以它继续拥有串行连接. DeckPilot 和另一个串行显示器不能同时使用同一端口。如果另一个工具已经拥有端口,应用程序会报告问题并重试而不影响普通键盘键。

新的计划 `DeckPilotProtocol.h` 由下列机构执行的相同执行: `tests/firmware-protocol.cpp`。它使用固定大小的缓冲器、边框输入处理和可用的 USB 传输空间; 在等待主机时, 按钮扫描永远不会被故意延迟。 最新的等待的按钮掩码是在后压下连接起来的。 没有命令执行宏、 写入 EEPROM、 禁用键盘、 修改配置文件或此诊断协议上的闪亮固件 。

时 115200 baud, 以 LF( CRLF 被接受) 结尾的行。 会话符号中包含整整八个大写小数十六进制字符。 所有数字都是未签名小数。 例如 :

```
PC:    DECKPILOT?
Board: DECKPILOT|1|promicro-32u4-16mhz|1.1.1
PC:    DP2|HELLO|1234ABCD
Board: DP2|INFO|1234ABCD|promicro-32u4-16mhz|1.1.1|12|3
PC:    DP2|PING|1234ABCD|7
Board: DP2|PONG|1234ABCD|7|42000|9|2049
Board: DP2|KEYS|1234ABCD|10|1
```

`INFO` 广告 12 键和容量位数 : 1 =心跳/状态快照, 2 = 关键状态事件。 `PONG` 响应请求计数器,然后以毫秒、状态序列和 12- 按钮口罩 `KEYS` 报告序列和掩码; 位 0 是 F13 和 位点 11 是 F24, 独立于物理校准。 会话和请求计数器拒绝 stale 回复。序列比较处理 32-bit raparound. 密钥只保留诊断数据:应用程序动作仍然通过现有的键盘路径运行一次,所以一个串行事件不能执行同样的动作两次.

Heartbeat 请求通常每两秒发送一次。 如果缺少匹配回复, 应用程序会重新连接 。 1.5 秒; 固件在6秒后无心跳或当串行连接关闭时停止主动诊断。 1.0.0 继续通过遗留身份查询得到支持,没有活的关键诊断。 DeckPilot 应用程序仍然可以读取固件的遗留身份 1.1.1.

来源版本在 `DeckPilotProtocol.h` 提供发布清单版本。现在已出版的固件包中也包含当前草图/标题。 `source/DeckPilot`,恢复草图和 DeckPilot 麻省理工学院执照

诊断链接有意使用 `Serial.dtr()` 而不是说 `bool(Serial)`: Arduino AVR 核心 : 1.8.6 插入一个 10 ms 延迟 `Serial_::operator bool()`。读取 DTR 直接使诊断路径不会在每次密钥扫描周期中添加暂停。主机协议测试删除隐含的 bul 转换,因此无法在共享协议执行中忽略此阻断模式 。
