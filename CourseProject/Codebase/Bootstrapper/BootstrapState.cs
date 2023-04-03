using CourseProject.Codebase.StateMachine;

namespace CourseProject.Codebase.Bootstrapper
{
    public class BootstrapState : IState, IDisposable // базовый класс, который описывает состояние бутстраппера
    {
        protected BootstrapPayload _payload; // экземпляр полезной нагрузки для состояния
        
        protected BootstrapState(BootstrapPayload payload) // конструктор базового класса состояний
        {
            _payload = payload; // присвоение полезной нагрузки
        }

        public virtual void Enter(Action onComplete) => onComplete?.Invoke(); // метод входа в состояние

        public virtual void Exit(Action onComplete) => onComplete.Invoke(); // методы выхода из состояния

        public virtual void Dispose() // метод очистки состояния
        {
            
        }
    }
}