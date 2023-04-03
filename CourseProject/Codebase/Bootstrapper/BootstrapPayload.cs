using CourseProject.Codebase.Context;
using CourseProject.Codebase.MySql;
using CourseProject.Codebase.StateMachine;

namespace CourseProject.Codebase.Bootstrapper
{
    public class BootstrapPayload // полезная нагрузка для состояний будстрапера
    {
        [NonSerialized] public IStateSwitcher StateDemander; // интерфейс переключателя состояний
        [NonSerialized] public ProjectDbContext ProjectDbContext; // экземпляр контекста базы данных
        [NonSerialized] public MySqlAgent MySqlAgent; // экземпляр агента
    }
}