using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Nancy;
using Nancy.Hosting.Self;

namespace MugatuServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new HostConfiguration();
            config.UrlReservations.CreateAutomatically = true;
            using (var host = new NancyHost(config, new Uri("http://localhost:80")))
            {
                host.Start();
                Console.WriteLine("Running on http://localhost:80");
                Console.ReadKey();
                host.Stop();
            }
        }
    }
    public class Module : NancyModule
    {
        public Module()
        {
            Post("/", _ => {
                byte[] postData = new byte[this.Request.Body.Length];
                this.Request.Body.Read(postData, 0, postData.Length);
                Console.WriteLine("Request Body: [{0}]", Encoding.ASCII.GetString(postData));
                byte[] ResponseData = new byte[0x2d];
                byte FillByte = (byte)'A';
                byte[] prefix = Encoding.ASCII.GetBytes("orange mocha frappuccino");
                int i = 0;
                for (i = 0; i < prefix.Length; i++)
                {
                    ResponseData[i] = (byte)(prefix[i] ^ 0x4d);
                }
                ResponseData[i] = 0 ^ 0x4d;//Null terminator
                i += 1;
                for (; i < ResponseData.Length; i++)
                {
                    ResponseData[i] = (byte)(FillByte);
                    FillByte += 2;
                }
                var B64Encoded = Convert.ToBase64String(ResponseData);
                Console.WriteLine("Responding with: [{0}]", B64Encoded);
                MemoryStream ResponseStream = new MemoryStream(Encoding.ASCII.GetBytes(B64Encoded));
                Console.WriteLine("=========================================================================");
                return this.Response.FromStream(ResponseStream, this.Request.Headers.ContentType);
            });
        }
    }
}
