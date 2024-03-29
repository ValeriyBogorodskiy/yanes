﻿namespace YaNES.Core
{
    public interface ICpuBus
    {
        byte Read8bit(ushort address);
        ushort Read16bit(ushort address);
        void Write8Bit(ushort address, byte value);
        void Write16Bit(ushort address, ushort value);

        void AttachPpu(IPpu ppu);
        void AttachJoypads(IJoypad[] joypads);
    }
}
