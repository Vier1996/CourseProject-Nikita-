using CourseProject.Codebase.Context;
using CourseProject.Codebase.MySql;
using CourseProject.Codebase.StateMachine;

namespace CourseProject.Codebase.Bootstrapper
{
    public class BootstrapPayload
    {
        [NonSerialized] public IStateSwitcher StateDemander;
        [NonSerialized] public ProjectDbContext ProjectDbContext;
        [NonSerialized] public MySqlAgent MySqlAgent;
    }
}