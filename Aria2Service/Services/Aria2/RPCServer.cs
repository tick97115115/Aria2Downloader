using Aria2Service.Services.ExecutableFileManagement;
using Aria2NET;
namespace Aria2Service.Services.Aria2;



public class RPCServer: ExecutableManagement, IRPCServer
{
    public RPCServer(string exe, string args, RPCServerConf serverConf): base(exe, args)
    {
        ServerConf = serverConf;
    }


    public RPCServerConf ServerConf { get; }

    public async Task ForceShutdownAsync()
    {
        var aria2Net = Aria2NetClientGenerator.Generate(ServerConf);
        await aria2Net.ForceShutdownAsync();
    }

    public async Task ShutdownAsync()
    {
        var aria2Net = Aria2NetClientGenerator.Generate(ServerConf);
        await aria2Net.ShutdownAsync();
    }
}

public class RPCServerGenerator : IRPCServerGenerator
{
    public static RPCServer Generate(RPCServerConf serverConf)
    {
        string exePath;
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            exePath = @"Resources\aria2-1.36.0-win-64bit-build1\aria2c.exe";
        }
        else
        {
            exePath = @"aria2"; //Executable name in linux and MacOS, the user under linux or Mac OS should make sure they have aria2 Executable add to env path.
        }

        var argList = new string[] {
                $"--enable-rpc={serverConf.EnableRpc.ToString().ToLower()}",
                $"--rpc-listen-port={serverConf.RpcListenPort}",
                //$"--rpc-listen-all={serverConf.RpcListenAll.ToString().ToLower()}",
                $"--dir={serverConf.Dir}",
                $"--disk-cache={serverConf.DiskCache}",
                $"--rpc-secret={serverConf.RpcSecret}",
                $"--rpc-allow-origin-all={serverConf.RpcAllowOriginAll.ToString().ToLower()}"
            }; //running will. paramters no problem.
        var arg = string.Join(" ", argList);
        Console.WriteLine(arg);
        
        return new RPCServer(exePath, arg, serverConf);
    }
}

public class RPCServerConf : IRPCServerConf
{
    public bool EnableRpc { get; set; } = true;
    public int RpcListenPort { get; set; } = 6800;
    //public bool RpcListenAll{ get; set; } = false;
    public string Dir{ get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public string DiskCache { get; set; } = "0";
    public string RpcSecret { get; set; } = "MySecret123";
    public bool RpcAllowOriginAll { get; set; } = true;
}
