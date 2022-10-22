using YaNES.Core;
using YaNES.Core.Utils;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Base
{
    internal abstract class TransferInstruction : Instruction
    {
        public TransferInstruction(byte opcode) : base(opcode)
        {
        }

        public override int Execute(Bus bus, RegistersProvider registers)
        {
            var value = SourceRegister(registers).State;
            TargetRegister(registers).State = value;

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, value.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, value.IsZero());

            return 2;
        }


        protected abstract Register8Bit SourceRegister(RegistersProvider registers);
        protected abstract Register8Bit TargetRegister(RegistersProvider registers);
    }
}
