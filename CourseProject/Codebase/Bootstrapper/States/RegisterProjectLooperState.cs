using CourseProject.Codebase.Looper;

namespace CourseProject.Codebase.Bootstrapper.States
{
    public class RegisterProjectLooperState : BootstrapState
    {
        public RegisterProjectLooperState(BootstrapPayload payload) : base(payload)
        {
        }
        
        public override void Enter(Action onComplete)
        {
            RegisterLooper();
            onComplete?.Invoke();
        }

        private void RegisterLooper()
        {
            ProjectLooper.Instance.ProjectLoopStateChanged += OnProjectLoopStateChanged;
            ProjectLooper.Instance.StartProjectLoop();
        }

        private void OnProjectLoopStateChanged(ProjectLoopState state)
        {
            ProjectLooper.Instance.ProjectLoopStateChanged -= OnProjectLoopStateChanged;
            _payload.StateDemander.DemandNextState();
        }
        
        public override void Exit(Action onComplete) => onComplete?.Invoke();
    }
}