using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Base
{
    internal abstract class BranchingInstruction : Instruction
    {
        internal BranchingInstruction(byte opcode) : base(opcode)
        {
        }

        public override int Execute(RAM ram, RegistersProvider registers)
        {
            var displacementAddress = registers.ProgramCounter.State;
            var displacement = ram.Read8bit(displacementAddress);
            registers.ProgramCounter.State += 1;

            if (!ConditionMet(ram, registers))
                return 2;

            var forwardBranching = !displacement.IsNegative();
            ushort newProgramCounterValue = forwardBranching ?
                (ushort)(registers.ProgramCounter.State + displacement) :
                (ushort)(registers.ProgramCounter.State - displacement.ToComplimentaryPositive());

            var pageCrossed = registers.ProgramCounter.State >> 8 != newProgramCounterValue >> 8;

            registers.ProgramCounter.State = newProgramCounterValue;

            return pageCrossed ? 4 : 3;
        }

        protected abstract bool ConditionMet(RAM ram, RegistersProvider registers);
    }
}
