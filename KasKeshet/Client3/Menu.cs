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
        SendReciveMsg sendRecive = new SendReciveMsg();
        public  void MainMenu(string userName, TcpClient client)
        {
            Console.WriteLine("Hi {0}, Welcome To KasKeshet" +
                "\n 1 -Send A Global Messages For All The Clients Registered" +
                "\n 2- Send A Private Massage For Registered Client" +
                "\n 3- Create A Group Chat", userName);
            int choise = Convert.ToInt32(Console.ReadLine());
            switch (choise)
            {
                case 1:
                    Console.WriteLine("Enter A Message:");
                    string message = Console.ReadLine();
                    AMessage send = new AMessage(userName, new List<int>() { }, message, MessageType.Public);
                    sendRecive.SendMsg(send, client);
                    break;
                case 2:
                    Console.WriteLine("Enter The Recipient's ID:");
                    int recipientId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter A Message:");
                    string message1 = Console.ReadLine();
                    AMessage send1 = new AMessage(userName, new List<int>() {recipientId }, message1, MessageType.Private);
                    sendRecive.SendMsg(send1, client);
                    break;
                case 3:

                    break;
                default:
                    Console.WriteLine("Please Choose A Valid Number");
                    break;
            }
            
        }
    }
}
