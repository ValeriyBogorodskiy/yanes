using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Logic;
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

            ram.Write8Bit(0X00, 0X10);

            var dec = (IInstructionLogicWithAddressingMode)new DEC();
            dec.Execute(immediateAddressingMode, ram, registers);

            Assert.That(ram.Read8bit(0X00), Is.EqualTo(0X0F));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }

        [Test]
        public void PositiveBeforeZeroAfter()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0X00, 0X01);

            var dec = (IInstructionLogicWithAddressingMode)new DEC();
            dec.Execute(immediateAddressingMode, ram, registers);

            Assert.That(ram.Read8bit(0X00), Is.EqualTo(0X00));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(true));
        }

        [Test]
        public void ZeroBeforeNegativeAfter()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0X00, 0X00);

            var dec = (IInstructionLogicWithAddressingMode)new DEC();
            dec.Execute(immediateAddressingMode, ram, registers);

            Assert.That(ram.Read8bit(0X00), Is.EqualTo(0XFF));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }

        [Test]
        public void NegativeBeforeNegativeAfter()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0X00, 0XFF);

            var dec = (IInstructionLogicWithAddressingMode)new DEC();
            dec.Execute(immediateAddressingMode, ram, registers);

            Assert.That(ram.Read8bit(0X00), Is.EqualTo(0XFE));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }

        [Test]
        public void NegativeBeforePositiveAfter()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0X00, 0X80);

            var dec = (IInstructionLogicWithAddressingMode)new DEC();
            dec.Execute(immediateAddressingMode, ram, registers);

            Assert.That(ram.Read8bit(0X00), Is.EqualTo(0X7F));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }
    }
}
