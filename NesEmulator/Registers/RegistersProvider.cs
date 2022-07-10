namespace NesEmulatorCPU.Registers
{
    // TODO : naming
    internal class RegistersProvider
    {
        internal ProgramCounter ProgramCounter { get; private set; } = new();
        internal StackPointer StackPointer { get; private set; } = new();
        internal Accumulator Accumulator { get; private set; } = new();
        internal IndexRegisterX IndexRegisterX { get; private set; } = new();
        internal IndexRegisterY IndexRegisterY { get; private set; } = new();
        internal ProcessorStatus ProcessorStatus { get; private set; } = new();

        internal void Reset()
        {
            ProgramCounter.State = 0;
            StackPointer.State = 0;
            Accumulator.State = 0;
            IndexRegisterX.State = 0;
            IndexRegisterY.State = 0;
            ProcessorStatus.State = 0;
        }
    }
}
