namespace YaNES.Core
{
    public interface IJoypad
    {
        void Write(byte value);
        byte Read();
        void SetButtonPressed(JoypadButton button, bool value);
    }
}
