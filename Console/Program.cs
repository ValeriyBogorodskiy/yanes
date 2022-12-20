using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using YaNes.Joypads;
using YaNes.PPU;
using YaNES.Console;
using YaNES.Core;
using YaNES.Core.Utils;
using YaNES.CPU;
using YaNES.ROM;

var context = new Context("../../../../PacMan.nes");
var frameRate = 60;
var nesScreenDimensions = new Vector2i(256, 240);
var renderingImage = new RenderingImage(nesScreenDimensions.X, nesScreenDimensions.Y);
var screenScalingFactor = 4;
var ppuScanlines = 262;
var scanlineCyclesDuration = 341;
var ppuCyclesPerFrame = ppuScanlines * scanlineCyclesDuration;

using (GameWindow2D yanesWindow = new(frameRate, nesScreenDimensions, screenScalingFactor))
{
    yanesWindow.UpdateFrame += args => OnUpdateFrame(yanesWindow);
    yanesWindow.KeyDown += args => OnKeyDown(args);
    yanesWindow.KeyUp += args => OnKeyUp(args);

    yanesWindow.Run();
}

void OnUpdateFrame(GameWindow2D yanesWindow)
{
    var ppuCyclesPerformed = 0;

    while (ppuCyclesPerformed < ppuCyclesPerFrame)
    {
        var cpuReport = context.Cpu.ExecuteNextInstruction();
        var cpuCyclesTaken = cpuReport.Cycles;
        var ppuCyclesBudget = cpuCyclesTaken * 3;

        for (int i = 0; i < ppuCyclesBudget; i++)
        {
            var visibleScanline = context.Ppu.Scanline < 240;
            var visiblePixel = context.Ppu.ScanlineCycle < 256;

            if (visibleScanline && visiblePixel)
                DrawPixel(context.Ppu.ScanlineCycle, context.Ppu.Scanline);

            context.Ppu.Update(1);
        }

        ppuCyclesPerformed += ppuCyclesBudget;
    }

    yanesWindow.SetImage(renderingImage.Pixels);
}

void OnKeyDown(KeyboardKeyEventArgs args)
{
    var joypadButton = MapKeyToJoypadButton(args.Key);

    if (joypadButton != JoypadButton.None)
        context.Joypads[0].SetButtonPressed(joypadButton, true);
}

// TODO : read mapping from file
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
    renderingImage.SetPixel(x, y, color[0], color[1], color[2]);
}

class Context
{
    public Rom Rom { get; }
    public Cpu Cpu { get; }
    public Ppu Ppu { get; }
    public Joypad[] Joypads { get; }

    public Context(string romPath)
    {
        Rom = RomParser.FromFile(romPath);
        Cpu = new Cpu(new CpuSettings { StartingProgramAddress = 0xC033, InitialProcessorStatus = 0x24 });
        Ppu = new Ppu();
        Joypads = new[] { new Joypad(), new Joypad() };

        Ppu.AttachInterruptsListener(Cpu);
        Ppu.AttachRom(Rom);

        Cpu.Bus.AttachPpu(Ppu);
        Cpu.Bus.AttachJoypads(Joypads);
        Cpu.InsertCartridge(Rom);
    }
}