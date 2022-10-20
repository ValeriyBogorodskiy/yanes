namespace YaNES.Interfaces
{
    public interface IRom
    {
        int PrgRomLength { get; }
        int ChrRomLength { get; }

        byte Read8bitPrg(ushort address);
        byte Read8bitChr(ushort address);

        ushort Read16bitPrg(ushort address);
    }
}
