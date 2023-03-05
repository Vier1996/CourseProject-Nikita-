namespace CourseProject.Codebase.StateMachine
{
    public interface IState
    {
        public void Enter(System.Action onComplete = null);
        public void Exit(System.Action onComplete);
    }
}