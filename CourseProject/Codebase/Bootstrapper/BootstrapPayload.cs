using CourseProject.Codebase.StateMachine;

namespace CourseProject.Codebase.Bootstrapper
{
    public class BootstrapPayload
    {
        [NonSerialized] public IStateSwitcher StateDemander;
    }
}