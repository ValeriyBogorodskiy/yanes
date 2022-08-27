using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Opcodes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    /// <summary>
    /// Test cases are taken from table in article http://www.righto.com/2012/12/the-6502-overflow-flag-explained.html
    /// </summary>
    [TestFixture]
    internal class ADCLogic
    {
        [Test]
        public void NoUnsignedCarryOrSignedOverflowPositiveResult()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            bus.Write8Bit(0x00, 0x50);
            registers.Accumulator.State = 0x10;

            var adc = (IInstructionLogicWithAddressingMode)new ADC();
            adc.Execute(immediateAddressingMode, bus, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0x60));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(false));
        }

        [Test]
        public void NoUnsignedCarryButSignedOverflowNegativeResult()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            bus.Write8Bit(0x00, 0x50);
            registers.Accumulator.State = 0x50;

            var adc = (IInstructionLogicWithAddressingMode)new ADC();
            adc.Execute(immediateAddressingMode, bus, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0xA0));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(false));
        }

        [Test]
        public void NoUnsignedCarryOrSignedOverflowNegativeResult()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            bus.Write8Bit(0x00, 0x50);
            registers.Accumulator.State = 0x90;

            var adc = (IInstructionLogicWithAddressingMode)new ADC();
            adc.Execute(immediateAddressingMode, bus, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0xE0));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(false));
        }

        [Test]
        public void UnsignedCarryButNoSignedOverflowPositiveResult()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            bus.Write8Bit(0x00, 0x50);
            registers.Accumulator.State = 0xD0;

            var adc = (IInstructionLogicWithAddressingMode)new ADC();
            adc.Execute(immediateAddressingMode, bus, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0x20));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(true));
        }

        [Test]
        public void NoUnsignedCarryOrSignedOverflowNegativeResult2()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            bus.Write8Bit(0x00, 0xD0);
            registers.Accumulator.State = 0x10;

            var adc = (IInstructionLogicWithAddressingMode)new ADC();
            adc.Execute(immediateAddressingMode, bus, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0xE0));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(false));
        }

        [Test]
        public void UnsignedCarryButNoSignedOverflowPositiveResult2()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            bus.Write8Bit(0x00, 0xD0);
            registers.Accumulator.State = 0x50;

            var adc = (IInstructionLogicWithAddressingMode)new ADC();
            adc.Execute(immediateAddressingMode, bus, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0x20));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(true));
        }

        [Test]
        public void UnsignedCarryAndSignedOverflowPositiveResult()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            bus.Write8Bit(0x00, 0xD0);
            registers.Accumulator.State = 0x90;

            var adc = (IInstructionLogicWithAddressingMode)new ADC();
            adc.Execute(immediateAddressingMode, bus, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0x60));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(true));
        }

        [Test]
        public void UnsignedCarryButNoSignedOverflowNegativeResult()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            bus.Write8Bit(0x00, 0xD0);
            registers.Accumulator.State = 0xD0;

            var adc = (IInstructionLogicWithAddressingMode)new ADC();
            adc.Execute(immediateAddressingMode, bus, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0xA0));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(true));
        }

        [Test]
        public void AddingTwoNegativeValues()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            bus.Write8Bit(0x00, 0x81);
            registers.Accumulator.State = 0x81;

            var adc = (IInstructionLogicWithAddressingMode)new ADC();
            adc.Execute(immediateAddressingMode, bus, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0x02));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(true));
        }

        [Test]
        public void SBCConcern()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            bus.Write8Bit(0x00, 0x3C);
            registers.Accumulator.State = 0xC0;

            var adc = (IInstructionLogicWithAddressingMode)new ADC();
            adc.Execute(immediateAddressingMode, bus, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0xFC));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(false));
        }
    }
}
