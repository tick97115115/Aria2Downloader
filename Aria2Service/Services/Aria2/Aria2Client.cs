using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aria2NET;
namespace Aria2Service.Services.Aria2;


public class Aria2NetClientGenerator: IAria2NetClientGenerator
{
    public Aria2NetClient Generate(RPCServerConf serverConf)
    {
        var proto = "http";
        string address = "127.0.0.1";
        if (serverConf.RpcListenAll is true)
        {
            address = "0.0.0.0";
        }
        int port = serverConf.RpcListenPort;
        var uri = $"{proto}://{address}:{port}/jsonrpc";
        
        var Client = new Aria2NetClient(uri, serverConf.RpcSecret);
        return Client;
    }
}
