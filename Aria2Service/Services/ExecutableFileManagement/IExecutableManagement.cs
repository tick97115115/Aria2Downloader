using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aria2Service.Services.ExecutableFileManagement;

public interface IExecutableManagement
{
    public Process Process { get; }
    public PossibleStates CurrentState { get; set; }

    public void Run();
    public bool IsRunning { get; }
    public bool IsTerminated { get; }
    public void Terminate();
    public void OnExited(object sender, EventArgs e);
}
