using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Logic;
using NesEmulatorCPU.Registers;


namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class INCLogic
    {
        [Test]
        public void PositiveBeforePositiveAfter()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0X00, 0X01);

            var inc = (IInstructionLogicWithAddressingMode)new INC();
            inc.Execute(immediateAddressingMode, ram, registers);

            Assert.That(ram.Read8bit(0X00), Is.EqualTo(0X02));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }

        [Test]
        public void ZeroBeforePositiveAfter()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0X00, 0X00);

            var inc = (IInstructionLogicWithAddressingMode)new INC();
            inc.Execute(immediateAddressingMode, ram, registers);

            Assert.That(ram.Read8bit(0X00), Is.EqualTo(0X01));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }

        [Test]
        public void NegativeBeforeNegativeAfter()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0X00, 0X82);

            var inc = (IInstructionLogicWithAddressingMode)new INC();
            inc.Execute(immediateAddressingMode, ram, registers);

            Assert.That(ram.Read8bit(0X00), Is.EqualTo(0X83));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }

        [Test]
        public void NegativeBeforeZeroAfter()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0X00, 0XFF);

            var inc = (IInstructionLogicWithAddressingMode)new INC();
            inc.Execute(immediateAddressingMode, ram, registers);

            Assert.That(ram.Read8bit(0X00), Is.EqualTo(0X00));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(true));
        }
    }
}
