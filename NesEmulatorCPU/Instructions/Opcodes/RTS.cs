using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class RTS : Instruction
    {
        public RTS(byte opcode) : base(opcode)
        {
        }

        public override int Execute(RAM ram, RegistersProvider registers)
        {
            registers.StackPointer.State += 1;

            var leastSignificantByte = ram.Read8bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State));

            registers.StackPointer.State += 1;

            var mostSignificantByte = ram.Read8bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State));

            registers.ProgramCounter.State = (ushort)((mostSignificantByte << 8) + leastSignificantByte);

            return 6;
        }
    }
}
