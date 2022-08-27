﻿using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Opcodes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class BPLLogic
    {
        [Test]
        public void BranchNotTaken()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, true);
            registers.ProgramCounter.State = 0x6001;
            bus.Write8Bit(0x6001, 0x01);

            var bpl = (IInstruction)new BPL(0x10);
            var cycles = bpl.Execute(bus, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x6002));
            Assert.That(cycles, Is.EqualTo(2));
        }

        [Test]
        public void BranchTakenPositive()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x6001;
            bus.Write8Bit(0x6001, 0x01);

            var bpl = (IInstruction)new BPL(0x10);
            var cycles = bpl.Execute(bus, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x6003));
            Assert.That(cycles, Is.EqualTo(3));
        }

        [Test]
        public void BranchTakenNegative()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x6001;
            bus.Write8Bit(0x6001, 0xFE);

            var bpl = (IInstruction)new BPL(0x10);
            var cycles = bpl.Execute(bus, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x6000));
            Assert.That(cycles, Is.EqualTo(3));
        }

        [Test]
        public void BranchTakenPageCrossedPositive()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x60FE;
            bus.Write8Bit(0x60FE, 0x01);

            var bpl = (IInstruction)new BPL(0x10);
            var cycles = bpl.Execute(bus, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x6100));
            Assert.That(cycles, Is.EqualTo(4));
        }

        [Test]
        public void BranchTakenPageCrossedNegative()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();

            registers.ProgramCounter.State = 0x60FF;
            bus.Write8Bit(0x60FF, 0xFF);

            var bpl = (IInstruction)new BPL(0x10);
            var cycles = bpl.Execute(bus, registers);

            Assert.That(registers.ProgramCounter.State, Is.EqualTo(0x60FF));
            Assert.That(cycles, Is.EqualTo(4));
        }
    }
}
