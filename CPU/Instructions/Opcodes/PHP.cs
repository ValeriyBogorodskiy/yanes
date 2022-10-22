using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    /// <summary>
    /// https://www.masswerk.at/6502/6502_instruction_set.html#PHP
    /// The status register will be pushed with the break flag and bit 5 set to 1.
    /// </summary>
    internal class PHP : Instruction
    {
        public PHP(byte opcode) : base(opcode)
        {
        }

        public override int Execute(Bus bus, RegistersProvider registers)
        {
            var value = registers.ProcessorStatus.State;

            value |= (byte)ProcessorStatus.Flags.BreakCommand;
            value |= (byte)ProcessorStatus.Flags.BFlag;

            bus.Write8Bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State), value);

            registers.StackPointer.State -= 1;

            return 3;
        }
    }
}
