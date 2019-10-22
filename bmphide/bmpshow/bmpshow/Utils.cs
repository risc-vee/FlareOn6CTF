using System;

namespace bmpshow
{
    class Utils
    {
        public static byte CalculateHash(int idx)
        {
            byte b = (byte)((long)(idx + 1) * (long)((ulong)309030853));
            byte k = (byte)((idx + 2) * 209897853);
            return XOR(b, k);
        }

        public static byte XOR(byte b, byte k)
        {
            for (int i = 0; i < 8; i++)
            {
                bool flag = (b >> i & 1) == (k >> i & 1);
                if (flag)
                {
                    b = (byte)((int)b & ~(1 << i) & 255);
                }
                else
                {
                    b = (byte)((int)b | (1 << i & 255));
                }
            }
            return b;
        }

        public static byte RotateLeft(byte b, int r)
        {
            for (int i = 0; i < r; i++)
            {
                byte b2 = (byte)((b & 128) / 128);
                b = (byte)((b * 2 & byte.MaxValue) + b2);
            }
            return b;
        }

        public static byte RotateRight(byte b, int r)
        {
            for (int i = 0; i < r; i++)
            {
                byte b2 = (byte)((b & 1) * 128);
                b = (byte)((b / 2 & byte.MaxValue) + b2);
            }
            return b;
        }
    }
}
