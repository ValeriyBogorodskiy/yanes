using YaNES.CPU;
using YaNES.ROM;

var rom = RomParser.FromFile("../../../nestest.nes");
var cpuSettings = new CpuSettings { StartingProgramAddress = 0xC000, InitialProcessorStatus = 0x24 };
var cpu = new CPU(cpuSettings);
var registers = cpu.Registers;
var cpuProcess = cpu.Run(rom);
var cycles = 7; // idk why first test.log entry has CYC:7 value

using var streamWriter = new StreamWriter("yanes.log");

while (true)
{
    cpuProcess.MoveNext();

    cycles += cpuProcess.Current.Cycles;

    streamWriter.WriteLine(
        $"{registers.ProgramCounter:X4}" +
        $" A:{registers.Accumulator:X2}" +
        $" X:{registers.IndexRegisterX:X2}" +
        $" Y:{registers.IndexRegisterY:X2}" +
        $" P:{registers.ProcessorStatus:X2}" +
        $" SP:{registers.StackPointer:X2}" +
        $" CYC:{cycles}");
}