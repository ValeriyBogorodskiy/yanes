namespace NesEmulator
{
    internal class LoadedProgram
    {
        private readonly byte[] rawProgram;

        internal LoadedProgram(byte[] rawProgram)
        {
            this.rawProgram = rawProgram;
        }

        internal byte Fetch(ushort nextInstructionAddress) => rawProgram[nextInstructionAddress];
    }
}
