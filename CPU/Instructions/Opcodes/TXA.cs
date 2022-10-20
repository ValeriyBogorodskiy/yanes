using NesEmulatorCPU.Instructions.Base;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class TXA : TransferInstruction
    {
        public TXA(byte opcode) : base(opcode)
        {
        }

        protected override CpuRegister8Bit SourceRegister(RegistersProvider registers) => registers.IndexRegisterX;

        protected override CpuRegister8Bit TargetRegister(RegistersProvider registers) => registers.Accumulator;
    }
}
