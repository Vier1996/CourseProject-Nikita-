using CourseProject.Codebase.Bootstrapper.States;
using CourseProject.Codebase.Context;
using CourseProject.Codebase.MySql;
using CourseProject.Codebase.StateMachine;

namespace CourseProject.Codebase.Bootstrapper
{
    public class BootstrapStateMachine : IStateSwitcher
    {
        public event Action StatesResolved;
        
        private readonly List<BootstrapState> _bootstrapStates = new List<BootstrapState>();
        private BootstrapState _currentBootstrapState = null;

        public BootstrapStateMachine(ProjectDbContext dbContext, MySqlAgent agent)
        {
            BootstrapPayload bootstrapPayload = PreparePayload();
            
            _bootstrapStates.Add(new RegisterProjectLooperState(bootstrapPayload));
            _bootstrapStates.Add(new RegisterSqlServiceState(bootstrapPayload, dbContext, agent));
            _bootstrapStates.Add(new RegisterMenuServiceState(bootstrapPayload, agent));
        }

        public void Resolve() => SwitchState(_bootstrapStates.First());

        public void DemandNextState()
        {
            int stateIndex = _bootstrapStates.IndexOf(_currentBootstrapState);

            if (stateIndex < _bootstrapStates.Count - 1)
            {
                BootstrapState newState = _bootstrapStates.ElementAt(++stateIndex);
                SwitchState(newState);
                return;
            }
            
            StatesResolved?.Invoke();
        }

        private void SwitchState(IState state, Action onComplete = null)
        {
            if (_currentBootstrapState == null)
            {
                OnSuccessfulExit();
                return;
            }
            
            _currentBootstrapState.Exit(OnSuccessfulExit);

            void OnSuccessfulExit()
            {
                _currentBootstrapState = (BootstrapState) state;
                _currentBootstrapState.Enter(onComplete);
            }
        }

        private BootstrapPayload PreparePayload() => 
            new BootstrapPayload()
            {
                StateDemander = this
            };
    }
}