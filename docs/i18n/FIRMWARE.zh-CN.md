> ▁这是完整固件文档的自动翻译。 英文原件是参考文献。 命令和标识符是准确保存的 。

<a id="deckpilot-firmware"></a>
# DeckPilot 固态软件

仅用于测试的硬件配置 : **Pro Micro ATmega32U4, 互联网档案馆的存檔,存档日期2013-12-02. 5 ▁第▁五 条 国家/领土 16 兆赫( MHz)**,使用 **▁阿杜伊诺·莱昂纳多** 董事会简介和a **▁凯特琳娜/AVR109** 启动器。 USB- C 描述连接器, 而不是棋盘相容性。 主人的原始草图已成功上传 。 1200- 鲍德重置和AVR109,地点: 57600 Baud. 车主也确认成功应用闪光,固件 1.0.0 所有人的身份识别和运行 12 引用板上的按钮。

<a id="wiring-and-behavior"></a>
## 线和行为

▁每个开关都连接到 GND 。 启用内部拉起。 Pins **2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 15, 16** 发送 **F13通过F24** 依此顺序,一个稳定 30 ms debounce hands 独立地按下并发布. 键盘行为, Windows 侧配置和校正仍然兼容; USB 键盘翻转仍然受Arduino 键盘库的限制. 固件尚未引入按键 USB 模式或本地媒体控制 。

键 **1** ▁在启动时为 GND 所持有会禁用键盘操作, 保留原有的故障安全性 。 USB 核心仍然可以支持启动器的输入 。▁请不要将 VCC 连接到 GND 。

▁当前 USB CDC 串行界面的答案 `DECKPILOT?\n` 时间: 115200 与下列机构有联系:

```
DECKPILOT|1|promicro-32u4-16mhz|1.1.1
```

正常键盘操作不需要串行连接。 身份是兼容性检查, 而不是加密硬件认证。 Windows 仍然报告 Arduino Leonardo USB 标识符; 许多其他的棋盘共享这些标识符 。

<a id="install-from-deckpilot"></a>
## 安装从 DeckPilot

1. 保留所提供的复原草图(`firmware/Original/Original.ino`)和工作化的Arduino上传设置作为恢复选项。保持 RST 和 GND 的物理可访问性。
2. 关闭Arduino IDE,串行监视器等串行工具. 断开其他Leonardo板.
3. 打开 **Firmware du macropad…**,选择正确的设备,确认板和接线。一个未识别的原始草图只有在明确确认后才能被安装。
4. ▁确认安装并离开 USB 电缆连接直至完成。 键盘动作中止于 DeckPilot ▁而固件对话框是打开的;普通的F13-F24事件仍然可以到达Windows.

固件和视窗 AVRDUDE 8.0 被捆绑在已发布的应用中。没有Arduino IDE或单独的驱动程序安装由 DeckPilot。 Windows▁必须已经曝光了董事会的 USB 串行端口,▁就像它为所有者成功上传所做的那样。 新的固件版本会随应用程序更新从 GitHub 发送; 更新应用程序确实 **▁没有** 自动闪存硬件。每个闪存需要确认 。

DeckPilot 检查SHA -256,英特尔HEX记录校验和,重置-飞行器存在,重叠和 **28672- 字节** 重置前的应用程序边界。▁它匹配运行时间和启动器端口的物理 USB 位置, 使用 AVR109 。 57600 baud, 需要 AVRDUDE 的书写验证,▁然后检查运行的固件的身份和版本。▁它不写引信、 EEPROM 或启动器。▁只有 Leonardo IDs `2341:8036` / `2A03:8036` 和卡特琳娜岛 `2341:0036` / `2A03:0036` 。如果 Windows 无法提供匹配的物理连接,闪存会被屏蔽,而不是猜测一个端口。

闪光不是原子: 电缆断开或断电可以使应用程序固件不全。▁没有自动固件备份或回滚。 如有必要,▁用 Arduino IDE 设置重新装入原始草图 。 **▁阿杜伊诺·莱昂纳多**。根据棋盘的后置器,两次将 RST 与 GND 短暂连接可能会暴露一个临时后置器端口;▁当该端口可用时, 上传。 损坏后后置器需要ISP 硬件, 无法由此 USB 更新器修复 。▁请不要写入不同的后置板或时钟变体 。

最后一个完成的 AVRDUDE 进程日志存储于 `%LOCALAPPDATA%\DeckPilot\Firmware\last-flash.log`.

<a id="build"></a>
## 构建

▁在 Windows▁从寄存器根安装 Arduino CMI **1.2.2** 和.NET SDK 8,然后运行 :

```powershell
./firmware/tools/build.ps1
dotnet build DeckPilot.sln -c Release -p:RequireFirmwareBundle=true
dotnet run --project tests/DeckPilot.Checks -c Release --no-build
```

