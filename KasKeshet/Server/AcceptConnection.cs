using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Server
{
    class AcceptConnection
    {
        
        public TcpClient AccceptConnection(TcpListener server)
        {
            Console.WriteLine("Wating For Connection.....");
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Connected!");
            return client;

        }
    }
}
