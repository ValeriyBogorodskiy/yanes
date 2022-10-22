using YaNES.CPU.Registers;
using YaNES.Utils;

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


        protected abstract CpuRegister8Bit SourceRegister(RegistersProvider registers);
        protected abstract CpuRegister8Bit TargetRegister(RegistersProvider registers);
    }
}
