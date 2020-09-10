using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.IO;

namespace Server
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Server");

            Connect connect = new Connect();
            ManageClients ManageClients = new ManageClients(new Dictionary<int, TcpClient>(), new Dictionary<int, string>());
            var serverSocket = connect.MakeConnection();

            ManageClients.AddClientsToLists(serverSocket);

            //while (true)
            //{
            //   ManageClients.StartConversation(count);
            //}
           

        }

        
    }
}
