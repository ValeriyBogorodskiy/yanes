using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions
{
    internal class InstructionWithDynamicLogic : Instruction
    {
        private readonly Func<Bus, RegistersProvider, int> logic;

        internal InstructionWithDynamicLogic(byte opcode, Func<Bus, RegistersProvider, int> logic) : base(opcode)
        {
            this.logic = logic;
        }

        public override int Execute(Bus bus, RegistersProvider registers) => logic(bus, registers);
    }
}
