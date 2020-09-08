using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Connect
    {
      
        public TcpListener MakeConnection()
        {
            TcpListener serverSocket = new TcpListener(IPAddress.Any, 11000);
            serverSocket.Start();
            return serverSocket;
        }

    }
}
