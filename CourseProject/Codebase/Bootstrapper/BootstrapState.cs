using CourseProject.Codebase.StateMachine;

namespace CourseProject.Codebase.Bootstrapper
{
    public class BootstrapState : IState
    {
        protected BootstrapPayload _payload;
        
        protected BootstrapState(BootstrapPayload payload)
        {
            _payload = payload;
        }

        public virtual void Enter(Action onComplete) => onComplete?.Invoke();

        public virtual void Exit(Action onComplete) => onComplete.Invoke();
    }
}