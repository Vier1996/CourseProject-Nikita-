using CourseProject.Codebase.Looper;

namespace CourseProject.Codebase.Bootstrapper.States
{
    public class RegisterProjectLooperState : BootstrapState // состояние регистрации службы "зацикливателя"
    {
        public RegisterProjectLooperState(BootstrapPayload payload) : base(payload) // конструктор класса
        {
        }
        
        public override void Enter(Action onComplete) // метод входа в состояние
        {
            RegisterLooper(); // вызов метода регистрации зацикливателя
            onComplete?.Invoke(); // вызов события "выполнено"
        }

        private void RegisterLooper() // метод регистрации зацикливателя
        {
            ProjectLooper.Instance.ProjectLoopStateChanged += OnProjectLoopStateChanged; // подписка на события смены состояния зацикливателя
            ProjectLooper.Instance.StartProjectLoop(); // запуск зацикливателя
        }

        private void OnProjectLoopStateChanged(ProjectLoopState state) // метод подписки на событие
        {
            ProjectLooper.Instance.ProjectLoopStateChanged -= OnProjectLoopStateChanged; // отписка от события смены состояния зацикливателя
            _payload.StateDemander.DemandNextState(); // запрос след состояния у бутстрапера
        }
        
        public override void Exit(Action onComplete) => onComplete?.Invoke(); // метод выхода из состояния

        public override void Dispose() { } // метод очистки состояния
    }
}