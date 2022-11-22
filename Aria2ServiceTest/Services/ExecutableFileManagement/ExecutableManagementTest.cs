namespace TestProject.Services.ExecutableFileManagement;

using Aria2Service.Services.ExecutableFileManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

[TestClass]
public class ExecutableManagementTest
{
    public static ExecutableManagement Aria2cServer { get; set; } = new ExecutableManagement(@"Resources\aria2-1.36.0-win-64bit-build1\aria2c.exe", $"--enable-rpc --rpc-listen-port={6801}");
    [TestInitialize]
    public void TestInitialize()
    {
        Aria2cServer.Run();
    }

    [TestCleanup]
     public void TestCleanup()
    {
        if (!Aria2cServer.Process.HasExited)
        {
            Aria2cServer.Process.Kill();
            Aria2cServer.Process.WaitForExit();
            Aria2cServer.Process.Close();
        }
    }
    [TestMethod]
    public void ExecutableManagement_Runable()
    {
        Thread.Sleep( 5000 );
        Assert.IsTrue(!Aria2cServer.Process.HasExited);
    }

    [TestMethod]
    public void ExecutableManagement_StateIsRunning()
    {
        Assert.IsTrue(Aria2cServer.IsRunning);
    }

    [TestMethod]
    public void ExecutableManagement_StateIsTerminated()
    {
        Aria2cServer.Terminate();
        Console.WriteLine(Aria2cServer.CurrentState);
        Assert.IsTrue(Aria2cServer.IsTerminated);
    }
}
