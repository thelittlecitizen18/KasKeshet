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
        public TcpClient MakeAConnection()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 11000;
            TcpClient client = new TcpClient();
            client.Connect(ip, port);
            return client;
        }

        public string RegistrationToServer(TcpClient client)
        {
            StreamWriter sW = new StreamWriter(client.GetStream());
            sW.AutoFlush = true;
            Console.WriteLine("Please Enter A User Name:");
            string userName = Console.ReadLine();
            sW.WriteLine(userName);
            Console.WriteLine("{0} connected!!", userName);
            return userName;
        }
    }
}
