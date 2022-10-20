namespace YaNES.CPU.Registers
{
    internal class ProcessorStatus : CpuRegister8Bit
    {
        internal enum Flags : byte
        {
            Negative = 1 << 7,
            Overflow = 1 << 6,
            BFlag = 1 << 5,
            BreakCommand = 1 << 4,
            Decimal = 1 << 3,
            InterruptDisable = 1 << 2,
            Zero = 1 << 1,
            Carry = 1 << 0
        }

        internal bool Get(Flags flag) => (State & (byte)flag) > 0;

        internal void Set(Flags flag, bool value)
        {
            if (value)
                State |= (byte)flag;
            else
                State &= (byte)~flag;
        }
    }
}
