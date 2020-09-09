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
    class GroupChat
    {
        public void PrivateMsg(string userName, TcpClient client)
        {
            Thread thread = new Thread(startThread => ReceiveData((TcpClient)startThread));
            thread.Start(client);

            NetworkStream ns = client.GetStream();

            Console.WriteLine("Who Do You Send A Message? Please Enter Users Id (By The Format 1,2,3 ...)");
            int userid = Convert.ToInt32(Console.ReadLine());
            Console.Write("You:");



            while (true)
            {
                string returnMenu = "@BackToMenu";
                string clientRespons = (Console.ReadLine());
                if (string.Equals(returnMenu, clientRespons))
                {
                    break;
                }
                string sendMsg = userName + ":" + clientRespons;





                //AMessage aMessage = new AMessage(userName, new List<int>()
                //{
                //   userid
                //}, sendMsg);


                //string aMessageJason = JsonConvert.SerializeObject(aMessage, Formatting.Indented);
                //byte[] buffer = Encoding.ASCII.GetBytes(sendMsg);
                //ns.Write(buffer, 0, buffer.Length);

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

        public void SpereatesId()
        {
            string userid = "1 2 3 4 6 7 8 9";
            char[] speaator = { ' ' };
            string[] idlist = userid.Split(speaator, StringSplitOptions.RemoveEmptyEntries);

            foreach (var s in idlist)
            {
                int a = Convert.ToInt32(s);
                //id.Add(a);

            }
        }
    }
}
