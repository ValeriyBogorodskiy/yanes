using YaNES.CPU.Registers;
using YaNES.CPU.Utils;

namespace YaNES.CPU.Instructions.Opcodes
{
    /// <summary>
    /// https://www.masswerk.at/6502/6502_instruction_set.html#PLP
    /// The status register will be pulled with the break flag and bit 5 ignored.
    /// </summary>
    internal class PLP : Instruction
    {
        public PLP(byte opcode) : base(opcode)
        {
        }

        public override int Execute(Bus bus, RegistersProvider registers)
        {
            registers.StackPointer.State += 1;

            var value = bus.Read8bit((ushort)(ReservedAddresses.StackBottom + registers.StackPointer.State));

            value &= (byte)~ProcessorStatus.Flags.BreakCommand;
            value |= (byte)ProcessorStatus.Flags.BFlag;

            registers.ProcessorStatus.State = value;

            return 4;
        }
    }
}
