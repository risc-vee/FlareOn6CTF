using System;
using System.IO;
using System.Drawing;

namespace bmpshow
{
    class Program
    {
        static void Main(string[] args)
        {
            switch (args[0])
            {
                case "e":
                {
                    string OriginalImagePath = Path.GetFullPath(args[1]);
                    string FileToHidePath = Path.GetFullPath(args[2]);
                    string OutputFilePath = args[3];
                    byte[] FileToHide = File.ReadAllBytes(FileToHidePath);
                    Bitmap OriginalImage = new Bitmap(OriginalImagePath);
                    byte[] EncryptedFile = DataHide.EncryptData(FileToHide);
                    DataHide.EncodeData(OriginalImage, EncryptedFile);
                    OriginalImage.Save(OutputFilePath);
                    break;
                }
                case "d":
                {
                    string ImageToDecodePath = Path.GetFullPath(args[1]);
                    string DecodedImagePath = Path.GetFullPath(args[2]);
                    byte[] Decoded = DataShow.DecodeData(new Bitmap(ImageToDecodePath));
                    byte[] Decrypted = DataShow.DecryptData(Decoded);
                    File.WriteAllBytes(DecodedImagePath, Decrypted);
                    break;
                }
                default:
                    Console.WriteLine("usage: bmpshow e <OriginalImage> <FileToHide> <OutputImage>");
                    Console.WriteLine("\tbmpshow d <ImageToDecode> <DecodedImage>");
                    break;
            }
        }
    }
}
