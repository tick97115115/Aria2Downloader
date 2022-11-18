using Aria2Service.Services.ExecutableFileManagement;

namespace Aria2Service.Services.Aria2;


public interface IRPCServerGenerator
{
    public ExecutableManagement Generate(RPCServerConf serverConf);
}

public interface IRPCServerConf
{
    public bool EnableRpc { get; set; }
    public int RpcListenPort { get; set; }
    public bool RpcListenAll { get; set; }
    public string Dir { get; set; }
    public string DiskCache { get; set; }
    public string RpcSecret { get; set; }
    public bool RpcAllowOriginAll { get; set; }
}