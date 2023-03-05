using CourseProject.Codebase.Context;
using CourseProject.Codebase.MySql;

namespace CourseProject.Codebase.Bootstrapper.States
{
    public class RegisterSqlServiceState : BootstrapState
    {
        private ProjectDbContext _dbContext;
        private MySqlAgent _agent;
        
        public RegisterSqlServiceState(BootstrapPayload payload, ProjectDbContext dbContext, MySqlAgent agent) : base(payload)
        {
            _dbContext = dbContext;
            _agent = agent;
        }
        
        public override void Enter(Action onComplete)
        {
            RegisterEntityFramework();
            
            onComplete?.Invoke();
        }

        private void RegisterEntityFramework()
        {
            _dbContext = new ProjectDbContext();
            _agent = new MySqlAgent(_dbContext);
            
            OnEntityFrameworkStarted();
        }

        private void OnEntityFrameworkStarted()
        {
            _payload.StateDemander.DemandNextState();
        }

        public override void Exit(Action onComplete) => onComplete?.Invoke();
    }
}