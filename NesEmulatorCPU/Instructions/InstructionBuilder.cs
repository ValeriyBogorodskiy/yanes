﻿using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions
{
    internal class InstructionBuilder
    {
        private byte? opcode;
        private int? cycles;

        private IInstructionLogic? instructionLogic;

        private IInstructionLogicWithAddressingMode? instructionLogicWithAdressingMode;
        private AddressingMode? addressingMode;

        private bool withPageCrossing;

        internal IInstruction Build()
        {
            TryPerformConfigurationChecks();

            var instructionSupportsMultipleAddressingModes = instructionLogicWithAdressingMode is not null;
            var logic = instructionSupportsMultipleAddressingModes ? CreateInstructionLogicWithAddressisngMode() : CreateSimpleInstructionLogic();

            if (withPageCrossing)
                logic = WrapLogicWithPageCrossingCheck(logic);

            return new InstructionWithDynamicLogic(opcode.Value, logic);
        }

        private void TryPerformConfigurationChecks()
        {
            if (!opcode.HasValue)
                throw new InvalidOperationException("Instruction opcode must be specified");

            if (!cycles.HasValue)
                throw new InvalidOperationException("Instruciton execution time in cycles must be specified");

            if (instructionLogic is null && instructionLogicWithAdressingMode is null)
                throw new InvalidOperationException("Instruction logic must be specified");

            if (instructionLogicWithAdressingMode is not null && addressingMode is null)
                throw new InvalidOperationException("Addressing mode for operation with multiple addressing modes support must be specified");

            if (withPageCrossing && addressingMode is not IBoundaryCrossingMode)
                throw new InvalidOperationException("Specified addressing mode doesn't support page crossing check");
        }

        private Func<RAM, RegistersProvider, int> CreateInstructionLogicWithAddressisngMode()
        {
            return (ram, registers) =>
            {
                instructionLogicWithAdressingMode.Execute(addressingMode, ram, registers);
                return cycles.Value;
            };
        }

        private Func<RAM, RegistersProvider, int> CreateSimpleInstructionLogic()
        {
            return (ram, registers) =>
            {
                instructionLogic.Execute(ram, registers);
                return cycles.Value;
            };
        }

        private Func<RAM, RegistersProvider, int> WrapLogicWithPageCrossingCheck(Func<RAM, RegistersProvider, int> logic)
        {
            var pageCrossingMode = addressingMode as IBoundaryCrossingMode;

            return (ram, registers) =>
            {
                var additionalCycles = pageCrossingMode.CheckIfBoundaryWillBeCrossed(ram, registers) ? 1 : 0;
                return logic(ram, registers) + additionalCycles;
            };
        }

        internal InstructionBuilder Opcode(byte opcode)
        {
            this.opcode = opcode;
            return this;
        }

        internal InstructionBuilder Cycles(int cycles)
        {
            this.cycles = cycles;
            return this;
        }

        internal InstructionBuilder Logic<TLogic>() where TLogic : IInstructionLogic, new()
        {
            instructionLogic = new TLogic();
            return this;
        }

        internal InstructionBuilder Logic<TLogic, TMode>() where TLogic : IInstructionLogicWithAddressingMode, new() where TMode : AddressingMode, new()
        {
            instructionLogicWithAdressingMode = new TLogic();
            addressingMode = new TMode();
            return this;
        }

        internal InstructionBuilder WithPageCrossing(bool value = true)
        {
            withPageCrossing = value;
            return this;
        }
    }
}
