using CourseProject.Codebase.Bootstrapper;
using CourseProject.Codebase.Constants.DataBaseModel;
using CourseProject.Codebase.Disposable;
using CourseProject.Codebase.Looper;
using CourseProject.Codebase.Menu;
using CourseProject.Codebase.MySQL;

namespace CourseProject.Codebase.InternalProject
{
    public class Project 
    {
        private readonly BootstrapStateMachine _bootstrapStateMachine;
        private readonly Disposer _disposer;

        private MySqlService _sqlService;
        
        public Project()
        {
            _bootstrapStateMachine = new BootstrapStateMachine();
            _disposer = new Disposer();
            _sqlService = MySqlService.Instance;

            _bootstrapStateMachine.StatesResolved += OnBootstrapResolved;
            _bootstrapStateMachine.Resolve();
        }

        private void OnBootstrapResolved()
        {
            _bootstrapStateMachine.StatesResolved -= OnBootstrapResolved;
            Start();
        }

        private void Start()
        {
            Console.WriteLine("All zaebis");
            string inputData;
            
            while (ProjectLooper.Instance.ProjectLoopState == ProjectLoopState.WORKING)
            {
                CallMenu();

                inputData = Console.ReadLine();
                int actionIndex = Convert.ToInt32(inputData);

                ApplyCallback(actionIndex);
            }
            
            ExitProject();
        }

        private void ExitProject()
        {
            _disposer.Dispose();
        }

        private void CallMenu() => MenuPresenter.Instance.ShowMenu();
        
        private void ApplyCallback(int callbackIndex) => MenuPresenter.Instance.ApplyCallback(callbackIndex);

        private void DebugSelect()
        {
            MySqlCustomCommand customCommand = new MySqlCustomCommand();
            customCommand.SelectOption(Group.MODEL_NAME);
            
            _sqlService.ExecuteCommand(customCommand);
        }
    }
}