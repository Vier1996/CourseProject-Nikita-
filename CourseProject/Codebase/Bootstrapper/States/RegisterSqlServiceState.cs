using CourseProject.Codebase.Context;
using CourseProject.Codebase.MySql;

namespace CourseProject.Codebase.Bootstrapper.States
{
    public class RegisterSqlServiceState : BootstrapState, IDisposable // состояние регистрации службы "SQL"
    {
        public RegisterSqlServiceState(BootstrapPayload payload) : base(payload) { } // коструктор класса
        
        public override void Enter(Action onComplete) // метод входа в состояние
        {
            RegisterEntityFramework(); // вызов метода регистрации Entity framework
            onComplete?.Invoke(); // вызов события "выполнено"
        }

        private void RegisterEntityFramework() // метод регистрации Entity framework
        {
            _payload.ProjectDbContext = new ProjectDbContext(); // создание экземпляра контекста базы данных
            _payload.MySqlAgent = new MySqlAgent(_payload.ProjectDbContext);  // создание экземпляра агента
            
            OnEntityFrameworkStarted(); // вызов метода который вызывается когда Entity Framework готов
        }

        private void OnEntityFrameworkStarted() // метод который вызывается когда Entity Framework готов
        {
            _payload.StateDemander.DemandNextState(); // запрос след состояния у бутстрапера
        }

        public override void Exit(Action onComplete) => onComplete?.Invoke(); // методы выхода из состояния

        public override void Dispose() => // метод очистки состояния
            _payload.ProjectDbContext.SaveChanges();
    }
}