using System;
using System.Drawing;

namespace bmpshow
{
    class DataHide
    {
        public static void EncodeData(Bitmap bm, byte[] data)
        {
            int num = 0;
            for (int i = 0; i < bm.Width; i++)
            {
                for (int j = 0; j < bm.Height; j++)
                {
                    bool flag = num > data.Length - 1;
                    if (flag)
                    {
                        break;
                    }
                    Color pixel = bm.GetPixel(i, j);
                    int red = ((int)pixel.R & 0xF8) | ((int)data[num] & 7);
                    int green = ((int)pixel.G & 0xF8) | (data[num] >> 3 & 7);
                    int blue = ((int)pixel.B & 0xFC) | (data[num] >> 6 & 3);
                    Color color = Color.FromArgb(0, red, green, blue);
                    bm.SetPixel(i, j, color);
                    num += 1;
                }
            }
        }

        public static byte[] EncryptData(byte[] data)
        {
            byte[] Encrypted = new byte[data.Length];
            int HashIndex = 0;
            for (int i = 0; i < data.Length; i++)
            {
                byte ByteVar = data[i];
                Console.WriteLine("ByteToEncrypt: data[{0:X}] = {1:X}", i, ByteVar);
                byte Hash1 = Utils.CalculateHash(HashIndex);
                Console.WriteLine("HashIndex -> {0:X} | Hash1 -> {1:X}", HashIndex, Hash1);
                HashIndex += 1;

                Console.Write("ByteVar({0:X}) XOR Hash1({1:X}) -> ", ByteVar, Hash1);
                ByteVar = Utils.XOR(ByteVar, Hash1);
                Console.WriteLine("{0:X}", ByteVar);
                Console.Write("ByteVar({0:X}) ROL ({1:X}) -> ", ByteVar, 7);
                ByteVar = Utils.RotateLeft(ByteVar, 7);
                Console.WriteLine("{0:X}", ByteVar);

                byte Hash2 = Utils.CalculateHash(HashIndex);
                Console.WriteLine("HashIndex -> {0:X} | Hash2 -> {1:X}", HashIndex, Hash2);
                HashIndex += 1;

                Console.Write("ByteVar({0:X}) XOR Hash2({1:X}) -> ", ByteVar, Hash2);
                ByteVar = Utils.XOR(ByteVar, Hash2);
                Console.WriteLine("{0:X}", ByteVar);
                Console.Write("ByteVar({0:X}) ROR ({1:X}) -> ", ByteVar, 3);
                ByteVar = Utils.RotateRight(ByteVar, 3);
                Console.WriteLine("{0:X}", ByteVar);

                Encrypted[i] = ByteVar;
                Console.WriteLine("Encrypted[{0:X}] = {1:X}", i, ByteVar);
                Console.WriteLine("================================================");
            }
            return Encrypted;
        }

        
    }
}
