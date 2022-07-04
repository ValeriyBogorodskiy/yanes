namespace NesEmulator
{
    internal class RAM
    {
        private readonly byte[] cells = new byte[ushort.MaxValue];

        internal byte Read(ushort address) => cells[address];

        internal void Wite(ushort address, byte value) => cells[address] = value;

        internal void LoadProgram(ushort startingProgramAddress, byte[] program)
        {
            for (int i = 0; i < program.Length; i++)
            {
                var programByte = program[i];
                cells[startingProgramAddress + i] = programByte;
            }
        }
    }
}
