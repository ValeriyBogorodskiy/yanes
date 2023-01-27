using YaNES.Core;
using YaNES.CPU;
using YaNES.ROM;

var rom = RomParser.FromFile("../../../nestest.nes");
var cpuSettings = new CpuSettings { InitialProcessorStatus = 0x24 };
var cpu = new Cpu(cpuSettings);
var registers = cpu.Registers;
var executionReport = cpu.InsertCartridge(rom);
var cycles = 0;

using var streamWriter = new StreamWriter("yanes.log");

while (executionReport.Result == CpuInstructionExecutionResult.Success)
{
    cycles += executionReport.Cycles;

    streamWriter.WriteLine(
    $"{registers.ProgramCounter:X4}" +
    $" A:{registers.Accumulator:X2}" +
    $" X:{registers.IndexRegisterX:X2}" +
    $" Y:{registers.IndexRegisterY:X2}" +
    $" P:{registers.ProcessorStatus:X2}" +
    $" SP:{registers.StackPointer:X2}" +
    $" CYC:{cycles}");

    executionReport = cpu.ExecuteNextInstruction();
}