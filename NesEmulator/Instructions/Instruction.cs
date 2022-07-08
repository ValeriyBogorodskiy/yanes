using NesEmulator.Cpu;

namespace NesEmulator.Instructions
{
    internal abstract class Instruction
    {
        internal Instruction(byte opcode)
        {
            Opcode = opcode;
        }

        internal byte Opcode { get; private set; }
        internal abstract void Execute(RAM ram, Cpu.Registers registers);
    }
}
