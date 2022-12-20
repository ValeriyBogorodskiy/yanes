using YaNES.Core;

namespace YaNes.Joypads
{
    public class Joypad : IJoypad
    {
        private const byte PressedButton = 1;
        private const byte NotPressedButton = 0;

        private bool strobeOn = false;
        private int nextButtonToRead = 0;
        private JoypadButton buttonsStatus = JoypadButton.None;


        public void Write(byte value)
        {
            strobeOn = value == 1;

            if (strobeOn)
                nextButtonToRead = 0;
        }

        public byte Read()
        {
            if (nextButtonToRead > 7)
                return PressedButton;

            var result = (buttonsStatus & (JoypadButton)nextButtonToRead) != JoypadButton.None ? PressedButton : NotPressedButton;

            if (!strobeOn)
                nextButtonToRead++;

            return result;
        }

        public void SetButtonPressed(JoypadButton button, bool value)
        {
            if (value)
                buttonsStatus |= button;
            else
                buttonsStatus &= ~button;
        }
    }
}