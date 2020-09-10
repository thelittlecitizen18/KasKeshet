using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Collections.Generic;

namespace Client1
{
    class Menu
    {
        public SendReciveMsg SendRecive1 { get; set; }

        SendReciveMsg SendRecive = new SendReciveMsg(new Dictionary<string, Thread>());


        public void MainMenu(TcpClient client, string userName, NetworkStream ns)
        {
            while (true)
            {
                Console.WriteLine("Hi {0}, Welcome To KasKeshet" +
                    "\n 1 -Send A Global Messages For All The Clients Registered" +
                    "\n 2- Send A Private Massage For Registered Client" +
                    "\n 3- Create A Group Chat", userName);
                int choise = Convert.ToInt32(Console.ReadLine());
                //return choise
                //CreateAMessage(choise, userName);

                switch (choise)
                {
                    case 1:
                        Console.WriteLine("Enter A Message:");
                        string message = Console.ReadLine();
                        AMessage send = new AMessage(userName, new List<int>() { }, message, MessageType.Public);
                        SendRecive.SendMsg(send, client, userName, ns);
                        break;

                    case 2:
                        Console.WriteLine("Enter The Recipient's ID:");
                        int recipientId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter A Message:");
                        string message1 = Console.ReadLine();
                        AMessage send1 = new AMessage(userName, new List<int>() { recipientId }, message1, MessageType.Private);
                        SendRecive.SendMsg(send1, client, userName, ns);
                        break;
                    case 3:
                        Console.WriteLine("Enter A Message:");
                        string message2 = Console.ReadLine();
                        Console.WriteLine("Enter How Many Recipient's:");
                        int numOfRecive = Convert.ToInt32(Console.ReadLine());
                        AMessage send2 = new AMessage(userName, GetDestinationIds(numOfRecive), message2, MessageType.Room);
                        SendRecive.SendMsg(send2, client, userName, ns);
                        break;

                    default:
                        Console.WriteLine("Please Choose A Valid Number");
                        break;
                        

                }
            }
        }

        public List<int> GetDestinationIds(int idCount)
        {
            List<int> ids = new List<int>();
            for (int i = 0; i < idCount; i++)
            {
                Console.WriteLine("Enter The Recipient's ID:");
                ids.Add(Convert.ToInt32(Console.ReadLine()));
            }
            return ids;
        }

    }

    

}


