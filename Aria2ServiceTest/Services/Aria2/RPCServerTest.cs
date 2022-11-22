using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Aria2ServiceTest.Services.Aria2;
using Aria2Service.Services.Aria2;
using Aria2Service.Services.ExecutableFileManagement;

[TestClass]
public class RPCServerTest
{
    public static RPCServer Server { get; set; } = RPCServerGenerator.Generate(new RPCServerConf());

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        Server.Run();
    }
    [ClassCleanup]
    public static void ClassCleanup()
    {
        if (Server.IsRunning)
        {
            Server.Terminate();
        }
    }


    [TestMethod]
    public void RPCServer_Runnable()
    {
        Thread.Sleep(5000);
        Assert.IsTrue(Server.IsRunning);
    }

    [TestMethod]
    public void RPCServer_SmoothShutdown()
    {
        Server.ShutdownAsync().Wait();
        Assert.IsTrue(Server.IsRunning);
    }
}




