using YaNES.CPU.Registers;
using YaNES.CPU.Utils;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class PHA : Instruction
    {
        public PHA(byte opcode) : base(opcode)
        {
        }

        public override int Execute(Bus bus, RegistersProvider registers)
        {
            var value = registers.Accumulator.State;

            bus.Write8Bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State), value);

            registers.StackPointer.State -= 1;

            return 3;
        }
    }
}
