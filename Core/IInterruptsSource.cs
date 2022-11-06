namespace YaNES.Core
{
    public interface IInterruptsSource
    {
        void Trigger(Interrupt interrupt);
    }
}
