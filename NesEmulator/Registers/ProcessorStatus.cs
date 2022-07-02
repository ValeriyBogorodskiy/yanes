namespace NesEmulator.Registers
{
    internal class ProcessorStatus : CpuRegister8Bit
    {
        public class Flag
        {
            private readonly int position;
            private readonly ProcessorStatus processorStatus;

            public Flag(int position, ProcessorStatus processorStatus)
            {
                this.position = position;
                this.processorStatus = processorStatus;
            }

            public void Set(bool value)
            {
                if (value)
                    processorStatus.State |= (byte)(1 << position);
                else
                    processorStatus.State &= (byte)~(1 << position);
            }

            private bool Get() => (processorStatus.State & (byte)(1 << position)) > 0;
        }

        public readonly Flag Negative;
        public readonly Flag Overflow;
        public readonly Flag BreakCommand;
        public readonly Flag Decimal;
        public readonly Flag InterruptDisable;
        public readonly Flag Zero;
        public readonly Flag Carry;

        public ProcessorStatus()
        {
            Negative = new(7, this);
            Overflow = new(6, this);
            BreakCommand = new(4, this);
            Decimal = new(3, this);
            InterruptDisable = new(2, this);
            Zero = new(1, this);
            Carry = new(0, this);
        }
    }
}
