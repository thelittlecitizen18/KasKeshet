using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Server
{
    class RecivedAndSendMessage
    {
        public void ReciveMsg(TcpClient client)
        {
            int i;
            Byte[] bytes = new Byte[1024];
            NetworkStream stream = client.GetStream();
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                string data = Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Recived:{0}", data);

                data = data.ToUpper();
                byte[] msg = Encoding.ASCII.GetBytes(data);

                stream.Write(msg, 0, msg.Length);
                Console.WriteLine("Send: {0}", data);
            }
            client.Close();
        }
    }
}
