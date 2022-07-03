﻿using NesEmulator.Tools;

namespace NesEmulator.Registers
{
    internal class ProcessorStatus : CpuRegister8Bit
    {
        public enum Flags
        {
            Negative = 1 << 7,
            Overflow = 1 << 6,
            BreakCommand = 1 << 4,
            Decimal = 1 << 3,
            InterruptDisable = 1 << 2,
            Zero = 1 << 1,
            Carry = 1 << 0
        }

        public void Set(Flags flag, bool value)
        {
            if (value)
                State |= (byte)flag;
            else
                State &= (byte)~flag;
        }

        public bool Get(Flags flag) => (State & (byte)flag) > 0;
    }
}
