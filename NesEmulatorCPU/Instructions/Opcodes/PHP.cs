using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class PHP : Instruction
    {
        public PHP(byte opcode) : base(opcode)
        {
        }

        public override int Execute(RAM ram, RegistersProvider registers)
        {
            var value = registers.ProcessorStatus.State;

            ram.Write8Bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State), value);

            registers.StackPointer.State -= 1;

            return 3;
        }
    }
}
