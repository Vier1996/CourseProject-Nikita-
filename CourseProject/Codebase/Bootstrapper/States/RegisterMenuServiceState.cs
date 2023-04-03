using CourseProject.Codebase.Looper;
using CourseProject.Codebase.Menu;

namespace CourseProject.Codebase.Bootstrapper.States
{
    public class RegisterMenuServiceState : BootstrapState, IDisposable // состояние регистрации службы "меню"
    {
        public RegisterMenuServiceState(BootstrapPayload payload) : base(payload) { } // конструктор класса
        
        public override void Enter(Action onComplete) // переопределенный метод входа в состояние
        {
            DeclareMenus(); // вызов декларации меню
            onComplete?.Invoke(); // вызов события "выполнено"
        }

        private void DeclareMenus() // метод декларации меню
        {
            MenuView parentMenu = new MenuView("Главное меню"); // экземпляр главного меню MenuView
            MenuView testSubMenuG = new MenuView("Меню по группам"); // экземпляр меню "группы" MenuView
            MenuView testSubMenuS = new MenuView("Меню по специальности"); // экземпляр меню "специальность" MenuView
            MenuView testSubMenuF = new MenuView("Меню по форме обучению"); // экземпляр меню "форма обучения" MenuView
            MenuView testSubMenuQ = new MenuView("Меню по направлению"); // экземпляр меню "направление" MenuView

            // Главное меню
            parentMenu.AddMenuItem(new MenuItem(1, "Меню по группам", () => MenuPresenter.Instance.SetCurrentMenu(testSubMenuG))); // меню айтем
            parentMenu.AddMenuItem(new MenuItem(2, "Меню по специальности", () => MenuPresenter.Instance.SetCurrentMenu(testSubMenuS))); // меню айтем
            parentMenu.AddMenuItem(new MenuItem(3, "Меню по форме обучения", () => MenuPresenter.Instance.SetCurrentMenu(testSubMenuF))); // меню айтем
            parentMenu.AddMenuItem(new MenuItem(4, "Меню по направлению ", () => MenuPresenter.Instance.SetCurrentMenu(testSubMenuQ))); // меню айтем
            parentMenu.AddMenuItem(new MenuItem(0, "Завершить работу ", () => ProjectLooper.Instance.CancelProjectLoop())); // меню айтем
            // Конец главного меню

            // Меню по группам
            testSubMenuG.AddMenuItem(new MenuItem(1, "Вывести общий список групп", () => _payload.MySqlAgent.GroupWrappedModel.DisplayInto())); // меню айтем
            testSubMenuG.AddMenuItem(new MenuItem(2, "Добавить группу", () => _payload.MySqlAgent.GroupWrappedModel.AddRow())); // меню айтем
            testSubMenuG.AddMenuItem(new MenuItem(3, "Редактировать группу", () => _payload.MySqlAgent.GroupWrappedModel.UpdateRow())); // меню айтем
            testSubMenuG.AddMenuItem(new MenuItem(4, "Удалить группу", () => _payload.MySqlAgent.GroupWrappedModel.RemoveRow())); // меню айтем
            testSubMenuG.AddMenuItem(new MenuItem(0, "Вернуться", () => MenuPresenter.Instance.SetCurrentMenu(parentMenu))); // меню айтем
            // Конец меню по группам

            // Меню по специальности
            testSubMenuS.AddMenuItem(new MenuItem(1, "Вывести общий список по специальности", () => _payload.MySqlAgent.SpecialityWrappedModel.DisplayInto())); // меню айтем
            testSubMenuS.AddMenuItem(new MenuItem(2, "Добавить специальность", () => _payload.MySqlAgent.SpecialityWrappedModel.AddRow())); // меню айтем
            testSubMenuS.AddMenuItem(new MenuItem(3, "Редактировать", () => _payload.MySqlAgent.SpecialityWrappedModel.UpdateRow())); // меню айтем
            testSubMenuS.AddMenuItem(new MenuItem(4, "Удалить", () => _payload.MySqlAgent.SpecialityWrappedModel.RemoveRow())); // меню айтем
            testSubMenuS.AddMenuItem(new MenuItem(0, "Вернуться", () => MenuPresenter.Instance.SetCurrentMenu(parentMenu))); // меню айтем
            // Конец меню по специальности

            // Меню по форме обучения
            testSubMenuF.AddMenuItem(new MenuItem(1, "Вывести общий список по форме обучения", () => _payload.MySqlAgent.FormedEducationWrappedModel.DisplayInto())); // меню айтем
            testSubMenuF.AddMenuItem(new MenuItem(2, "Добавить формеу обучения", () => _payload.MySqlAgent.FormedEducationWrappedModel.AddRow())); // меню айтем
            testSubMenuF.AddMenuItem(new MenuItem(3, "Редактировать", () => _payload.MySqlAgent.FormedEducationWrappedModel.UpdateRow())); // меню айтем
            testSubMenuF.AddMenuItem(new MenuItem(4, "Удалить", () => _payload.MySqlAgent.FormedEducationWrappedModel.RemoveRow())); // меню айтем
            testSubMenuF.AddMenuItem(new MenuItem(0, "Вернуться", () => MenuPresenter.Instance.SetCurrentMenu(parentMenu))); // меню айтем
            // Конец меню по форме обучения

            // Меню по форме по направлению
            testSubMenuQ.AddMenuItem(new MenuItem(1, "Вывести общий список по форме обучения", () => _payload.MySqlAgent.QualificationWrappedModel.DisplayInto())); // меню айтем
            testSubMenuQ.AddMenuItem(new MenuItem(2, "Добавить формеу обучения", () => _payload.MySqlAgent.QualificationWrappedModel.AddRow())); // меню айтем
            testSubMenuQ.AddMenuItem(new MenuItem(3, "Редактировать", () => _payload.MySqlAgent.QualificationWrappedModel.UpdateRow())); // меню айтем
            testSubMenuQ.AddMenuItem(new MenuItem(4, "Удалить", () => _payload.MySqlAgent.QualificationWrappedModel.RemoveRow())); // меню айтем
            testSubMenuQ.AddMenuItem(new MenuItem(0, "Вернуться", () => MenuPresenter.Instance.SetCurrentMenu(parentMenu))); // меню айтем
            // Конец меню по направлению

            MenuPresenter.Instance.SetCurrentMenu(parentMenu);
            OnDeclared();
        }

        private void OnDeclared() => _payload.StateDemander.DemandNextState();

        public override void Exit(Action onComplete) => onComplete?.Invoke();

        public override void Dispose() { }
    }
}