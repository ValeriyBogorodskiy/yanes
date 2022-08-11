namespace NesEmulatorCPU.Registers
{
    internal class RegistersProvider : IRegisters
    {
        ushort IRegisters.ProgramCounter { get => ProgramCounter.State; set => ProgramCounter.State = value; }
        byte IRegisters.StackPointer { get => StackPointer.State; set => StackPointer.State = value; }
        byte IRegisters.Accumulator { get => Accumulator.State; set => Accumulator.State = value; }
        byte IRegisters.IndexRegisterX { get => IndexRegisterX.State; set => IndexRegisterX.State = value; }
        byte IRegisters.IndexRegisterY { get => IndexRegisterY.State; set => IndexRegisterY.State = value; }
        byte IRegisters.ProcessorStatus { get => ProcessorStatus.State; set => ProcessorStatus.State = value; }

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
