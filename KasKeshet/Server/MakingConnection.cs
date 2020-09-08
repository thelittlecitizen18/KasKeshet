using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class MakingConnection
    {
      

        public TcpListener MakeConnection()
        {
            int port = 11000;
            IPAddress ip = IPAddress.Parse("10.1.0.26");
            TcpListener server = new TcpListener(ip, port);
            server.Start();
            return server;            
        }

    }
}
