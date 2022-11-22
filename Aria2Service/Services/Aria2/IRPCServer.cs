using Aria2Service.Services.ExecutableFileManagement;

namespace Aria2Service.Services.Aria2;


public interface IRPCServer : IExecutableManagement
{
    public Task ShutdownAsync();
    public Task ForceShutdownAsync();
    public RPCServerConf ServerConf { get; }
}

public interface IRPCServerGenerator
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

        return new RPCServer(exePath, arg, serverConf);
    }
}

public interface IRPCServerConf
{
    public bool EnableRpc { get; set; }
    public int RpcListenPort { get; set; }
    //public bool RpcListenAll { get; set; }
    public string Dir { get; set; }
    public string DiskCache { get; set; }
    public string RpcSecret { get; set; }
    public bool RpcAllowOriginAll { get; set; }
}