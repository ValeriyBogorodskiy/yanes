using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Logic;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class JSRLogic
    {
        /// <summary>
        /// https://www.cs.jhu.edu/~phi/csf/slides/lecture-6502-stack.pdf
        /// </summary>
        [Test]
        public void JumpToSubroutine()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var absoluteAddressingMode = new Absolute();

            registers.StackPointer.State = 0xFD;
            registers.ProgramCounter.State = 0x4403;
            ram.Write16Bit(0x4403, 0xFFD2);

            var jsr = (IInstructionLogicWithAddressingMode)new JSR();
            jsr.Execute(absoluteAddressingMode, ram, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0xFFD2));
            Assert.That(ram.Read8bit(0x01FD), Is.EqualTo(0x44));
            Assert.That(ram.Read8bit(0x01FC), Is.EqualTo(0x05));
        }
    }
}
