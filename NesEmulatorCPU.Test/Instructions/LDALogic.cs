using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Logic;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class LDALogic
    {
        [Test]
        public void LoadingZeroValue()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            var lda = (IInstructionLogicWithAddressingMode)new LDA();
            lda.Execute(immediateAddressingMode, ram, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0x00));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(true));
        }

        [Test]
        public void LoadingPositiveValue()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0x00, 0x7F);

            var ldx = (IInstructionLogicWithAddressingMode)new LDX();
            ldx.Execute(immediateAddressingMode, ram, registers);

            Assert.That(registers.IndexRegisterX.State, Is.EqualTo(0x7F));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }

        [Test]
        public void LoadingNegativeValue()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0x00, 0xAA);

            var ldy = (IInstructionLogicWithAddressingMode)new LDY();
            ldy.Execute(immediateAddressingMode, ram, registers);

            Assert.That(registers.IndexRegisterY.State, Is.EqualTo(0xAA));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }
    }
}
