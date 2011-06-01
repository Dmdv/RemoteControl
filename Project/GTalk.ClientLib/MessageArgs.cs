using System;
using agsXMPP.protocol.client;

namespace GTalk.Client
{
    public class MessageArgs : EventArgs
    {
        public MessageArgs(Message msg)
        {
            Message = msg;
        }

        public Message Message { get; private set; }
    }
}