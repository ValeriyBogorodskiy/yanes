﻿using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions;
using NesEmulatorCPU.Instructions.Opcodes;
using NesEmulatorCPU.Registers;


namespace NesEmulatorCPU.Test.Instructions
{
    [TestFixture]
    internal class INCLogic
    {
        [Test]
        public void PositiveBeforePositiveAfter()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            bus.Write8Bit(0x00, 0x01);

            var inc = (IInstructionLogicWithAddressingMode)new INC();
            inc.Execute(immediateAddressingMode, bus, registers);

            Assert.That(bus.Read8bit(0x00), Is.EqualTo(0x02));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }

        [Test]
        public void ZeroBeforePositiveAfter()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            bus.Write8Bit(0x00, 0x00);

            var inc = (IInstructionLogicWithAddressingMode)new INC();
            inc.Execute(immediateAddressingMode, bus, registers);

            Assert.That(bus.Read8bit(0x00), Is.EqualTo(0x01));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }

        [Test]
        public void NegativeBeforeNegativeAfter()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            bus.Write8Bit(0x00, 0x82);

            var inc = (IInstructionLogicWithAddressingMode)new INC();
            inc.Execute(immediateAddressingMode, bus, registers);

            Assert.That(bus.Read8bit(0x00), Is.EqualTo(0x83));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(true));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(false));
        }

        [Test]
        public void NegativeBeforeZeroAfter()
        {
            var bus = new Bus();
            var registers = new RegistersProvider();
            var immediateAddressingMode = new Immediate();

            bus.Write8Bit(0x00, 0xFF);

            var inc = (IInstructionLogicWithAddressingMode)new INC();
            inc.Execute(immediateAddressingMode, bus, registers);

            Assert.That(bus.Read8bit(0x00), Is.EqualTo(0x00));

            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative), Is.EqualTo(false));
            Assert.That(registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero), Is.EqualTo(true));
        }
    }
}
