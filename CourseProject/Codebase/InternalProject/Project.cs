using CourseProject.Codebase.Bootstrapper;
using CourseProject.Codebase.Disposable;
using CourseProject.Codebase.Looper;
using CourseProject.Codebase.Menu;

namespace CourseProject.Codebase.InternalProject
{
    public class Project // основной класс проекта
    {
        private readonly BootstrapStateMachine _bootstrapStateMachine; // объявляем экземпляр бутстрапера
        private readonly Disposer _disposer; // объявляем экземпляр очистителя
        
        public Project() // конструктор класса
        {
            _bootstrapStateMachine = new BootstrapStateMachine(); // создаем экземпляр бутстрапера
            _disposer = new Disposer(); // создаем экземпляр очистителя

            _bootstrapStateMachine.StatesResolved += OnBootstrapResolved; // подписываемся на событие "все состояния готовы"
            _bootstrapStateMachine.Resolve(); // говорим бутстраперу решать вопросы
        }

        private void OnBootstrapResolved() // методы который выполняется после готовности всех состояний
        {
            _bootstrapStateMachine.StatesResolved -= OnBootstrapResolved; // отписываеимя от события "все состояния готовы"

            Start(); // вызываем метод старта проекта
        }

        private void Start() // метод старта проекта
        {
            while (ProjectLooper.Instance.ProjectLoopState == ProjectLoopState.WORKING) // выполняем while пока состояние зацикливателя "работаю"
            {
                CallMenu(); // вызываем метод показа меню

                string? inputData = Console.ReadLine(); // ожидаем ввод пользователя
                int actionIndex = Convert.ToInt32(inputData); // переводим ввод пользователя в число

                ApplyCallback(actionIndex); // вызываем метод выполнения действия "меню"
            }
            
            ExitProject(); // вызываем метод "выйти с проекта"
        }

        private void ExitProject() // метод выхода из проекта
        {
            _disposer.Dispose(); // вызываем у очистителя метод очистки
        }

        private void CallMenu() => MenuPresenter.Instance.ShowMenu(); // метод вызова меню
        
        private void ApplyCallback(int callbackIndex) => MenuPresenter.Instance.ApplyCallback(callbackIndex); // метод выполения действия "меню"
    }
}