剧本 : Arduino AVR 核心 **1.8.6**键盘 **1.0.6**,和上游 AVRDUDE **8.0**. CLI管理软件包使用Arduino的软件包校验和验证;AVRDUDE▁二进制/源文件档案有明确的SHA-.256 脚本中指针。 `artifacts/firmware/DeckPilot-firmware.zip` 包含 HEX, express, Windows 工具, 许可证和相应的上游 AVRDUDE 源归档. Publishing CI 需要捆绑;▁一个普通的开发者构建,▁没有它就可以运行应用程序但不能闪烁.

原画 12- 扣子素描改自大卫·麦迪逊 **流出廉价** 版权项目 2018麻省理工学院,其通知仍在 `DeckPilot.ino`. DeckPilot 添加使用寄存器MIT许可证. **AVRDUDE是GPL-下的单独软件.2.0- 或后来**,其通知/许可证和来源捆绑;存储器MIT许可证不取代该许可证。 https://github.com/avrdudes/avrdude/tree/v8.0 用于上游建设指令和依赖性通知。

<a id="firmware-111-and-usb-protocol-2"></a>
## 固件 1.1.1 和 USB 协议 2

固件 1.1.1 ▁在保留相同的F13-F24键盘映射和平针的同时,添加只读的诊断链接-1 启动故障安全 。 **店主的体能测试覆盖了坚固的软件 1.0.0. 固件 1.1.1 拥有自动协议测试和AVR构建,但仍需要其首次在实际板上进行测试.** 配置和校正仍然保留在PC上,不需要为这个固件更新而重新创建.

▁新应用程序识别了连接的卡片,保持其序列端口的开通,解接后重新连接,并在启动时和重联后检查其固件版本. Firmware更新随应用程序发布而来: 启动检查将连接的固件与已安装的应用程序中包含的已验证的捆绑进行对比. App更新检查仍然从 GitHub 获得更新的发布.▁没有单独的固件仅限下载通道.

检测到的更古老的固件提示每一次应用会话,其中 24- 延迟时提醒延迟。通知可禁用 **Connexion et diagnostic**。未知的Arduino草图、断开的设备以及模糊的多卡选择不会触发自动闪出或更新提示。固件安装总是需要确认。打开固件管理器会暂停显示器并等待序列端口的发布;在对话框关闭后会恢复监测。

**▁从Arduino IDE 手动上传一个内容, 退出 DeckPilot ▁首先从它的托盘图标。** 关闭主窗口只隐藏它,所以它继续拥有串行连接. DeckPilot 和另一个串行显示器不能同时使用同一端口。如果另一个工具已经拥有端口,应用程序会报告问题并重试而不影响普通键盘键。

新的计划 `DeckPilotProtocol.h` 由下列人员执行的相同: `tests/firmware-protocol.cpp`。它使用固定大小的缓冲器、边框输入处理和可用的 USB 传输空间;▁在等待主机时, 按钮扫描永远不会被故意延迟。 最新的待处理按钮掩码是在后压下连接起来的。▁没有命令执行宏、 写入 EEPROM、 禁用键盘、 修改配置文件或此诊断协议上的闪光固件 。

时 115200 baud, 以 LF( CRLF 被接受)▁为结尾的行。▁会话符号包含整整8个大写小数十六进制字符。▁所有数字都是未签名小数。▁例如 :

```
PC:    DECKPILOT?
Board: DECKPILOT|1|promicro-32u4-16mhz|1.1.1
PC:    DP2|HELLO|1234ABCD
Board: DP2|INFO|1234ABCD|promicro-32u4-16mhz|1.1.1|12|3
PC:    DP2|PING|1234ABCD|7
Board: DP2|PONG|1234ABCD|7|42000|9|2049
Board: DP2|KEYS|1234ABCD|10|1
```

`INFO` 广告 12 键和容量位数 : 1 =心跳/状态快照, 2 = 关键状态事件。 `PONG` 响应请求的计数器,然后以毫秒、状态序列和 12- 按钮口罩 `KEYS` 报告序列和遮罩; 位 0 ▁是 F13 和 位点 11 ▁是 F24, 独立于物理校正。▁会话和请求计数器拒绝▁stale 答复。序列比较处理 32-bit slaparound. 密钥只保留诊断数据:应用程序动作仍然通过已存在的键盘路径运行一次,所以一个串行事件不能两次执行相同的动作.

Heartbeat 请求通常每两秒钟发送一次。▁如果缺少匹配回复, 应用程序会重新连接 。 1.5 秒; 固件在无心跳或串行连接关闭6秒后停止主动诊断。 1.0.0 继续通过遗留身份查询得到支持,没有活的关键诊断。 DeckPilot 应用程序仍然可以读取固件的遗留身份 1.1.1.

源版本为 `DeckPilotProtocol.h` 提供发布清单版本。现在已出版的固件包中也包含当前草图/标题。 `source/DeckPilot`、复原草图和 DeckPilot 麻省理工学院执照.

诊断链接有意使用 `Serial.dtr()` 而不是说 `bool(Serial)`: Arduino AVR 核心设备 1.8.6 插入a 10 ▁ms 延迟 `Serial_::operator bool()`。读取 DTR 直接使诊断路径不会添加暂停到每个密钥扫描周期。主机协议测试删除了隐含的 bul 转换,因此无法在共享协议执行中重新使用此阻断模式 。
