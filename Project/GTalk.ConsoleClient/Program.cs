using System;
using System.Threading;
using agsXMPP;
using agsXMPP.protocol.client;

namespace GTalk.Prototype
{
    public static class Program
    {
        private static readonly AutoResetEvent _ev = new AutoResetEvent(false);
        private static readonly XmppClientConnection _xmppCon = new XmppClientConnection();

        static Program()
        {
            _xmppCon.OnLogin += delegate
                                    {
                                        Console.Out.WriteLine("Logged in...");
                                        _ev.Set();
                                    };

            _xmppCon.OnClose += delegate
                                    {
                                        Console.Out.WriteLine("Logged out...");
                                        _ev.Set();
                                    };

            _xmppCon.OnAuthError += (o, element) => Console.Out.WriteLine("Auth Error..." + element);

            _xmppCon.OnMessage += delegate(object o, Message msg)
                                      {
                                          Console.Out.WriteLine("Message body: '{0}'", msg.Body);
                                          Console.Out.WriteLine("----------------------------");
                                          Console.Out.WriteLine("Message: '{0}'", msg);
                                          Console.Out.WriteLine("Message lang: '{0}'", msg.Language);
                                          _ev.Set();
                                      };
        }

        public static void Main()
        {
            Login();
            _ev.WaitOne();

            SendChatMessage();
            //_ev.WaitOne();

            LogOut();
            _ev.WaitOne();
        }

        private static void Login()
        {
            var jidUser = new Jid("dm@gmail.com");

            _xmppCon.Username = jidUser.User;
            _xmppCon.Server = jidUser.Server;
            _xmppCon.Password = "qqq";
            _xmppCon.AutoResolveConnectServer = true;

            _xmppCon.Open();
        }

        private static void LogOut()
        {
            _xmppCon.Close();
        }

        private static void SendChatMessage()
        {
            var msg = new Message
            {
                Type = MessageType.chat,
                To = new Jid("dima@gmail.com"),
                Body = "Тестовое сообщение"
            };

            _xmppCon.Send(msg);
        }
    }
}