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
        static readonly object _lock = new object();
        static readonly Dictionary<int, TcpClient> list_clients = new Dictionary<int, TcpClient>();
        static readonly Dictionary<int, string> list_userNames = new Dictionary<int, string>();
        

        static void Main(string[] args)
        {
            int count = 1;
            

            TcpListener ServerSocket = new TcpListener(IPAddress.Any, 11000);
            ServerSocket.Start();

            while (true)
            {
                TcpClient client = ServerSocket.AcceptTcpClient();
                lock (_lock) list_clients.Add(count, client);

                StreamReader sR = new StreamReader(client.GetStream());
                string userName = sR.ReadLine();
                Console.WriteLine("{0} connected!!, Id:{1}", userName, count);
                lock (_lock) list_userNames.Add(count, userName);

                Thread clientHandler = new Thread(Handle_clients);
                clientHandler.Start(count);
                count++;
            }
        }



        public static void Handle_clients(object o)
        {
            int id = (int)o;
            TcpClient client;

            lock (_lock) client = list_clients[id];

            while (true)
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int byte_count = stream.Read(buffer, 0, buffer.Length);
                if (byte_count == 0)
                {
                    break;
                }

                string data = Encoding.ASCII.GetString(buffer, 0, byte_count);
                Broadcast(data, id);
                Console.WriteLine(data);
            }

            lock (_lock) list_clients.Remove(id);
            Console.WriteLine("{0} disconnected!", list_userNames[id]);
            lock (_lock) list_userNames.Remove(id);
            client.Client.Shutdown(SocketShutdown.Both);
            client.Close();
        }


        public static void Broadcast(string data, int idSender)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);

            lock (_lock)
            {
                foreach (KeyValuePair<int, TcpClient> clientInBroadcast in list_clients)
                {
                    if (clientInBroadcast.Key != idSender)
                    {
                        NetworkStream stream = clientInBroadcast.Value.GetStream();

                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }
    }
}
