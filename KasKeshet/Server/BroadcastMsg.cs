using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Collections.Generic;

namespace Server
{
    class BroadcastMsg
    {
        static readonly object _lock = new object();

        public void Broadcast(string data, int idSender, ManageClients manage)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);

            lock (_lock)
            {
                foreach (KeyValuePair<int, TcpClient> clientInBroadcast in manage.ClientList)
                {
                    if (clientInBroadcast.Key != idSender)
                    {
                        NetworkStream stream = clientInBroadcast.Value.GetStream();

                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }
    }
}

