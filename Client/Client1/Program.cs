using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress hostIP = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(hostIP, 8000);
            Socket socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketClient.Connect(endPoint);
            string S = "";
            while (!S.Contains(","))
            {
                byte[] byted = new byte[1024];
                int length = socketClient.Receive(byted);
                int lengthOfFile = BitConverter.ToInt32(byted, 0);
                string fileName = Encoding.ASCII.GetString(byted, 4, lengthOfFile);
                S = Encoding.ASCII.GetString(byted, lengthOfFile + 4, 50);
                Console.WriteLine(S);
            }
        }
    }
}
