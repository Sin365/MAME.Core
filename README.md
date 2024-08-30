## MAME.Core

一个MAME模拟器核心的现代化.Net版本移植作为独立模拟器核心（但是.NetStandard 2.0)

移植的目的是，便于用于Unity Godot Stride MonoGame FNA 等跨平台的游戏引擎，并运行在不同平台。

MAME.Core 接口化的核心。算一个真正的泛用型.Net/mono 任意生态版本 可用的MAME核心库。性能卓越。

并且，最终会接入HaoYueNet高性能网络库

[HaoYueNet-Github](https://github.com/Sin365/HaoYueNet "HaoYueNet-Github")

自己开发联机功能。

此外，我自己也开发了Relpay功能，用于随时保存和回看之前的游玩回放。

基于我自己的AxiRelpay

[AxiReplay-Github](https://github.com/Sin365/AxiReplay "AxiReplay-Github")

## MAME.Unity 项目说明

是基于MAME.Core，在Unity中的实现，

如上所说，加上对色彩通道和精度的合理对齐，视频非常高效的显示。音频也处理得较为完备。

PC和Android，已经就比较好的实现。

目前有一个简易的游戏选择器，Android制作了虚拟按键。

下一步是接入PSV，PS3等游戏机，往跨平台联机靠拢。

## MAME.Core 项目说明

原项目将MAME C++翻译为C#的一个Windows下的WinForm项目，

高度依赖Windows、WinForm、Win32API（Tick）、System.Drawing、Bitmap，DirectX音/视频

有些静态变量是放在Form做GDI，gdi的扩展类里都是Bitmap用unsafe处理指针来填充RGBA。

本项目(MAME.Core)中 **已经完全脱离如上依赖** ，并将一切标准化，去掉和整理好一切不规范的部分。

是一个标准的.Net Standard 2.0 库。无障碍跨平台。

接口抽象化：IKeyboard、ILog、IMouse、IResources、ISoundPlayer、IVideoPlayer

抽象枚举：MotionKey

资源加载，设置，按键映射，音视频接入，可以自由自定义接入任何C#生态的平台。

btw，总所周知街机游戏，已经有类似现代的场景和摄像机的概念，

比如CPS1 实际逻辑绘制的画布场景是512x512，实际显示区域（类似摄像机概念）只有其中384*224

在完美显示必要内容时，不再使用Clone的方式重写，而是像素矩阵绘制时，那么我们需要扣出中间那部分。

最高效率的数据读取设计模式，就是直接不拷贝。

MAME.Core中，通过计算的方式，仅处理显示区域的颜色。

并在提交颜色int[]时，额外提供了GC固定维护的安全Ptr（非unsafe），甚至可以做到显示GPU提交直通。

即，模拟器核心中的颜色缓冲的指针引用直接提交给外部GPU DrawCall，如Unity接入可以用于直接映射Texture

**效率极佳**，没有新的内存对象，无GC。

## References

shunninghuang - https://www.codeproject.com/Articles/1275365/The-Main-Architecture-of-MAME-NET

MAME-Multiple Arcade Machine Emulator - https://github.com/mamedev

MSDN - https://msdn.microsoft.com

BizHawk M68000 and Z80 code - https://github.com/TASEmulators/BizHawk/tree/master/src/BizHawk.Emulation.Cores/CPUs

VCMAME detail by Bryan McPhail - https://www.codeproject.com/Articles/4923/VCMAME-Multiple-Arcade-Machine-Emulator-for-Visual

MAME and MAMEUI Visual C Project Files - http://www.mikesarcade.com/arcade/vcmame.html

CPS1.NET - https://www.codeproject.com/Articles/998595/CPS1-NET-A-Csharp-Based-CPS1-MAME-Emulator