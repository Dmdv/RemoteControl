using System;
using System.Windows.Forms;
using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.protocol.iq.roster;
using agsXMPP.Xml.Dom;
using Message = agsXMPP.protocol.client.Message;

namespace GTalk.Prototype
{
    public partial class MainForm : Form
    {
        private readonly XmppClientConnection _xmppCon = new XmppClientConnection();

        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            listEvents.Items.Clear();
            _xmppCon.OnLogin += OnLogin;
            _xmppCon.OnRosterStart += OnRosterStart;
            _xmppCon.OnRosterEnd += OnRosterEnd;
            _xmppCon.OnRosterItem += OnRosterItem;
            _xmppCon.OnPresence += OnPresence;
            _xmppCon.OnAuthError += OnAuthError;
            _xmppCon.OnError += OnError;
            _xmppCon.OnClose += OnClose;
            _xmppCon.OnMessage += OnMessage;
        }

        private void OnMessage(object sender, Message msg)
        {
            if (msg.Body == null) return;

            if (InvokeRequired)
            {
                BeginInvoke(new MessageHandler(OnMessage), new[] {sender, msg});
                return;
            }

            listEvents.Items.Add(String.Format("OnMessage from:{0} type:{1}", msg.From.Bare, msg.Type));
            listEvents.Items.Add(msg.Body);
            listEvents.SelectedIndex = listEvents.Items.Count - 1;
        }

        private void OnClose(object sender)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ObjectHandler(OnClose), new[] {sender});
                return;
            }
            listEvents.Items.Add("OnClose Connection closed");
            listEvents.SelectedIndex = listEvents.Items.Count - 1;
        }

        private void OnError(object sender, Exception ex)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ErrorHandler(OnError), new[] {sender, ex});
                return;
            }
            listEvents.Items.Add("OnError");
            listEvents.SelectedIndex = listEvents.Items.Count - 1;
        }

        private void OnAuthError(object sender, Element e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new XmppElementHandler(OnAuthError), new[] {sender, e});
                return;
            }
            listEvents.Items.Add("OnAuthError");
            listEvents.SelectedIndex = listEvents.Items.Count - 1;
        }

        private void OnPresence(object sender, Presence pres)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new PresenceHandler(OnPresence), new[] {sender, pres});
                return;
            }

            const string Msg = "Received Presence from:{0} show:{1} status:{2}";
            listEvents.Items.Add(String.Format(Msg, pres.From, pres.Show, pres.Status));
            listEvents.SelectedIndex = listEvents.Items.Count - 1;
        }

        private void OnRosterItem(object sender, RosterItem item)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new XmppClientConnection.RosterHandler(OnRosterItem), new[] {sender, item});
                return;
            }
            listEvents.Items.Add(String.Format("Received Contact {0}", item.Jid.Bare));
            listEvents.SelectedIndex = listEvents.Items.Count - 1;
        }

        private void OnRosterEnd(object sender)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ObjectHandler(OnRosterEnd), new[] {sender});
                return;
            }
            listEvents.Items.Add("OnRosterEnd");
            listEvents.SelectedIndex = listEvents.Items.Count - 1;

            // Send our own presence to teh server, so other epople send us online
            // and the server sends us the presences of our contacts when they are
            // available
            _xmppCon.SendMyPresence();
        }

        private void OnRosterStart(object sender)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ObjectHandler(OnRosterStart), new[] {sender});
                return;
            }
            listEvents.Items.Add("OnRosterStart");
            listEvents.SelectedIndex = listEvents.Items.Count - 1;
        }

        private void OnLogin(object sender)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ObjectHandler(OnLogin), new[] {sender});
                return;
            }
            listEvents.Items.Add("OnLogin");
            listEvents.SelectedIndex = listEvents.Items.Count - 1;
        }

        private void CmdLoginClick(object sender, EventArgs e)
        {
            var jidUser = new Jid(txtJabberId.Text);

            _xmppCon.Username = jidUser.User;
            _xmppCon.Server = jidUser.Server;
            _xmppCon.Password = txtPassword.Text;
            _xmppCon.AutoResolveConnectServer = true;

            _xmppCon.Open();
        }

        private void CmdLogoutClick(object sender, EventArgs e)
        {
            _xmppCon.Close();
        }

        private void CmdSendClick(object sender, EventArgs e)
        {
            // Send a message
            var msg = new Message
                          {
                              Type = MessageType.chat,
                              To = new Jid(txtJabberIdReceiver.Text),
                              Body = txtMessage.Text
                          };

            _xmppCon.Send(msg);

            txtMessage.Text = string.Empty;
        }
    }
}