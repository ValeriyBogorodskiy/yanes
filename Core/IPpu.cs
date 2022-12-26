namespace YaNES.Core
{
    public interface IPpu
    {
        byte Controller { set; }
        byte Mask { set; }
        byte Status { get; }
        byte OamAddress { set; }
        byte OamData { get; }
        byte Scroll { set; }
        byte Address { set; }
        byte Data { get; set; }
        byte OpenBus { get; }

        void AttachRom(IRom rom);
        void AttachInterruptsListener(IInterruptsListener interruptsSource);
        void Update(int cycles);
        void WriteOamData(byte[] buffer);
    }
}
