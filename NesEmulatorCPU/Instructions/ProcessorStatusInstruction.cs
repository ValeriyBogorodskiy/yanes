using NesEmulatorCPU.Registers;
using static NesEmulatorCPU.Registers.ProcessorStatus;

namespace NesEmulatorCPU.Instructions
{
    internal abstract class ProcessorStatusInstruction : IInstruction
    {
        byte IInstruction.Opcode => opcode;

        private readonly byte opcode;

        internal ProcessorStatusInstruction(byte opcode)
        {
            this.opcode = opcode;
        }

        int IInstruction.Execute(RAM ram, RegistersProvider registers)
        {
            registers.ProcessorStatus.Set(Flag, Value);
            return 2;
        }

        protected abstract Flags Flag { get; }
        protected abstract bool Value { get; }
    }
}
