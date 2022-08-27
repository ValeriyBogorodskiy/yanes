using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Opcodes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class CMPLogic
    {
        [Test]
        public void RegisterIsLesserThanMemory()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            registers.Accumulator.State = 5;
            bus.Write8Bit(0x00, 10);

            var cmp = (IInstructionLogicWithAddressingMode)new CMP();
            cmp.Execute(immediateAddressingMode, bus, registers);

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(false));
        }

        [Test]
        public void RegisterIsEqualToMemory()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            registers.IndexRegisterX.State = 20;
            bus.Write8Bit(0x00, 20);

            var cpx = (IInstructionLogicWithAddressingMode)new CPX();
            cpx.Execute(immediateAddressingMode, bus, registers);

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(true));
        }

        [Test]
        public void RegisterIsGreaterThanMemory()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            registers.IndexRegisterY.State = 100;
            bus.Write8Bit(0x00, 20);

            var cpy = (IInstructionLogicWithAddressingMode)new CPY();
            cpy.Execute(immediateAddressingMode, bus, registers);

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry), Is.EqualTo(true));
        }
    }
}
