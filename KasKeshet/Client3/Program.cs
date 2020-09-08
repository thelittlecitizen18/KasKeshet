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
            Connection connect = new Connection();
            BroadcastMessages broadcast = new BroadcastMessages();
            var client = connect.MakeAConnection();
            var userName = connect.RegistrationToServer(client);

            Console.WriteLine("Hi {0}, Welcome To KasKeshet" +
                "\n 1 -Send A Global Messages For All The Clients Registered" +
                "\n 2- Send A Private Massage For Registered Client" +
                "\n 3- Create A Group Chat");
            int choise = Convert.ToInt32(Console.ReadLine());
            switch (choise)
            {
                case 1:
                    broadcast.SendMsg(userName, client);
                    break;
                case 2:

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

