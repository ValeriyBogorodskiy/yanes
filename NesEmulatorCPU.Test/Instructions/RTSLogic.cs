using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Opcodes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class RTSLogic
    {
        [Test]
        public void ReturnFromSubroutine()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var absoluteAddressingMode = new Absolute();

            registers.StackPointer.State = 0xFF;
            registers.ProgramCounter.State = 0x0601;
            ram.Write16Bit(0x0601, 0x0605);

            var jsr = (IInstructionLogicWithAddressingMode)new JSR();
            jsr.Execute(absoluteAddressingMode, ram, registers);

            Assert.That(registers.StackPointer.State, Is.EqualTo(0xFD));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x0605));

            var rts = new RTS(0x60);
            rts.Execute(ram, registers);

            Assert.That(registers.StackPointer.State, Is.EqualTo(0xFF));
            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x0603));
        }
    }
}
