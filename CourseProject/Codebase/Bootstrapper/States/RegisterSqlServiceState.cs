using CourseProject.Codebase.Context;

namespace CourseProject.Codebase.Bootstrapper.States
{
    public class RegisterSqlServiceState : BootstrapState
    {
        private ProjectDbContext _dbContext;
        
        public RegisterSqlServiceState(BootstrapPayload payload, ProjectDbContext dbContext) : base(payload)
        {
            _dbContext = dbContext;
        }
        
        public override void Enter(Action onComplete)
        {
            RegisterEntityFramework();
            
            onComplete?.Invoke();
        }

        private void RegisterEntityFramework()
        {
            _dbContext = new ProjectDbContext();
            _dbContext.OnInitializationComplete(OnEntityFrameworkStarted);
        }

        private void OnEntityFrameworkStarted()
        {
            _payload.StateDemander.DemandNextState();
        }

        public override void Exit(Action onComplete) => onComplete?.Invoke();
    }
}