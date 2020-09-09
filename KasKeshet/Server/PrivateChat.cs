//using System;
//using System.Net.Sockets;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using System.Threading;
//using System.Collections.Generic;
//using System.IO;
//namespace Server
//{
//    class PrivateChat
//    {
//        static readonly object _lock = new object();
//        public Dictionary<int, string> MyProperty { get; set; }

        
//        public void MakePrivateChat(ManageClients manageClients, TcpListener serverSocket)
//        {
//            int count = 1;
//            while (true)
//            {
//                TcpClient client = serverSocket.AcceptTcpClient();
//                lock (_lock) manageClients.ClientList.Add(count, client);

//                StreamReader sR = new StreamReader(client.GetStream());
//                string userName = sR.ReadLine();
//                Console.WriteLine("{0} connected!!, Id:{1}", userName, count);
//                lock (_lock) manageClients.UserList.Add(count, userName);
//                string newClient = userName + " Join The App!, User Id:" + count;
//                Broadcast(newClient, count);

//                StartConversation(count);


//                //Thread clientHandler = new Thread(HandleClients);
//                //clientHandler.Start(count);
//                count++;
//                //int idNum = count - 1; 
//                //return idNum;

//            }


//        }
//        public void AddClientsToLists(ManageClients manageClients, TcpListener serverSocket)
//        {
//            int count = 1;
//            while (true)
//            {
//                TcpClient client = serverSocket.AcceptTcpClient();
//                lock (_lock) manageClients.ClientList.Add(count, client);

//                StreamReader sR = new StreamReader(client.GetStream());
//                string userName = sR.ReadLine();
//                Console.WriteLine("{0} connected!!, Id:{1}", userName, count);
//                lock (_lock) manageClients.UserList.Add(count, userName);
//                string newClient = userName + " Join The App!, User Id:" + count;
//                Broadcast(newClient, count);

//                StartConversation(count);


//                //Thread clientHandler = new Thread(HandleClients);
//                //clientHandler.Start(count);
//                count++;
//                //int idNum = count - 1; 
//                //return idNum;

//            }
//        }

//        public void StartConversation(int idNum)
//        {
//            Thread clientHandler = new Thread(HandleClients);
//            clientHandler.Start(idNum);
//        }

//        public void HandleClients(object o)
//        {
//            int id = (int)o;
//            TcpClient client;

//            lock (_lock) client = manageClients.ClientList[id];

//            while (true)
//            {
//                NetworkStream stream = client.GetStream();
//                byte[] buffer = new byte[1024];
//                int byte_count = stream.Read(buffer, 0, buffer.Length);
//                if (byte_count == 0)
//                {
//                    break;
//                }
//                string data = Encoding.ASCII.GetString(buffer, 0, byte_count);
//                Broadcast(data, id);
//                Console.WriteLine(data);
//            }

//            lock (_lock) ClientList.Remove(id);
//            Console.WriteLine("{0} disconnected!", UserList[id]);
//            string disconnectedClient = UserList[id] + " disconnected :(";
//            Broadcast(disconnectedClient, id);
//            lock (_lock) UserList.Remove(id);
//            client.Client.Shutdown(SocketShutdown.Both);
//            client.Close();
//        }


//        public void Broadcast(string data, int idSender)
//        {
//            byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);

//            lock (_lock)
//            {
//                foreach (KeyValuePair<int, TcpClient> clientInBroadcast in ClientList)
//                {
//                    if (clientInBroadcast.Key != idSender)
//                    {
//                        NetworkStream stream = clientInBroadcast.Value.GetStream();

//                        stream.Write(buffer, 0, buffer.Length);
//                    }
//                }
//            }
//        }

//    }
//}
