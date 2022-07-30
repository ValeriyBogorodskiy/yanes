using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Logic;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class EORLogic
    {
        [Test]
        public void PositiveResult()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0X00, 0b0101_0101);
            registers.Accumulator.State = 0b0010_1010;

            var eor = (IInstructionLogicWithAddressingMode)new EOR();
            eor.Execute(immediateAddressingMode, ram, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0b0111_1111));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }

        [Test]
        public void ZeroResult()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0X00, 0b1111_1111);
            registers.Accumulator.State = 0b1111_1111;

            var eor = (IInstructionLogicWithAddressingMode)new EOR();
            eor.Execute(immediateAddressingMode, ram, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0b0000_00000));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(true));
        }

        [Test]
        public void NegativeResult()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0X00, 0b0000_0000);
            registers.Accumulator.State = 0b1000_0001;

            var eor = (IInstructionLogicWithAddressingMode)new EOR();
            eor.Execute(immediateAddressingMode, ram, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0b1000_0001));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }
    }
}
