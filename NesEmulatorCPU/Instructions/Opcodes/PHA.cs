using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class PHA : Instruction
    {
        public PHA(byte opcode) : base(opcode)
        {
        }

        public override int Execute(RAM ram, RegistersProvider registers)
        {
            var value = registers.Accumulator.State;

            ram.Write8Bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State), value);

            registers.StackPointer.State -= 1;

            return 3;
        }
    }
}
