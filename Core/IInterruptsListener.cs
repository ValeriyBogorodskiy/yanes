namespace YaNES.Core
{
    public interface IInterruptsListener
    {
        void Trigger(Interrupt interrupt);
    }
}
