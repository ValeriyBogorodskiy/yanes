using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class AAX : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            var xState = registers.IndexRegisterX.State;
            var accumulatorState = registers.Accumulator.State;

            var andResult = (byte)(xState & accumulatorState);
            var ramAddress = addressingMode.GetRamAddress(bus, registers);

            bus.Write8Bit(ramAddress, andResult);
        }
    }
}
