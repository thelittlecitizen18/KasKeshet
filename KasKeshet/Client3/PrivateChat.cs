using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Client1
{
    class PrivateChat
    {
        public void MakePrivateChat(string userName, TcpClient client)
        {
            Thread thread = new Thread(startThread => ReceiveData((TcpClient)startThread));
            thread.Start(client);
            NetworkStream ns = client.GetStream();

        }
        public void SendMsg(string userName, TcpClient client)
        {
            Thread thread = new Thread(startThread => ReceiveData((TcpClient)startThread));
            thread.Start(client);
            NetworkStream ns = client.GetStream();

            while (true)
            {
                string returnMenu = "@BackToMenu";
                string clientRespons = (Console.ReadLine());
                if (string.Equals(returnMenu, clientRespons))
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
    }
}
