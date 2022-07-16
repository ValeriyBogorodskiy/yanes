using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Logic;
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
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0X00, 0X50);
            registers.Accumulator.State = 0X10;

            var adc = (IInstructionLogicWithAddressingMode)new ADC();
            adc.Execute(immediateAddressingMode, ram, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0X60));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(false));
        }

        [Test]
        public void NoUnsignedCarryButSignedOverflowNegativeResult()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0X00, 0X50);
            registers.Accumulator.State = 0X50;

            var adc = (IInstructionLogicWithAddressingMode)new ADC();
            adc.Execute(immediateAddressingMode, ram, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0XA0));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(false));
        }

        [Test]
        public void NoUnsignedCarryOrSignedOverflowNegativeResult()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0X00, 0X50);
            registers.Accumulator.State = 0X90;

            var adc = (IInstructionLogicWithAddressingMode)new ADC();
            adc.Execute(immediateAddressingMode, ram, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0XE0));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(false));
        }

        [Test]
        public void UnsignedCarryButNoSignedOverflowPositiveResult()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0X00, 0X50);
            registers.Accumulator.State = 0XD0;

            var adc = (IInstructionLogicWithAddressingMode)new ADC();
            adc.Execute(immediateAddressingMode, ram, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0X20));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(true));
        }

        [Test]
        public void NoUnsignedCarryOrSignedOverflowNegativeResult2()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0X00, 0XD0);
            registers.Accumulator.State = 0X10;

            var adc = (IInstructionLogicWithAddressingMode)new ADC();
            adc.Execute(immediateAddressingMode, ram, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0XE0));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(false));
        }

        [Test]
        public void UnsignedCarryButNoSignedOverflowPositiveResult2()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0X00, 0XD0);
            registers.Accumulator.State = 0X50;

            var adc = (IInstructionLogicWithAddressingMode)new ADC();
            adc.Execute(immediateAddressingMode, ram, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0X20));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(true));
        }

        [Test]
        public void UnsignedCarryAndSignedOverflowPositiveResult()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0X00, 0XD0);
            registers.Accumulator.State = 0X90;

            var adc = (IInstructionLogicWithAddressingMode)new ADC();
            adc.Execute(immediateAddressingMode, ram, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0X60));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(true));
        }

        [Test]
        public void UnsignedCarryButNoSignedOverflowNegativeResult()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0X00, 0XD0);
            registers.Accumulator.State = 0XD0;

            var adc = (IInstructionLogicWithAddressingMode)new ADC();
            adc.Execute(immediateAddressingMode, ram, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0XA0));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(true));
        }

        [Test]
        public void AddingTwoNegativeValues()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0X00, 0X81);
            registers.Accumulator.State = 0X81;

            var adc = (IInstructionLogicWithAddressingMode)new ADC();
            adc.Execute(immediateAddressingMode, ram, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0X02));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(true));
        }
    }
}
