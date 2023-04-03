namespace CourseProject.Codebase.StateMachine
{
    public interface IStateSwitcher // интерфейс описывающий переключателя состояния
    {
        public void DemandNextState(); // интерфейсный методы "запрос следующего состояния"
    }
}