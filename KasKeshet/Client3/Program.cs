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
    class Program
    {
        static void Main(string[] args)
        {
            Connection connect = new Connection();
            SendReciveMsg sendRecive = new SendReciveMsg(new Dictionary<string, Thread>());
            //PrivateChat privateChat = new PrivateChat();
            var client = connect.MakeAConnection();
            var userName = connect.RegistrationToServer(client);
            var ns = sendRecive.RecivedMsg(client, userName);

            Menu menu = new Menu();
            var task = Task.Factory.StartNew(obj =>
            {
                menu.MainMenu(client, userName, ns);
                
            }, userName
            );

            //var msg = menu.MainMenu(userName);
            //sendRecive.SendMsg(msg, client, userName, ns);

        }

    }

    
}

