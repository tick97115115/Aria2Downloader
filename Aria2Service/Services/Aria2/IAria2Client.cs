using Aria2NET;
using WatsonWebsocket;

namespace Aria2Service.Services.Aria2;

public interface IAria2NetClientGenerator
{
    public static Aria2NetClient Generate(RPCServerConf serverConf)
    {
        string proto = "http";
        string address = "127.0.0.1";
        int port = serverConf.RpcListenPort;
        string uri = $"{proto}://{address}:{port}/jsonrpc";

        var Client = new Aria2NetClient(uri, serverConf.RpcSecret);
        return Client;
    }
}

public interface IAria2Clinet
{
    Aria2NetClient Aria2NetClient { get; init; }
    HashSet<string> TaskList { get; }
    WatsonWsClient WsClient { get; init; }
}