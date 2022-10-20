namespace YaNES.CPU.Registers
{
    internal class RegistersProvider : ICpuRegisters
    {
        ushort ICpuRegisters.ProgramCounter { get => ProgramCounter.State; set => ProgramCounter.State = value; }
        byte ICpuRegisters.StackPointer { get => StackPointer.State; set => StackPointer.State = value; }
        byte ICpuRegisters.Accumulator { get => Accumulator.State; set => Accumulator.State = value; }
        byte ICpuRegisters.IndexRegisterX { get => IndexRegisterX.State; set => IndexRegisterX.State = value; }
        byte ICpuRegisters.IndexRegisterY { get => IndexRegisterY.State; set => IndexRegisterY.State = value; }
        byte ICpuRegisters.ProcessorStatus { get => ProcessorStatus.State; set => ProcessorStatus.State = value; }

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
