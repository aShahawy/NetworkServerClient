using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, 8000);
            Socket Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Server.Bind(ipEnd);
            Server.Listen(5);
            Socket newServer = Server.Accept();
            string path = args[0];
            Console.WriteLine(path);
            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            byte[] byte1 = BitConverter.GetBytes(path.Length);
            byte[] byte2 = Encoding.ASCII.GetBytes(path);
            Console.WriteLine("Ahmed_Hamed_Mohamed");
            Console.WriteLine("1");
            Console.WriteLine("CS");
            while (streamReader.Peek() != -1)
            {
                string line = streamReader.ReadLine();
                byte[] bytem = new byte[1024];
                byte[] byted = Encoding.ASCII.GetBytes(line);
                byte1.CopyTo(bytem, 0);
                byte2.CopyTo(bytem, 4);
                byted.CopyTo(bytem, 12);
                newServer.Send(bytem);
            }
            byte[] t = new byte[1024];
            byte[] comma = Encoding.ASCII.GetBytes(",");
            byte1.CopyTo(t, 0);
            byte2.CopyTo(t, 4);
            comma.CopyTo(t, 4 + path.Length);
            newServer.Send(t);
            newServer.Shutdown(SocketShutdown.Both);
            newServer.Close();
        }
    }
}

