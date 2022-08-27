using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Opcodes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class BNELogic
    {
        [Test]
        public void BranchNotTaken()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, true);
            registers.ProgramCounter.State = 0x601;
            bus.Write8Bit(0x601, 0x01);

            var bne = (IInstruction)new BNE(0x10);
            var cycles = bne.Execute(bus, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x602));
            Assert.That(cycles, Is.EqualTo(2));
        }

        [Test]
        public void BranchTaken()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, false);
            registers.ProgramCounter.State = 0x601;
            bus.Write8Bit(0x601, 0x01);

            var bne = (IInstruction)new BNE(0x10);
            var cycles = bne.Execute(bus, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x603));
            Assert.That(cycles, Is.EqualTo(3));
        }
    }
}
