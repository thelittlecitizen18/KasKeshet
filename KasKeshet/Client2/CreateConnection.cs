using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Client2
{
    class CreateConnection
    {
        public TcpClient MakeConnection()
        {
            IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse("10.1.0.26"), 11000);
            TcpClient client = new TcpClient(iPEnd);
            return client;
        }
    }
}
