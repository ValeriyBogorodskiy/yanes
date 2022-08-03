using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions
{
    internal class InstructionWithDynamicLogic : Instruction
    {
        private readonly Func<RAM, RegistersProvider, int> logic;

        internal InstructionWithDynamicLogic(byte opcode, Func<RAM, RegistersProvider, int> logic) : base(opcode)
        {
            this.logic = logic;
        }

        public override int Execute(RAM ram, RegistersProvider registers) => logic(ram, registers);
    }
}
