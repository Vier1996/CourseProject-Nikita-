using CourseProject.Codebase.Bootstrapper.States;
using CourseProject.Codebase.Context;
using CourseProject.Codebase.MySql;
using CourseProject.Codebase.StateMachine;

namespace CourseProject.Codebase.Bootstrapper
{
    public class BootstrapStateMachine : IStateSwitcher, IDisposable // Класс который заводит и инициализирует сервисы
    {
        public event Action StatesResolved; // Ивент, который гласит о том, что все состояния готовы к работе
        
        private readonly List<BootstrapState> _bootstrapStates = new List<BootstrapState>(); // список состояний
        private BootstrapState _currentBootstrapState = null; // текущее состояние
        
        public BootstrapStateMachine() // конструктор
        {
            BootstrapPayload bootstrapPayload = PreparePayload(); // Подготавливает полезную нагрузку
            
            _bootstrapStates.Add(new RegisterProjectLooperState(bootstrapPayload)); // добовляет состояние регистрации ProjectLooper (зацикливателя)
            _bootstrapStates.Add(new RegisterSqlServiceState(bootstrapPayload)); // добовляет состояние регистрации SqlService (сервиса базы данных)
            _bootstrapStates.Add(new RegisterMenuServiceState(bootstrapPayload)); // добовляет состояние регистрации MenuService (сервиса меню)
        }

        public void Resolve() => SwitchState(_bootstrapStates.First()); // меняет состояние машины на первое из списка

        public void DemandNextState() // метод для запроса следующено состояния
        {
            int stateIndex = _bootstrapStates.IndexOf(_currentBootstrapState); // получение текущего индекса

            if (stateIndex < _bootstrapStates.Count - 1) // проверка на наличее доступных состояний
            {
                BootstrapState newState = _bootstrapStates.ElementAt(++stateIndex); // присваивание нового состояния
                SwitchState(newState); // смена состояния
                return; // обрыв выполнения метода
            }
            
            StatesResolved?.Invoke(); // вызов ивента о готовности всех сервисов
        }

        private void SwitchState(IState state, Action onComplete = null) // метод смены состояний
        {
            if (_currentBootstrapState == null) // проверка состояния на то, что оно существует
            {
                OnSuccessfulExit(); // вызов анонимного метода OnSuccessfulExit
                return; // обрыв выполнения метода
            }
            
            _currentBootstrapState.Exit(OnSuccessfulExit); // вызов у текущего состояния метод Exit и передача калбека OnSuccessfulExit

            void OnSuccessfulExit() // анонимный метод OnSuccessfulExit
            {
                _currentBootstrapState = (BootstrapState) state; // переприсваивание текущего состояния
                _currentBootstrapState.Enter(onComplete); // вход в текущее состояние
            }
        }

        private BootstrapPayload PreparePayload() => // подготовка полезной нагрузки
            new BootstrapPayload()
            {
                StateDemander = this // инициализирует StateDemander (запрашиватель состояний)
            };

        public void Dispose() // метод который вызывает очистку у состояний
        {
            for (int i = 0; i < _bootstrapStates.Count; i++) // проходим по состояниям бутстраппера
                _bootstrapStates[i].Dispose(); // очищаем состояние бутстраппера
        }
    }
}