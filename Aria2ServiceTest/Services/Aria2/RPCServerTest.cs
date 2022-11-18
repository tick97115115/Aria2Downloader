using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Aria2ServiceTest.Services.Aria2;
using Aria2Service.Services.Aria2;
using Aria2Service.Services.ExecutableFileManagement;

[TestClass]
public class RPCServerTest
{
    public static RPCServerGenerator ServerGenerator = new RPCServerGenerator();
    public static ExecutableManagement RPCServer { get; set; } = ServerGenerator.Generate(new RPCServerConf());

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        RPCServer.Run();
    }
    [ClassCleanup]
    public static void ClassCleanup()
    {
        RPCServer.Terminate();
    }


    [TestMethod]
    public void RPCServer_Runnable()
    {
        Thread.Sleep(5000);
        Assert.IsTrue(RPCServer.IsRunning);
    }
}




