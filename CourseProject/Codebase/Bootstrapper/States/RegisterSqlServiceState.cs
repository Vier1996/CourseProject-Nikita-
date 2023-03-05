using CourseProject.Codebase.MySQL;

namespace CourseProject.Codebase.Bootstrapper.States
{
    public class RegisterSqlServiceState : BootstrapState
    {
        public RegisterSqlServiceState(BootstrapPayload payload) : base(payload)
        {
        }
        
        public override void Enter(Action onComplete)
        {
            RegisterSql();
            onComplete?.Invoke();
        }

        private void RegisterSql()
        {
            if (MySqlService.Instance.IsReady)
            {
                OnSqlStarted();
                return;
            }

            MySqlService.Instance.ConnectionSuccessfullyEstablished += OnSqlStarted;
            MySqlService.Instance.ConnectToDataBase();
        }

        private void OnSqlStarted()
        {
            MySqlService.Instance.ConnectionSuccessfullyEstablished -= OnSqlStarted;
            _payload.StateDemander.DemandNextState();
        }

        public override void Exit(Action onComplete) => onComplete?.Invoke();
    }
}