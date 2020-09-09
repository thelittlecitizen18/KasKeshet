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
            SendReciveMsg broadcast = new SendReciveMsg();
            //PrivateChat privateChat = new PrivateChat();
            var client = connect.MakeAConnection();
            var userName = connect.RegistrationToServer(client);

            Menu menu = new Menu();
            menu.MainMenu(userName,client);

        }


        public static void Menu(string userName)
        {

            var task = Task.Factory.StartNew(obj =>
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


                        break;
                    case 2:
                        //privateChat.PrivateMsg(userName, client);
                        break;
                    case 3:

                        break;
                    default:
                        Console.WriteLine("Please Choose A Valid Number");
                        break;
                }
                //broadcast.SendMsg(userName, client);
            }, userName
            );
        }



        

    }
}

