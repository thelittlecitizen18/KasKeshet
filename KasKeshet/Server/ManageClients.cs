﻿using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Collections.Generic;

namespace Server
{

    class ManageClients
    {
        static readonly object _lock = new object();
        public Dictionary<int, TcpClient> ClientList { get; set; }
        public Dictionary<int, string> UserList { get; set; }

        public ManageClients(Dictionary<int, TcpClient> clientList, Dictionary<int, string> userList)
        {
            ClientList = clientList;
            UserList = userList;
        }

        public void AddClientsToLists(TcpListener serverSocket)
        {
            int count = 1;
            while (true)
            {
                TcpClient client = serverSocket.AcceptTcpClient();
                lock (_lock) ClientList.Add(count, client);

                StreamReader sR = new StreamReader(client.GetStream());
                string userName = sR.ReadLine();
                Console.WriteLine("{0} connected!!, Id:{1}", userName, count);
                lock (_lock) UserList.Add(count, userName);
                string newClient = userName + " Join The App!, User Id:" + count;
                Broadcast(newClient, count);
                Thread clientHandler = new Thread(HandleClients);
                
                clientHandler.Start(count);
                count++;
            }
        }
        public void HandleClients(object o)
        {
            int id =(int) o;
            TcpClient client;

            lock (_lock) client = ClientList[id];

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

            lock (_lock) ClientList.Remove(id);
            Console.WriteLine("{0} disconnected!", UserList[id]);
            string disconnectedClient = UserList[id] + " disconnected :(";
            Broadcast(disconnectedClient, id);
            lock (_lock) UserList.Remove(id);
            client.Client.Shutdown(SocketShutdown.Both);
            client.Close();
        }


        public void Broadcast(string data, int idSender)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);

            lock (_lock)
            {
                foreach (KeyValuePair<int, TcpClient> clientInBroadcast in ClientList)
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
