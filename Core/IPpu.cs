namespace YaNES.Core
{
    public interface IPpu
    {
        IPpuRegisters Registers { get; }
        int Scanline { get; }
        int ScanlineCycle { get; }
        void AttachRom(IRom rom);
        void AttachInterruptsListener(IInterruptsListener interruptsSource);
        void Update(int cycles);
        void WriteOamData(byte[] buffer);
        byte[] GetPixelColor(int x, int y);
    }
}
