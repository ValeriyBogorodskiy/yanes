using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class PLP : Instruction
    {
        public PLP(byte opcode) : base(opcode)
        {
        }

        public override int Execute(Bus bus, RegistersProvider registers)
        {
            var value = bus.Read8bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State));

            registers.ProcessorStatus.State = value;
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, value.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, value.IsZero());

            registers.StackPointer.State += 1;

            return 4;
        }
    }
}
