using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Client1
{
    class SendReciveMsg
    {
        public void SendMsg(AMessage SendMsg, TcpClient client)
        {
            Thread thread = new Thread(startThread => ReceiveData((TcpClient)startThread));
            thread.Start(client);
            NetworkStream ns = client.GetStream();

            while (true)
            {
                string exitChat = "@ExitChat";
                Console.WriteLine("");
                string clientRespons = (Console.ReadLine());
                if (string.Equals(exitChat, clientRespons))
                {
                    break;
                }
                string sendMsg = SendMsg.Source + ":" + clientRespons;
                AMessage aMessage = new AMessage(SendMsg.Source, new List<int>()
                {

                }, sendMsg, MessageType.Private);
 
                string aMessageJason = JsonConvert.SerializeObject(aMessage, Formatting.Indented);
                byte[] buffer = Encoding.ASCII.GetBytes(aMessageJason);
                ns.Write(buffer, 0, buffer.Length);
                /*byte[] buffer = Encoding.ASCII.GetBytes(sendMsg);
                ns.Write(buffer, 0, buffer.Length);*/

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
