namespace YaNES.Core
{
    public abstract class Register8BitWith<T> : Register8Bit where T : struct, Enum
    {
        public bool Get(T flag) => (State & (byte)(object)flag) > 0;

        public void Set(T flag, bool value)
        {
            if (value)
                State |= (byte)(object)flag;
            else
                State &= (byte)~(byte)(object)flag;
        }
    }
}
