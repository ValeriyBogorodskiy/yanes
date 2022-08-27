using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Opcodes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class BCCLogic
    {
        [Test]
        public void BranchNotTaken()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, true);
            registers.ProgramCounter.State = 0x6001;
            bus.Write8Bit(0x6001, 0x01);

            var bcc = (IInstruction)new BCC(0x10);
            var cycles = bcc.Execute(bus, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x6002));
            Assert.That(cycles, Is.EqualTo(2));
        }

        [Test]
        public void BranchTaken()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, false);
            registers.ProgramCounter.State = 0x6001;
            bus.Write8Bit(0x6001, 0x01);

            var bcc = (IInstruction)new BCC(0x10);
            var cycles = bcc.Execute(bus, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x6003));
            Assert.That(cycles, Is.EqualTo(3));
        }
    }
}
