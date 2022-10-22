using YaNES.Core.Utils;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class INX : IInstructionLogic
    {
        void IInstructionLogic.Execute(Bus bus, RegistersProvider registers)
        {
            byte value = (byte)(registers.IndexRegisterX.State + 1);

            registers.IndexRegisterX.State = value;
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, value.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, value.IsZero());
        }
    }
}
