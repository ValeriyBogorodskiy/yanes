using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class RTS : Instruction
    {
        public RTS(byte opcode) : base(opcode)
        {
        }

        public override int Execute(Bus bus, RegistersProvider registers)
        {
            registers.StackPointer.State += 1;

            var leastSignificantByte = bus.Read8bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State));

            registers.StackPointer.State += 1;

            var mostSignificantByte = bus.Read8bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State));

            registers.ProgramCounter.State = (ushort)((mostSignificantByte << 8) + leastSignificantByte);

            return 6;
        }
    }
}
