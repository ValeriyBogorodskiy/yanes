﻿using YaNES.CPU.Instructions.Base;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class BEQ : BranchingInstruction
    {
        public BEQ(byte opcode) : base(opcode)
        {
        }

        protected override bool ConditionMet(Bus bus, RegistersProvider registers)
        {
            return registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero);
        }
    }
}
