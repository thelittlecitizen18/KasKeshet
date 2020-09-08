using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Client1
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 11000;
            TcpClient client = new TcpClient();
            client.Connect(ip, port);

            StreamWriter sW = new StreamWriter(client.GetStream());
            sW.AutoFlush = true;
            Console.WriteLine("Please Enter A User Name:");
            string userName = Console.ReadLine();
            sW.WriteLine(userName);
            Console.WriteLine("{0} connected!!", userName);

            SendMsg(userName, client);

            //Thread thread = new Thread(startThread => ReceiveData((TcpClient)startThread));
            //thread.Start(client);

            //SendMsg(userName, client);
            //NetworkStream ns = client.GetStream();

            //while (true)
            //{
            //    Console.WriteLine("Me:");
            //    string sendMsg = userName + ":";
            //    string exitChat = "@ExitChat";
            //    sendMsg += (Console.ReadLine());
            //    if (string.Equals(exitChat, sendMsg))
            //    {
            //        break;
            //    }

            //    byte[] buffer = Encoding.ASCII.GetBytes(sendMsg);
            //    ns.Write(buffer, 0, buffer.Length);


            //}

            //client.Client.Shutdown(SocketShutdown.Send);
            //thread.Join();
            //ns.Close();
            //client.Close();
            //Console.WriteLine("disconnect from server!!");
            //Console.ReadKey();
        }

        public static void SendMsg(string userName, TcpClient client)
        {
            Thread thread = new Thread(startThread => ReceiveData((TcpClient)startThread));
            thread.Start(client);
            NetworkStream ns = client.GetStream();

            while (true)
            {
                string exitChat = "@ExitChat";
                string clientRespons = (Console.ReadLine());
                if (string.Equals(exitChat, clientRespons))
                {
                    break;
                }
                string sendMsg = userName + ":" + clientRespons;
                byte[] buffer = Encoding.ASCII.GetBytes(sendMsg);
                ns.Write(buffer, 0, buffer.Length);
                
            }

            client.Client.Shutdown(SocketShutdown.Send);
            thread.Join();
            ns.Close();
            client.Close();
            Console.WriteLine("disconnect from server!!");
            Console.ReadKey();

        }

        static void ReceiveData(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            byte[] receivedBytes = new byte[1024];
            int byte_count;

            while ((byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
            {
                Console.Write(Encoding.ASCII.GetString(receivedBytes, 0, byte_count));
            }
        }

        public static int MainMenu()
        {
            Console.WriteLine("Hi {0}, Welcome To KasKeshet" +
                "\n 1 - for Send A Global Messages For All The Clients Registered" +
                "\n 2- for Send A Private Massage For Registered Client");
            int choice = Convert.ToInt32(Console.ReadLine());
            return choice;
        }
    }
}

