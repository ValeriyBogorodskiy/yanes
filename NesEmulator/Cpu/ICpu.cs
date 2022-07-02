using NesEmulator.Registers;

namespace NesEmulator.Cpu
{
    public interface ICpu
    {
        void Run(byte[] program);

        IReadOnlyCpuRegister16Bit ProgramCounter { get; }
        IReadOnlyCpuRegister8Bit StackPointer { get; }
        IReadOnlyCpuRegister8Bit Accumulator { get; }
        IReadOnlyCpuRegister8Bit IndexRegisterX { get; }
        IReadOnlyCpuRegister8Bit IndexRegisterY { get; }
        IReadOnlyCpuRegister8Bit ProcessorStatus { get; }
    }
}
