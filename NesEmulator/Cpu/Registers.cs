using NesEmulator.Registers;

namespace NesEmulator.Cpu
{
    internal class Registers
    {
        internal ProgramCounter ProgramCounter { get; private set; } = new();
        internal StackPointer StackPointer { get; private set; } = new();
        internal Accumulator Accumulator { get; private set; } = new();
        internal IndexRegisterX IndexRegisterX { get; private set; } = new();
        internal IndexRegisterY IndexRegisterY { get; private set; } = new();
        internal ProcessorStatus ProcessorStatus { get; private set; } = new();

        internal void Reset()
        {
            ProgramCounter.Reset();
            StackPointer.Reset();
            Accumulator.Reset();
            IndexRegisterX.Reset();
            IndexRegisterY.Reset();
            ProcessorStatus.Reset();
        }
    }
}
