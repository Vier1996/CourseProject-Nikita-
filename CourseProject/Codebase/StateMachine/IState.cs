namespace CourseProject.Codebase.StateMachine
{
    public interface IState // интерфейс описывающий состояние
    {
        public void Enter(System.Action onComplete = null); // интерфейсный метод входа в состояние
        public void Exit(System.Action onComplete); // интерфейсный метод выхода из состояния
    }
}