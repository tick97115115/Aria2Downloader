using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aria2Service.Services.ExecutableFileManagement;


public class ExecutableManagement : IExecutableManagement
{
    private Process _process;
    public Process Process => _process;
    public string Exe { get; }
    public string Args { get; }
    private PossibleStates _currentState;
    public PossibleStates CurrentState { get => _currentState; set => _currentState = value; }
    public ExecutableManagement(string exe, string args)
    {
        Exe = exe;
        Args = args;
        _currentState = new Terminated();
        _process = new Process();
        _process.Exited += OnExited;
        _process.StartInfo.FileName = Exe;
        _process.StartInfo.Arguments = Args;
        //_process.StartInfo.UseShellExecute = true;
        _process.EnableRaisingEvents = true;
        _process.StartInfo.CreateNoWindow = true;
        _process.StartInfo.RedirectStandardOutput = true;
        _process.StartInfo.RedirectStandardError = true;
    }

    public bool IsRunning => _currentState is Run;

    public bool IsTerminated => _currentState is Terminated;
    public void Run()
    {
        CurrentState.ToRun(this);
    }

    public void Terminate()
    {
        CurrentState.ToTerminate(this);
    }

    public void OnExited(object? sender, EventArgs e)
    {
        var process = (Process)sender;
        _currentState = new Terminated();
    }
}
