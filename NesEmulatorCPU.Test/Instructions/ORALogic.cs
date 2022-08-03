using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Opcodes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class ORALogic
    {
        [Test]
        public void NegativeResult()
        {
            var ram = new RAM();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            ram.Write8Bit(0x00, 0b1000_1111);
            registers.Accumulator.State = 0b0011_0001;

            var ora = (IInstructionLogicWithAddressingMode)new ORA();
            ora.Execute(immediateAddressingMode, ram, registers);

            Assert.That(registers.Accumulator.State, Is.EqualTo(0b1011_1111));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }
    }
}
