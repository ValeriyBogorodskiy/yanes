using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class RTI : Instruction
    {
        public RTI(byte opcode) : base(opcode)
        {
        }

        public override int Execute(Bus bus, RegistersProvider registers)
        {
            registers.StackPointer.State += 1;

            var processorStatus = bus.Read8bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State));
            processorStatus |= (byte)ProcessorStatus.Flags.BFlag;

            registers.ProcessorStatus.State = processorStatus;

            registers.StackPointer.State += 1;

            var programCounterLeastSignificantByte = bus.Read8bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State));

            registers.StackPointer.State += 1;

            var programCounterMostSignificantByte = bus.Read8bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State));

            registers.ProgramCounter.State = (ushort)((programCounterMostSignificantByte << 8) + programCounterLeastSignificantByte);

            return 6;
        }
    }
}
