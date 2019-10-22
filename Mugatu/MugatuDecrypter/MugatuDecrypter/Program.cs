using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MugatuDecrypter
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] FullFile = File.ReadAllBytes(Path.GetFullPath(args[0]));
            byte[] TwoDWORDs = new byte[8];
            Buffer.BlockCopy(FullFile, 0, TwoDWORDs, 0, 8);
            for (uint i2 = 0; i2 < 256; i2++)
            {
                for (uint i3 = 0; i3 < 256; i3++)
                {
                    for (uint i4 = 0; i4 < 256; i4++)
                    {
                        byte[] Decrypted = Decrypt(TwoDWORDs, new byte[] { 0x31, (byte)i2, (byte)i3, (byte)i4 });
                        if (Encoding.ASCII.GetString(Decrypted).StartsWith("GIF8"))
                        {
                            Decrypted = Decrypt(FullFile, new byte[] { 0x31, (byte)i2, (byte)i3, (byte)i4 });
                            File.WriteAllBytes(Path.GetFullPath(args[1]), Decrypted);
                        }
                    }
                }
            }
        }

        static byte[] Decrypt(byte[] EncryptedGIF, byte[] DecryptionKey)
        {
            uint DecryptionCounter = 0x20;
            uint Op2 = 0;
            uint XorKey1;
            uint XorKey2;

            uint EncryptedFirstDWORD;
            uint EncryptedSecondDWORD;

            MemoryStream EncryptedGIFStream = new MemoryStream(EncryptedGIF);
            MemoryStream DecryptedGIFStream = new MemoryStream(EncryptedGIFStream.Capacity);

            BinaryReader GIFBinaryReader = new BinaryReader(EncryptedGIFStream);
            BinaryWriter GIFBinaryWriter = new BinaryWriter(DecryptedGIFStream);

            DecryptionCounter = 0x20;
            Op2 = 0;
            do
            {
                Op2 = Op2 - 0x61C88647;
                DecryptionCounter -= 1;
            }
            while (DecryptionCounter != 0);
            do
            {
                try
                {
                    EncryptedFirstDWORD = GIFBinaryReader.ReadUInt32();
                    EncryptedSecondDWORD = GIFBinaryReader.ReadUInt32();
                }
                catch (EndOfStreamException EndOfStreamEx)
                {
                    break;
                }

                DecryptionCounter = 0x20;
                Op2 = 0;
                do
                {
                    Op2 = Op2 - 0x61C88647;
                    DecryptionCounter -= 1;
                }
                while (DecryptionCounter != 0);

                DecryptionCounter = 0x20;
                uint FirstDWORDComputed = 0;
                uint EncryptedSecondDWORDPrev = 0;
                uint SecondDWORDComputed = 0;
                uint EncryptedFirstDWORDPrev = 0;

                do
                {
                    FirstDWORDComputed = (EncryptedFirstDWORD << 4 ^ EncryptedFirstDWORD >> 5) + EncryptedFirstDWORD;
                    XorKey2 = DecryptionKey[(Op2 >> 0xb & 3)] + Op2;
                    EncryptedSecondDWORDPrev = EncryptedSecondDWORD - (FirstDWORDComputed ^ XorKey2);
                    SecondDWORDComputed = (EncryptedSecondDWORDPrev << 4 ^ EncryptedSecondDWORDPrev >> 5) + EncryptedSecondDWORDPrev;
                    XorKey1 = DecryptionKey[((Op2 + 0x61C88647) & 3)] + (Op2 + 0x61C88647);
                    EncryptedFirstDWORDPrev = EncryptedFirstDWORD - (SecondDWORDComputed ^ XorKey1);
                    EncryptedFirstDWORD = EncryptedFirstDWORDPrev;
                    EncryptedSecondDWORD = EncryptedSecondDWORDPrev;
                    Op2 = Op2 + 0x61C88647;
                    DecryptionCounter -= 1;
                } while (DecryptionCounter != 0);

                GIFBinaryWriter.Write(EncryptedFirstDWORDPrev);
                GIFBinaryWriter.Write(EncryptedSecondDWORDPrev);
            } while (DecryptedGIFStream.Length < DecryptedGIFStream.Capacity);

            return DecryptedGIFStream.ToArray();
        }

        static byte[] Encrypt(byte[] GIFToEncrypt, byte[] EncrpytionKey)
        {
            uint const20h = 0x20;
            uint XorKey1;
            uint XorKey2;
            uint SecondDWORD;
            uint Op2;
            uint FirstDWORD;
            MemoryStream GIFToEncryptStream = new MemoryStream(GIFToEncrypt);
            MemoryStream EncryptedGIF = new MemoryStream((int)GIFToEncryptStream.Length);

            BinaryReader GIFBinaryReader = new BinaryReader(GIFToEncryptStream);
            BinaryWriter GIFBinaryWriter = new BinaryWriter(EncryptedGIF);
            GIFBinaryWriter.Seek(0, SeekOrigin.Begin);
            int EncryptedBytesCount = 0;
            do
            {
                FirstDWORD = GIFBinaryReader.ReadUInt32();
                SecondDWORD = GIFBinaryReader.ReadUInt32();
                Op2 = 0;
                const20h = 0x20;
                do
                {
                    XorKey1 = (uint)(EncrpytionKey[(Op2 & 3)]) + Op2;
                    //Op2 = Op2 + 0x9e3779b9;//0x61C88647 * -1
                    Op2 = Op2 - 0x61C88647;
                    XorKey2 = (uint)(EncrpytionKey[(Op2 >> 0xb & 3)]) + Op2;
                    uint SecondDWORDComputed = ((SecondDWORD << 4 ^ SecondDWORD >> 5) + SecondDWORD);
                    FirstDWORD = FirstDWORD + (SecondDWORDComputed ^ XorKey1);
                    uint FirstDWORDComputed = ((FirstDWORD * 0x10 ^ FirstDWORD >> 5) + FirstDWORD);
                    SecondDWORD = SecondDWORD + (FirstDWORDComputed ^ XorKey2);
                    const20h -= 1;
                }
                while (const20h != 0);
                GIFBinaryWriter.Write(FirstDWORD);
                GIFBinaryWriter.Write(SecondDWORD);
                EncryptedBytesCount += 8;
            }
            while (EncryptedBytesCount < GIFToEncryptStream.Length);

            return EncryptedGIF.ToArray();
        }
    }
}