using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Opcodes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class BVCLogic
    {
        [Test]
        public void BranchNotTaken()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Overflow, true);
            registers.ProgramCounter.State = 0x601;
            bus.Write8Bit(0x601, 0x01);

            var bvc = (IInstruction)new BVC(0x10);
            var cycles = bvc.Execute(bus, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x602));
            Assert.That(cycles, Is.EqualTo(2));
        }

        [Test]
        public void BranchTaken()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Overflow, false);
            registers.ProgramCounter.State = 0x601;
            bus.Write8Bit(0x601, 0x01);

            var bvc = (IInstruction)new BVC(0x10);
            var cycles = bvc.Execute(bus, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x603));
            Assert.That(cycles, Is.EqualTo(3));
        }
    }
}
