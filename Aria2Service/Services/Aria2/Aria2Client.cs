using Aria2NET;
using WatsonWebsocket;

namespace Aria2Service.Services.Aria2;


public class Aria2NetClientGenerator: IAria2NetClientGenerator
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

public class Aria2Client : IAria2Clinet
{
    public Aria2NetClient Aria2NetClient { get; init; }
    public RPCServerConf ServerConf { get; init; }
    public HashSet<string> TaskList { get; } = new HashSet<string>();
    public WatsonWsClient WsClient { get; init; }

    public Aria2Client(RPCServerConf rpcServerConf)
    {
        ServerConf = rpcServerConf;
        Aria2NetClient = Aria2NetClientGenerator.Generate(ServerConf);
        string address = "127.0.0.1";
        WsClient = new WatsonWsClient($"{address}", ServerConf.RpcListenPort, false);
        WsClient.ServerConnected += ServerConnected;
        WsClient.ServerDisconnected += ServerDisconnected;
        WsClient.MessageReceived += MessageReceived;
    }
    private static void MessageReceived(object? sender, MessageReceivedEventArgs args)
    {
        //
    }
    private static void ServerConnected(object? sender, EventArgs args)
    {
        //
    }
    private static void ServerDisconnected(object? sender, EventArgs args)
    {
        //
    }
}