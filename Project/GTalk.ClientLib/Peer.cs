using System;
using System.Collections.Generic;
using System.Linq;
using agsXMPP;

namespace GTalk.Client
{
    public sealed class Peer
    {
        private readonly XmppClientConnection _xmppCon = new XmppClientConnection();
        private readonly PeersCollection _peers = new PeersCollection();
        private readonly Jid _user;

        public Peer(string user)
        {
            user = GTalkRules.FixPostfix(user);
            _user = new Jid(user);
        }

        public string Password { get; set; }

        public event EventHandler<MessageArgs> OnMessage;

        /// <summary>
        ///   login
        /// </summary>
        public void Login()
        {
            _xmppCon.Username = _user.User;
            _xmppCon.Server = _user.Server;
            _xmppCon.Password = Password;
            _xmppCon.AutoResolveConnectServer = true;

            _xmppCon.Open();
        }

        /// <summary>
        ///   logout.
        /// </summary>
        public void Logout()
        {
            _xmppCon.Close();
        }

        /// <summary>
        ///   Add recipient.
        /// </summary>
        public void AddPeer(string peer)
        {
            peer = GTalkRules.FixPostfix(peer);

            if (!_peers.Contains(peer))
            {
                _peers.Add(peer);
            }
        }

        /// <summary>
        ///   Add recipient.
        /// </summary>
        public void AddPeer(IEnumerable<string> peers)
        {
            var list = peers.Select(GTalkRules.FixPostfix);

            foreach (var peer in list.Where(x => !_peers.Contains(x)))
            {
                _peers.Add(peer);
            }
        }

        /// <summary>
        ///   Add recipient.
        /// </summary>
        public void AddPeer(Peer peer)
        {
            if (!_peers.Contains(peer._user.Bare))
            {
                _peers.Add(peer._user.Bare);
            }
        }

        /// <summary>
        ///   Send message.
        /// </summary>
        public void SendMessage(string message)
        {
        }

        private void InvokeOnMessage(MessageArgs e)
        {
            var handler = OnMessage;
            if (handler != null) handler(this, e);
        }
    }
}