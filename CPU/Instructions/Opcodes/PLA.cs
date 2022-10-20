using YaNES.CPU.Registers;
using YaNES.CPU.Utils;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class PLA : Instruction
    {
        public PLA(byte opcode) : base(opcode)
        {
        }

        public override int Execute(Bus bus, RegistersProvider registers)
        {
            registers.StackPointer.State += 1;

            var value = bus.Read8bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State));

            registers.Accumulator.State = value;
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, value.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, value.IsZero());

            return 4;
        }
    }
}
