using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Logic;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class LDXLogic
    {
        [Test]
        public void LoadingZeroValue()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            var ldx = (IInstructionLogicWithAddressingMode)new LDX();
            ldx.Execute(immediateAddressingMode, ram, registers);

            Assert.That(registers.IndexRegisterX.State, Is.EqualTo(0x00));
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

            var ldx = (IInstructionLogicWithAddressingMode)new LDX();
            ldx.Execute(immediateAddressingMode, ram, registers);

            Assert.That(registers.IndexRegisterX.State, Is.EqualTo(0xAA));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }
    }
}
