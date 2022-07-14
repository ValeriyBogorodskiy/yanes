using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions
{
    internal class Instruction : IInstruction
    {
        private readonly byte opcode;
        private readonly Func<RAM, RegistersProvider, int> logic;

        internal Instruction(byte opcode, Func<RAM, RegistersProvider, int> logic)
        {
            this.opcode = opcode;
            this.logic = logic;
        }

        byte IInstruction.Opcode => opcode;

        int IInstruction.Execute(RAM ram, RegistersProvider registers) => logic(ram, registers);
    }
}
