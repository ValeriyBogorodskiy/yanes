using YaNES.CPU.Registers;
using YaNES.CPU.Utils;

namespace YaNES.CPU.Instructions.Opcodes
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

            registers.ProgramCounter.State = (ushort)((mostSignificantByte << 8) + leastSignificantByte + 1);

            return 6;
        }
    }
}
