using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using YaNes.Joypads;
using YaNes.PPU;
using YaNES.Console;
using YaNES.Core;
using YaNES.Core.Utils;
using YaNES.CPU;
using YaNES.ROM;

var screenScale = 4;
// TODO : read from file
var pathToRom = "../../../../PacMan.nes";
var context = new Context(pathToRom);
var renderBuffer = new RenderBuffer(Constants.Nes.ScreenWidth, Constants.Nes.ScreenHeight, false, true);
var nesScreenDimensions = new Vector2i(Constants.Nes.ScreenWidth, Constants.Nes.ScreenHeight);

using (GameWindow2D yanesWindow = new(Constants.Nes.FramesPerSecond, nesScreenDimensions, screenScale))
{
    yanesWindow.Title = "YaNES";
    yanesWindow.UpdateFrame += args => OnUpdateFrame(yanesWindow);
    yanesWindow.KeyDown += args => OnKeyDown(args);
    yanesWindow.KeyUp += args => OnKeyUp(args);

    yanesWindow.Run();
}

void OnUpdateFrame(GameWindow2D yanesWindow)
{
    var ppuCyclesPerformed = 0;

    while (ppuCyclesPerformed < Constants.Ppu.CyclesPerFrame)
    {
        var cpuReport = context.Cpu.ExecuteNextInstruction();
        var ppuCyclesBudget = cpuReport.Cycles * Constants.Ppu.CyclesPerCpuCycle;

        for (var i = 0; i < ppuCyclesBudget; i++)
        {
            if (context.Ppu.Scanline < Constants.Nes.ScreenHeight && context.Ppu.ScanlineCycle < Constants.Nes.ScreenWidth)
                DrawPixel(context.Ppu.ScanlineCycle, context.Ppu.Scanline);

            context.Ppu.Update(1);
        }

        ppuCyclesPerformed += ppuCyclesBudget;
    }

    yanesWindow.SetImage(renderBuffer.Pixels);
}

void OnKeyDown(KeyboardKeyEventArgs args)
{
    var joypadButton = MapKeyToJoypadButton(args.Key);

    if (joypadButton != JoypadButton.None)
        context.Joypads[0].SetButtonPressed(joypadButton, true);
}

// TODO : read from file
JoypadButton MapKeyToJoypadButton(OpenTK.Windowing.GraphicsLibraryFramework.Keys key)
{
    return key switch
    {
        OpenTK.Windowing.GraphicsLibraryFramework.Keys.W => JoypadButton.Up,
        OpenTK.Windowing.GraphicsLibraryFramework.Keys.A => JoypadButton.Left,
        OpenTK.Windowing.GraphicsLibraryFramework.Keys.S => JoypadButton.Down,
        OpenTK.Windowing.GraphicsLibraryFramework.Keys.D => JoypadButton.Right,
        OpenTK.Windowing.GraphicsLibraryFramework.Keys.Left => JoypadButton.ButtonA,
        OpenTK.Windowing.GraphicsLibraryFramework.Keys.Right => JoypadButton.ButtonB,
        OpenTK.Windowing.GraphicsLibraryFramework.Keys.Enter => JoypadButton.Start,
        OpenTK.Windowing.GraphicsLibraryFramework.Keys.RightShift => JoypadButton.Select,
        _ => JoypadButton.None
    };
}

void OnKeyUp(KeyboardKeyEventArgs args)
{
    var joypadButton = MapKeyToJoypadButton(args.Key);

    if (joypadButton != JoypadButton.None)
        context.Joypads[0].SetButtonPressed(joypadButton, false);
}

void DrawPixel(int x, int y)
{
    var color = context.Ppu.GetPixelColor(x, y);
    renderBuffer.SetPixel(x, y, color[0], color[1], color[2]);
}

class Context
{
    public IRom Rom { get; }
    public ICpu Cpu { get; }
    public IPpu Ppu { get; }
    public IJoypad[] Joypads { get; }

    public Context(string romPath)
    {
        Rom = RomParser.FromFile(romPath);
        Cpu = new Cpu(new CpuSettings { StartingProgramAddress = 0xC033, InitialProcessorStatus = 0x24 });
        Ppu = new Ppu();
        Joypads = new[] { new Joypad(), new Joypad() };

        Ppu.AttachInterruptsListener(Cpu.InterruptsListener);
        Ppu.AttachRom(Rom);

        Cpu.Bus.AttachPpu(Ppu);
        Cpu.Bus.AttachJoypads(Joypads);
        Cpu.InsertCartridge(Rom);
    }
}