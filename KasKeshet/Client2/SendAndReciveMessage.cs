using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Client2
{
    class SendAndRecivedMessage
    {
        public void SendReciveMsg(TcpClient client)
        {
            Console.WriteLine("Write a Message:");
            string massage = Console.ReadLine();
            Byte[] data = Encoding.ASCII.GetBytes(massage);

            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
            Console.WriteLine("Send: {0}", massage);

            data = new Byte[1024];
            Int32 bytes = stream.Read(data, 0, data.Length);
            string responseData = Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Received: {0}", responseData);

        }


    }
}
