using System;
using System.Drawing;

namespace bmpshow
{
    class DataShow
    {
        public static byte[] DecodeData(Bitmap bm)
        {
            int DecodedBytesLength = (bm.Width * bm.Height);
            byte[] DecodedBytes = new byte[DecodedBytesLength];
            int DecodedBytesIndex = 0;
            for (int w = 0; w < bm.Width; w++)
            {
                for (int h = 0; h < bm.Height; h++)
                {
                    Color Pixel = bm.GetPixel(w, h);
                    byte B = (byte)((Pixel.B & 3) << 6);
                    byte G = (byte)((Pixel.G & 7) << 3);
                    byte R = (byte)(Pixel.R & 7);
                    byte Decoded = (byte)(B | G | R);
                    DecodedBytes[DecodedBytesIndex] = Decoded;
                    ++DecodedBytesIndex;
                }
            }
            return DecodedBytes;
        }

        public static byte[] DecryptData(byte[] Data)
        {
            byte[] Decrypted = new byte[Data.Length];
            int HashIndex = 0;
            for (int i = 0; i < Data.Length; i++)
            {
                byte EncryptedByte = Data[i];
                EncryptedByte = Utils.RotateLeft(EncryptedByte, 3);

                byte Hash1 = Utils.CalculateHash(HashIndex);
                HashIndex += 1;
                byte Hash2 = Utils.CalculateHash(HashIndex);
                HashIndex += 1;

                EncryptedByte = Utils.XOR(EncryptedByte, Hash2);
                EncryptedByte = Utils.RotateRight(EncryptedByte, 7);
                EncryptedByte = Utils.XOR(EncryptedByte, Hash1);
                Decrypted[i] = EncryptedByte;
            }
            return Decrypted;
        }
    }
}
