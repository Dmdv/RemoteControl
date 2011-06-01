using GTalk.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    /// <summary>
    ///   UnitTests.
    /// </summary>
    [TestClass]
    public class UnitTests
    {
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
        }

        [ClassCleanup]
        public static void MyClassCleanup()
        {
        }

        [TestInitialize]
        public void MyTestInitialize()
        {
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
        }

        [TestMethod]
        public void TestPeerCollection()
        {
            var peer = new Peer("zzz@gmail.com") { Password = "****" };
            peer.AddPeer(new[] { "recipient", "recipient", "recipient2", "fdfs@gmail.com" });
        }

        [TestMethod]
        public void TestPeerLogin()
        {
            var peer = new Peer("zzz@gmail.com") {Password = "****"};
            peer.Login();
            peer.Logout();
        }

        [TestMethod]
        public void TestPeerSendMessage()
        {
            var peer = new Peer("zzz@gmail.com") {Password = "****"};
            peer.Login();
            peer.AddPeer("recipient@gmail.com");
            peer.SendMessage("message");
            peer.Logout();
        }

        [TestMethod]
        public void TestMultiPeerSendMessage()
        {
            var peer = new Peer("zzz@gmail.com") {Password = "****"};
            peer.Login();
            peer.AddPeer(new[] {"recipient@gmail.com", "recipient2@gmail.com"});
            peer.SendMessage("message");
            peer.Logout();
        }

        [TestMethod]
        public void TestPeerSendReceiveMessage()
        {
            // Peer1.

            var peer1 = new Peer("zzz@gmail.com") {Password = "****"};
            peer1.Login();

            // Peer2.

            var peer2 = new Peer("yyy@gmail.com") {Password = "****"};
            peer2.Login();
            peer2.OnMessage += OnMessage;

            // Send message.

            peer1.AddPeer(peer2);
            peer1.SendMessage("message");

            // Logout.

            peer1.Logout();
            peer2.Logout();
        }

        private static void OnMessage(object sender, MessageArgs messageArgs)
        {
        }
    }
}