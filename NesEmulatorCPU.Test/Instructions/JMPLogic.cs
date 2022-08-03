using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Opcodes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class JMPLogic
    {
        [Test]
        public void SimpleJump()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var absoluteAddressingMode = new Absolute();

            ram.Write16Bit(0x0000, 0x0600);

            var jmp = (IInstructionLogicWithAddressingMode)new JMP();
            jmp.Execute(absoluteAddressingMode, ram, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x0600));
        }

        [Test]
        public void JumpFromTheLastByteOfAPage()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var indirectAddressingMode = new Indirect();

            ram.Write8Bit(0x3000, 0x40);
            ram.Write8Bit(0x30FF, 0x80);
            ram.Write8Bit(0x3100, 0x50);

            ram.Write16Bit(0x0000, 0x30FF);

            var jmp = (IInstructionLogicWithAddressingMode)new JMP();
            jmp.Execute(indirectAddressingMode, ram, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x4080));
        }
    }
}
