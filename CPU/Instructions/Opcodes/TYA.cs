using NesEmulatorCPU.Instructions.Base;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class TYA : TransferInstruction
    {
        public TYA(byte opcode) : base(opcode)
        {
        }

        protected override CpuRegister8Bit SourceRegister(RegistersProvider registers) => registers.IndexRegisterY;
        protected override CpuRegister8Bit TargetRegister(RegistersProvider registers) => registers.Accumulator;
    }
}
