using NesEmulator.Cpu;

namespace NesEmulator.Instructions
{
    internal class INX : Instruction
    {
        internal INX(byte opcode) : base(opcode)
        {
        }

        internal override void Execute(RAM ram, Cpu.Registers registers)
        {
            byte value = (byte)(registers.IndexRegisterX.State + 1);

            registers.IndexRegisterX.State = value;
            registers.ProcessorStatus.UpdateNegativeFlag(value);
            registers.ProcessorStatus.UpdateZeroFlag(value);
        }
    }
}
