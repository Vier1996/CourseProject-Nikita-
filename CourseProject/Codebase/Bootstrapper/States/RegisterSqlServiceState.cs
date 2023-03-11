using CourseProject.Codebase.Context;
using CourseProject.Codebase.MySql;

namespace CourseProject.Codebase.Bootstrapper.States
{
    public class RegisterSqlServiceState : BootstrapState, IDisposable
    {
        public RegisterSqlServiceState(BootstrapPayload payload) : base(payload) { }
        
        public override void Enter(Action onComplete)
        {
            RegisterEntityFramework();
            
            onComplete?.Invoke();
        }

        private void RegisterEntityFramework()
        {
            _payload.ProjectDbContext = new ProjectDbContext();
            _payload.MySqlAgent = new MySqlAgent(_payload.ProjectDbContext);
            
            OnEntityFrameworkStarted();
        }

        private void OnEntityFrameworkStarted()
        {
            _payload.StateDemander.DemandNextState();
        }

        public override void Exit(Action onComplete) => onComplete?.Invoke();

        public override void Dispose() => 
            _payload.ProjectDbContext.SaveChanges();
    }
}