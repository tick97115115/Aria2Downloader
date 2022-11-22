using Aria2Service.Services.ExecutableFileManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aria2NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aria2Service.Services.Aria2;

namespace Aria2ServiceTest.Services.Aria2;

[TestClass]
public class Aria2ClientTest
{
    public static RPCServerConf configuration = new RPCServerConf()
    {
        RpcListenPort = 6800
    };
    public static Aria2NetClient Aria2Client { get; set; } = Aria2NetClientGenerator.Generate(configuration);
    public static RPCServer Server { get; set; } = RPCServerGenerator.Generate(configuration);
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        Server.Run();
    }
    [ClassCleanup]
    public static void ClassCleanup()
    {
        // Clean test function
        var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        var uri = new Uri("http://seopic.699pic.com/photo/50048/7605.jpg_wh1200.jpg");
        var path = Path.Combine(desktop, uri.AbsolutePath.Split("/").Last());
        File.Delete(path);

        // Close server process
        Server?.Terminate();
    }

    [TestMethod]
    public async Task DownloadTest()
    {
        var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        var uri = new Uri("http://seopic.699pic.com/photo/50048/7605.jpg_wh1200.jpg");
        var path = Path.Combine(desktop, uri.AbsolutePath.Split("/").Last());
        Console.WriteLine(path);
        var result = await Aria2Client.AddUriAsync
            (new List<string>
            {
                uri.ToString(),
            },
            new Dictionary<string, object>
            {
                {"dir", desktop }
            },
            0
        );
        Thread.Sleep(5000);
        Assert.IsTrue(File.Exists(path));
    }
}
