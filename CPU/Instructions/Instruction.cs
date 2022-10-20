using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions
{
    internal abstract class Instruction : IInstruction
    {
        private readonly byte opcode;

        internal Instruction(byte opcode)
        {
            this.opcode = opcode;
        }

        byte IInstruction.Opcode => opcode;

        public abstract int Execute(Bus bus, RegistersProvider registers);
    }
}
