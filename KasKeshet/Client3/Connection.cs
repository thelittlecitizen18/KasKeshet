using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Client1
{
    class Connection
    {
        public IPAddress Ip { get; set; }
        public int Port { get; set; }



        public void MakeAConnection()
        {

        }



        //IPAddress ip = IPAddress.Parse("127.0.0.1");
        //int port = 11000;
        //TcpClient client = new TcpClient();
        //client.Connect(ip, port);
    }
}
