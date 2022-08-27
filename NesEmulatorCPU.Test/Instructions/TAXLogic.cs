using NesEmulatorCPU.Instructions.Opcodes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class TAXLogic
    {
        [Test]
        public void LoadingZeroValue()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.Accumulator.State = 0x00;

            new TAX(0x00).Execute(bus, registers);

            Assert.That(registers.IndexRegisterX.State, Is.EqualTo(0x00));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(true));
        }

        [Test]
        public void LoadingPositiveValue()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.Accumulator.State = 0x7F;

            new TAX(0x00).Execute(bus, registers);

            Assert.That(registers.IndexRegisterX.State, Is.EqualTo(0x7F));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }

        [Test]
        public void LoadingNegativeValue()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.Accumulator.State = 0xAA;

            new TAX(0x00).Execute(bus, registers);

            Assert.That(registers.IndexRegisterX.State, Is.EqualTo(0xAA));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }
    }
}
