using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class JSR : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, RAM ram, RegistersProvider registers)
        {
            var memoryAddress = addressingMode.GetRamAddress(ram, registers);

            var mostSignificantByte = (byte)(registers.ProgramCounter.State >> 8);
            var leastSignificantByte = (byte)registers.ProgramCounter.State;

            ram.Write8Bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State), mostSignificantByte);

            registers.StackPointer.State -= 1;

            ram.Write8Bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State), leastSignificantByte);

            registers.StackPointer.State -= 1;

            registers.ProgramCounter.State = memoryAddress;
        }
    }
}
