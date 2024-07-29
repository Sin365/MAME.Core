# MAME.Core

一个MAME模拟器核心的现代化.Net版本移植作为独立模拟器核心（但是.NetStandard 2.0)

移植的目的是，便于用于Unity Godot Stride MonoGame FNA 等跨平台的游戏引擎，并运行在不同平台

所以要脱离WinForm或者说Windows。
目前大部分已经移植，原始有些静态变量是放在Form做GDI。这些我基本已经解耦
然后把Mouse Keyboard Input Video Sound接口化了，不依赖DX的
变成一个MAME模拟器核心类库。并尝开发联机功能

原项目gdi的扩展类里都是Bitmap用unsafe处理指针来填充RGBA
这部分我已经做到脱离System.Drawing(脱离平台依赖)，重写BitmapData的逻辑，使其可以跨平台

git.axibug.com/sin365/MAME.Core
总之 ，研究过程很欢乐。

# References

shunninghuang - https://www.codeproject.com/Articles/1275365/The-Main-Architecture-of-MAME-NET

MAME-Multiple Arcade Machine Emulator - https://github.com/mamedev

MSDN - https://msdn.microsoft.com

BizHawk M68000 and Z80 code - https://github.com/TASEmulators/BizHawk/tree/master/src/BizHawk.Emulation.Cores/CPUs

VCMAME detail by Bryan McPhail - https://www.codeproject.com/Articles/4923/VCMAME-Multiple-Arcade-Machine-Emulator-for-Visual

MAME and MAMEUI Visual C Project Files - http://www.mikesarcade.com/arcade/vcmame.html

CPS1.NET - https://www.codeproject.com/Articles/998595/CPS1-NET-A-Csharp-Based-CPS1-MAME-Emulator