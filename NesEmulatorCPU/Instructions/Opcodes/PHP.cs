using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class PHP : Instruction
    {
        public PHP(byte opcode) : base(opcode)
        {
        }

        public override int Execute(Bus bus, RegistersProvider registers)
        {
            var value = registers.ProcessorStatus.State;

            bus.Write8Bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State), value);

            registers.StackPointer.State -= 1;

            return 3;
        }
    }
}
