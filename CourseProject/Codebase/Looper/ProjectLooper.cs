namespace CourseProject.Codebase.Looper
{
    public class ProjectLooper // синглтон класс "зацикливатель"
    {
        public event Action<ProjectLoopState> ProjectLoopStateChanged; // событие "состояние изменилось"

        public static ProjectLooper Instance // экземпляр на зацикливатель
        {
            get
            {
                if (_instance == null) // проверка экземпляра на существование
                    _instance = new ProjectLooper(); // созданеи экземпляра зацикливателя
                return _instance; // возвращение экземпляра
            }
        }

        public ProjectLoopState ProjectLoopState => _currentLoopState; // сво-во "состояние зацикливателя"

        private ProjectLoopState _currentLoopState; // приватное сво-во состояния зацикливателя
        private static ProjectLooper _instance;

        public ProjectLooper() => _currentLoopState = ProjectLoopState.NONE; // конструктор зацикливателя

        public void StartProjectLoop() => ChangeLooperState(ProjectLoopState.WORKING); // метод запуска зацикливателя

        public void CancelProjectLoop() => ChangeLooperState(ProjectLoopState.STOPED); // метод остановки зацикливателя

        private void ChangeLooperState(ProjectLoopState state) // метод смены состояния зацикливателя
        {
            _currentLoopState = state; // присваивание нового состояния
            ProjectLoopStateChanged?.Invoke(_currentLoopState); // вызов события "состояние изменилось"
        }
    }
}