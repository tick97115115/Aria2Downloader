using Aria2NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aria2Service.Services.Aria2;

public interface IAria2NetClientGenerator
{
    public Aria2NetClient Generate(RPCServerConf serverConf);
}
