using YaNes.PPU;
using YaNES.CPU;
using YaNES.ROM;
using YaNES.Console;
using OpenTK.Mathematics;
using YaNES.Core.Utils;

var context = new Context("../../../../SuperMarioBros.nes");
var frameRate = 60;
var nesScreenDimensions = new Vector2i(256, 240);
var renderingImage = new RenderingImage(nesScreenDimensions.X, nesScreenDimensions.Y);
var screenScalingFactor = 4;
var ppuScanlines = 262;
var scanlineCyclesDuration = 341;
var ppuCyclesPerFrame = ppuScanlines * scanlineCyclesDuration;
// TODO
var colors = new byte[4][];
colors[0] = new byte[3] { 0, 0, 0 };
colors[1] = new byte[3] { 85, 85, 85 };
colors[2] = new byte[3] { 170, 170, 170 };
colors[3] = new byte[3] { 255, 255, 255 };

using (GameWindow2D yanesWindow = new(frameRate, nesScreenDimensions, screenScalingFactor))
{
    yanesWindow.UpdateFrame += args => OnUpdateFrame(yanesWindow);
    yanesWindow.Run();
}

void OnUpdateFrame(GameWindow2D yanesWindow)
{
    var ppuCyclesPerformed = 0;

    Console.WriteLine($"frame start");

    while (ppuCyclesPerformed < ppuCyclesPerFrame)
    {
        var cpuReport = context.Cpu.ExecuteNextInstruction();
        Console.WriteLine($"cpu opcode {cpuReport.Opcode:X4}");
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

void DrawPixel(int x, int y)
{
    var ppuControllerState = context.Ppu.Controller;
    var nametableIndex = ppuControllerState & 0b0000_0011;
    var baseNametableAddress = nametableIndex switch
    {
        0 => 0x0000,
        1 => 0x0400,
        2 => 0x0800,
        3 => 0x0C00,
        _ => throw new ArgumentOutOfRangeException()
    };
    var bgTilesBaseAddress = (ppuControllerState & 0b0001_0000) == 0 ? 0x0000 : 0x1000;
    var tileSizePixels = 8;
    var nametableX = x / tileSizePixels;
    var nametableY = y / tileSizePixels;
    var nametableWidthTiles = 32;
    var nametableAddress = baseNametableAddress + nametableX + nametableY * nametableWidthTiles;
    var tile = context.Ppu.ReadRam(nametableAddress);
    var tileSize = 16;
    var tileStart = bgTilesBaseAddress + tile * tileSize;
    var tileX = x % tileSize;
    var tileY = y & tileSize;
    var upper = context.Rom.ChrRom[tileStart + tileY];
    upper = (byte)(upper << tileX);
    var lower = context.Rom.ChrRom[tileStart + tileY + 8];
    lower = (byte)(lower << tileX);
    var colorCode = (upper & 0b1000_0000) > 0 ? 2 : 0 +
                    (lower & 0b1000_0000) > 0 ? 1 : 0;
    var color = colors[colorCode];
    renderingImage.SetPixel(x, y, color[0], color[1], color[2]);
}

class Context
{
    public Rom Rom { get; }
    public Cpu Cpu { get; }
    public Ppu Ppu { get; }

    public Context(string romPath)
    {
        Rom = RomParser.FromFile(romPath);
        Cpu = new Cpu(new CpuSettings { StartingProgramAddress = 0xC000, InitialProcessorStatus = 0x24 });
        Ppu = new Ppu();

        Cpu.Bus.AttachPpu(Ppu);
        Ppu.AttachInterruptsListener(Cpu);
        Ppu.AttachRom(Rom);
        Cpu.InsertCartridge(Rom);
    }
}