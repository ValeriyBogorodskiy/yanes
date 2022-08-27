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
            var bus = new Bus();
            var registers = new RegistersProvider();
            var absoluteAddressingMode = new Absolute();

            bus.Write16Bit(0x0000, 0x0600);

            var jmp = (IInstructionLogicWithAddressingMode)new JMP();
            jmp.Execute(absoluteAddressingMode, bus, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x0600));
        }

        [Test]
        public void JumpFromTheLastByteOfAPage()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var indirectAddressingMode = new Indirect();

            bus.Write8Bit(0x400, 0x40);
            bus.Write8Bit(0x4FF, 0x80);
            bus.Write8Bit(0x500, 0x50);

            bus.Write16Bit(0x0000, 0x4FF);

            var jmp = (IInstructionLogicWithAddressingMode)new JMP();
            jmp.Execute(indirectAddressingMode, bus, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x4080));
        }
    }
}
