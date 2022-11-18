using YaNes.PPU;
using YaNES.CPU;
using YaNES.ROM;
using YaNES.Console;
using OpenTK.Mathematics;
using YaNES.Core.Utils;

var context = new Context("../../../../SuperMarioBros.nes");
var frameRate = 60;
var nesScreenDimensions = new Vector2i(128, 128);
var resolutionScalingFactor = 5;
var scaledScreenDimensions = nesScreenDimensions * resolutionScalingFactor;
var ppuScanlines = 262;
var scanlineCyclesDuration = 341;
var ppuCyclesPerFrame = ppuScanlines * scanlineCyclesDuration;
var scalableImage = new ScalableImage(nesScreenDimensions.X, nesScreenDimensions.Y, resolutionScalingFactor);

using (GameWindow2D yanesWindow = new(frameRate, scaledScreenDimensions.X, scaledScreenDimensions.Y))
{
    yanesWindow.UpdateFrame += args => OnUpdateFrame(yanesWindow);
    yanesWindow.Run();
}

void OnUpdateFrame(GameWindow2D yanesWindow)
{
    void DrawTile(byte[] chrRom, byte bank, int tile, int pixelX, int pixelY) // TODO : pixelX, pixelY temp params
    {
        var bankStart = bank * 0x1000;
        var tileSize = 16;
        var tileStart = bankStart + tile * tileSize;

        for (var y = 0; y < 8; y++)
        {
            var upper = chrRom[tileStart + y];
            var lower = chrRom[tileStart + y + 8];

            for (var x = 0; x < 8; x++)
            {
                var colorCode = (upper & 0b1000_0000) > 0 ? 2 : 0 +
                                (lower & 0b1000_0000) > 0 ? 1 : 0;
                var color = colorCode switch
                {
                    0 => new byte[3] { 0, 0, 0 },
                    1 => new byte[3] { 85, 85, 85 },
                    2 => new byte[3] { 170, 170, 170 },
                    3 => new byte[3] { 255, 255, 255 },
                    _ => throw new ArgumentOutOfRangeException()
                };

                scalableImage.SetPixel(pixelX + x, pixelY + y, color[0], color[1], color[2]);

                upper = (byte)(upper << 1);
                lower = (byte)(lower << 1);
            }
        }
    }

    for (var i = 0; i < 256; i++)
    {
        DrawTile(context.Rom.ChrRom, 0, i, (i * 8) % 128, (i / 16) * 8);
    }

    scalableImage.Scale();
    yanesWindow.SetImage(scalableImage.ScaledImage);
}

//void OnUpdateFrame(YanesWindow window)
//{
//    var ppyCyclesPerformed = 0;

//    while (ppyCyclesPerformed < ppuCyclesPerFrame)
//    {
//        var cpuReport = context.Cpu.ExecuteNextInstruction();
//        var cpuCyclesTaken = cpuReport.Cycles;
//        var ppuCyclesBudget = cpuCyclesTaken * 3;

//        context.Ppu.Update(ppuCyclesBudget);
//    }
//}

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