using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class PLA : Instruction
    {
        public PLA(byte opcode) : base(opcode)
        {
        }

        public override int Execute(RAM ram, RegistersProvider registers)
        {
            var value = ram.Read8bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State));

            registers.Accumulator.State = value;
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, value.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, value.IsZero());

            registers.StackPointer.State += 1;

            return 4;
        }
    }
}
