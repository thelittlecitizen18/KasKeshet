using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Client1
{
    class SendMessages
    {
        public void SendMsg(string userName, TcpClient client)
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

        public void ReceiveData(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            byte[] receivedBytes = new byte[1024];
            int byte_count;

            while ((byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
            {
                Console.Write(Encoding.ASCII.GetString(receivedBytes, 0, byte_count));
            }
        }

        public int MainMenu()
        {
            Console.WriteLine("Hi {0}, Welcome To KasKeshet" +
                "\n 1 - for Send A Global Messages For All The Clients Registered" +
                "\n 2- for Send A Private Massage For Registered Client");
            int choice = Convert.ToInt32(Console.ReadLine());
            return choice;
        }
    }
}
