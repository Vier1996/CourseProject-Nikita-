namespace CourseProject.Codebase.Looper
{
    public class ProjectLooper
    {
        public event Action<ProjectLoopState> ProjectLoopStateChanged;

        public static ProjectLooper Instance
        {
            get
            {
                if (_instance == null) 
                    _instance = new ProjectLooper();
                return _instance;
            }
        }

        public ProjectLoopState ProjectLoopState => _currentLoopState;

        private ProjectLoopState _currentLoopState;
        private static ProjectLooper _instance;

        public ProjectLooper() => _currentLoopState = ProjectLoopState.NONE;

        public void StartProjectLoop() => ChangeLooperState(ProjectLoopState.WORKING);

        public void CancelProjectLoop() => ChangeLooperState(ProjectLoopState.STOPED);

        private void ChangeLooperState(ProjectLoopState state)
        {
            _currentLoopState = state;
            ProjectLoopStateChanged?.Invoke(_currentLoopState);
        }
    }
}