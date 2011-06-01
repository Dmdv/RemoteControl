using GTalk.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    /// <summary>
    /// UnitTests.
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
        public void TestPeerLogin()
        {
            //var peer = new Peer("141700.info@gmail.com");
            //peer.Password = "****";
            //peer.Login();
            //peer.Logout();
        }
    }
}