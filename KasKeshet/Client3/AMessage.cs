using System;
using System.Collections.Generic;
using System.Text;

namespace Client1
{
    [Serializable]
    public class AMessage
    {  
        public string Source { get; set; }
        public List<int> Destination { get; set; }
        public string Message { get; set; }
        public MessageType Type { get; }
        

        public AMessage(string source, List<int> destination, string message, MessageType type)
        {
            Source = source;
            Destination = destination;
            Message = message;
            Type = type;
        }
    }
}
