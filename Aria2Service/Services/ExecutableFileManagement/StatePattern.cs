using System.Runtime.InteropServices;

namespace Aria2Service.Services.ExecutableFileManagement;

public interface PossibleStates
{
    void ToRun(IExecutableManagement context);
    void ToTerminate(IExecutableManagement context);
}

public enum States
{
    Run,
    Stop
}

public class Run : PossibleStates
{
    public void ToRun(IExecutableManagement context)
    {
        return;
    }

    public void ToTerminate(IExecutableManagement context)
    {
        context.Process.Kill();
        context.Process.WaitForExit(); // When you kill process you must invoke function WaitForExit.Because close process needs some time, WaitForExit is used to synchronize the actual process closing behavior with your program.
    }
}

public class Terminated : PossibleStates
{

    public void ToRun(IExecutableManagement context)
    {
        context.Process.Start();
        context.CurrentState = new Run();  //State transform must be done in the last step.
    }

    public void ToTerminate(IExecutableManagement context)
    {
        return;
    }
}
