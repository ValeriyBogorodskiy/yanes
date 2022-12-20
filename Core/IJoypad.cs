namespace YaNES.Core
{
    public interface IJoypad
    {
        void Write(byte value);

        byte Read();
    }
}
