using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Opcodes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class DECLogic
    {
        [Test]
        public void PositiveBeforePositiveAfter()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0x00, 0x10);

            var dec = (IInstructionLogicWithAddressingMode)new DEC();
            dec.Execute(immediateAddressingMode, ram, registers);

            Assert.That(ram.Read8bit(0x00), Is.EqualTo(0x0F));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }

        [Test]
        public void PositiveBeforeZeroAfter()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0x00, 0x01);

            var dec = (IInstructionLogicWithAddressingMode)new DEC();
            dec.Execute(immediateAddressingMode, ram, registers);

            Assert.That(ram.Read8bit(0x00), Is.EqualTo(0x00));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(true));
        }

        [Test]
        public void ZeroBeforeNegativeAfter()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0x00, 0x00);

            var dec = (IInstructionLogicWithAddressingMode)new DEC();
            dec.Execute(immediateAddressingMode, ram, registers);

            Assert.That(ram.Read8bit(0x00), Is.EqualTo(0xFF));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }

        [Test]
        public void NegativeBeforeNegativeAfter()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0x00, 0xFF);

            var dec = (IInstructionLogicWithAddressingMode)new DEC();
            dec.Execute(immediateAddressingMode, ram, registers);

            Assert.That(ram.Read8bit(0x00), Is.EqualTo(0xFE));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }

        [Test]
        public void NegativeBeforePositiveAfter()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0x00, 0x80);

            var dec = (IInstructionLogicWithAddressingMode)new DEC();
            dec.Execute(immediateAddressingMode, ram, registers);

            Assert.That(ram.Read8bit(0x00), Is.EqualTo(0x7F));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }
    }
}
