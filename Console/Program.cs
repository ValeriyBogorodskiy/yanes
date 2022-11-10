using YaNes.PPU;
using YaNES.CPU;
using YaNES.ROM;
using YaNES.Console;
using OpenTK.Mathematics;

var context = new Context("../../../../SuperMarioBros.nes");
var frameRate = 60;
var nesScreenDimensions = new Vector2i(320, 240);
var resolutionScalingFactor = 3;
var scaledScreenDimensions = nesScreenDimensions * resolutionScalingFactor;
var ppuScanlines = 262;
var scanlineCyclesDuration = 341;
var ppuCyclesPerFrame = ppuScanlines * scanlineCyclesDuration;

using (YanesWindow yanesWindow = new(frameRate, scaledScreenDimensions.X, scaledScreenDimensions.Y))
{
    yanesWindow.UpdateFrame += args => OnUpdateFrame(yanesWindow);
    yanesWindow.Run();
}

void OnUpdateFrame(YanesWindow window)
{
    var ppyCyclesPerformed = 0;

    while (ppyCyclesPerformed < ppuCyclesPerFrame)
    {
        var cpuReport = context.Cpu.ExecuteNextInstruction();
        var cpuCyclesTaken = cpuReport.Cycles;
        var ppuCyclesBudget = cpuCyclesTaken * 3;

        context.Ppu.Update(ppuCyclesBudget);
    }
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