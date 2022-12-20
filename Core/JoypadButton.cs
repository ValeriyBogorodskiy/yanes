﻿namespace YaNES.Core
{
    [Flags]
    public enum JoypadButton : byte
    {
        None = 0b0000_0000,
        Right = 0b1000_0000,
        Left = 0b0100_0000,
        Down = 0b0010_0000,
        Up = 0b0001_0000,
        Start = 0b0000_1000,
        Select = 0b0000_0100,
        ButtonA = 0b0000_0010,
        ButtonB = 0b0000_0001
    }
}
