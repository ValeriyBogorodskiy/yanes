﻿using YaNES.CPU.AddressingModes;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class DOP : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            addressingMode.GetRamValue(bus, registers);
        }
    }
}
