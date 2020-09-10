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
        ////public SendReciveMsg sendMsg { get; set; }


        public AMessage MainMenu(string userName)
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
                    return send;

                case 2:
                    Console.WriteLine("Enter The Recipient's ID:");
                    int recipientId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter A Message:");
                    string message1 = Console.ReadLine();
                    AMessage send1 = new AMessage(userName, new List<int>() { recipientId }, message1, MessageType.Private);
                    return send1;
              
                default:
                    Console.WriteLine("Please Choose A Valid Number");
                    return null;
                    


            }
        }

    }
}

