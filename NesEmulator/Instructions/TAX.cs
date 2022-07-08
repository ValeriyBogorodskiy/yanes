using NesEmulator.Cpu;

namespace NesEmulator.Instructions
{
    internal class TAX : Instruction
    {
        public TAX(byte opcode) : base(opcode)
        {
        }

        internal override void Execute(RAM ram, Cpu.Registers registers)
        {
            var value = registers.Accumulator.State;

            registers.IndexRegisterX.Load(value);
            registers.ProcessorStatus.UpdateNegativeFlag(value);
            registers.ProcessorStatus.UpdateZeroFlag(value);
        }
    }
}
