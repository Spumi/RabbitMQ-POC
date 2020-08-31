using System;
using System.Net;
using System.Net.Sockets;

namespace ChatRoomServer
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);

            server.Start();
            Console.WriteLine("Server started", Environment.NewLine);

            TcpClient client = server.AcceptTcpClient();

            Console.WriteLine("A client connected.");
        }
    }
}
