using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Logic;
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

            ram.Write16Bit(0X0000, 0X0600);

            var jmp = (IInstructionLogicWithAddressingMode)new JMP();
            jmp.Execute(absoluteAddressingMode, ram, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0X0600));
        }

        [Test]
        public void JumpFromTheLastByteOfAPage()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var indirectAddressingMode = new Indirect();

            ram.Write8Bit(0X3000, 0X40);
            ram.Write8Bit(0X30FF, 0X80);
            ram.Write8Bit(0X3100, 0X50);

            ram.Write16Bit(0X0000, 0X30FF);

            var jmp = (IInstructionLogicWithAddressingMode)new JMP();
            jmp.Execute(indirectAddressingMode, ram, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0X4080));
        }
    }
}
