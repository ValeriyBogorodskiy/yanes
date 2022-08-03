using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Logic
{
    internal class NOP : IInstruction
    {
        private readonly byte opcode;

        internal NOP(byte opcode)
        {
            this.opcode = opcode;
        }

        byte IInstruction.Opcode => opcode;

        int IInstruction.Execute(RAM ram, RegistersProvider registers)
        {
            return 2;
        }
    }
}
