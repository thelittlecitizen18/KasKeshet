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
        public Dictionary<string, Thread> ThreadList { get; set; }

        public SendReciveMsg(Dictionary<string, Thread> threadList)
        {
            ThreadList = threadList;
        }

        public NetworkStream RecivedMsg(TcpClient client, string userName)
        {
            while (true)
            {
                Thread thread = new Thread(startThread => ReceiveData((TcpClient)startThread));
                thread.Start(client);
                NetworkStream ns = client.GetStream();
                ThreadList.Add(userName, thread);
                Console.WriteLine("log: Thread create");
                return ns;
            }
           
            
        }
        //public AMessage ClientSend (AMessage msgInfo)
        //{
        //    Console.Write("You:");
        //    string Console.ReadLine();
        //    AMessage msgFull = new AMessage(msgInfo.Source, msgInfo.)


        //}

        public void SendMsg( AMessage sendMsg, TcpClient client, string userName, NetworkStream ns)
        {
            //Thread thread = new Thread(startThread => ReceiveData((TcpClient)startThread));
            //thread.Start(client);
            //NetworkStream ns = client.GetStream();

            while (true)
            {
                string exitChat = "@ExitChat";
                string clientRespons = sendMsg.Message;
                if (string.Equals(exitChat, clientRespons))
                {
                    break;
                }
 
                string aMessageJason = JsonConvert.SerializeObject(sendMsg, Formatting.Indented);
                byte[] buffer = Encoding.ASCII.GetBytes(aMessageJason);
                ns.Write(buffer, 0, buffer.Length);

                sendMsg.Message = Console.ReadLine();




            }

            client.Client.Shutdown(SocketShutdown.Send);
            ThreadList[userName].Join();
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
