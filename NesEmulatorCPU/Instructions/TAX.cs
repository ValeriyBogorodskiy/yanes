using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions
{
    internal class TAX : Instruction
    {
        internal TAX(byte opcode) : base(opcode)
        {
        }

        internal override void Execute(RAM ram, RegistersProvider registers)
        {
            var value = registers.Accumulator.State;

            registers.IndexRegisterX.State = value;
            registers.ProcessorStatus.UpdateNegativeFlag(value);
            registers.ProcessorStatus.UpdateZeroFlag(value);
        }
    }
}
