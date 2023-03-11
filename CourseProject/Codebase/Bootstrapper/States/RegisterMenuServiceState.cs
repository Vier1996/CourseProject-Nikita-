using CourseProject.Codebase.Looper;
using CourseProject.Codebase.Menu;

namespace CourseProject.Codebase.Bootstrapper.States
{
    public class RegisterMenuServiceState : BootstrapState, IDisposable
    {
        public RegisterMenuServiceState(BootstrapPayload payload) : base(payload) { }
        
        public override void Enter(Action onComplete)
        {
            DeclareMenus();
            onComplete?.Invoke();
        }

        private void DeclareMenus()
        {
            MenuView parentMenu = new MenuView("Главное меню");
            MenuView testSubMenuG = new MenuView("Меню по группам");
            MenuView testSubMenuS = new MenuView("Меню по специальности");
            MenuView testSubMenuF = new MenuView("Меню по форме обучению");
            MenuView testSubMenuQ = new MenuView("Меню по направлению");

            // Главное меню
            parentMenu.AddMenuItem(new MenuItem(1, "Меню по группам", () => MenuPresenter.Instance.SetCurrentMenu(testSubMenuG)));
            parentMenu.AddMenuItem(new MenuItem(2, "Меню по специальности", () => MenuPresenter.Instance.SetCurrentMenu(testSubMenuS)));
            parentMenu.AddMenuItem(new MenuItem(3, "Меню по форме обучения", () => MenuPresenter.Instance.SetCurrentMenu(testSubMenuF)));
            parentMenu.AddMenuItem(new MenuItem(4, "Меню по направлению ", () => MenuPresenter.Instance.SetCurrentMenu(testSubMenuQ)));
            parentMenu.AddMenuItem(new MenuItem(0, "Завершить работу ", () => ProjectLooper.Instance.CancelProjectLoop()));
            // Конец главного меню

            // Меню по группам
            testSubMenuG.AddMenuItem(new MenuItem(1, "Вывести общий список групп", () => _payload.MySqlAgent.GroupWrappedModel.DisplayInto()));
            testSubMenuG.AddMenuItem(new MenuItem(2, "Добавить группу", () => _payload.MySqlAgent.GroupWrappedModel.AddRow()));
            testSubMenuG.AddMenuItem(new MenuItem(3, "Редактировать группу", () => _payload.MySqlAgent.GroupWrappedModel.UpdateRow()));
            testSubMenuG.AddMenuItem(new MenuItem(4, "Удалить группу", () => _payload.MySqlAgent.GroupWrappedModel.RemoveRow()));
            testSubMenuG.AddMenuItem(new MenuItem(0, "Вернуться", () => MenuPresenter.Instance.SetCurrentMenu(parentMenu)));
            // Конец меню по группам

            // Меню по специальности
            testSubMenuS.AddMenuItem(new MenuItem(1, "Вывести общий список по специальности", () => _payload.MySqlAgent.SpecialityWrappedModel.DisplayInto()));
            testSubMenuS.AddMenuItem(new MenuItem(2, "Добавить специальность", () => _payload.MySqlAgent.SpecialityWrappedModel.AddRow()));
            testSubMenuS.AddMenuItem(new MenuItem(3, "Редактировать", () => _payload.MySqlAgent.SpecialityWrappedModel.UpdateRow()));
            testSubMenuS.AddMenuItem(new MenuItem(4, "Удалить", () => _payload.MySqlAgent.SpecialityWrappedModel.RemoveRow()));
            testSubMenuS.AddMenuItem(new MenuItem(0, "Вернуться", () => MenuPresenter.Instance.SetCurrentMenu(parentMenu)));
            // Конец меню по специальности

            // Меню по форме обучения
            testSubMenuF.AddMenuItem(new MenuItem(1, "Вывести общий список по форме обучения", () => _payload.MySqlAgent.FormedEducationWrappedModel.DisplayInto()));
            testSubMenuF.AddMenuItem(new MenuItem(2, "Добавить формеу обучения", () => _payload.MySqlAgent.FormedEducationWrappedModel.AddRow()));
            testSubMenuF.AddMenuItem(new MenuItem(3, "Редактировать", () => _payload.MySqlAgent.FormedEducationWrappedModel.UpdateRow()));
            testSubMenuF.AddMenuItem(new MenuItem(4, "Удалить", () => _payload.MySqlAgent.FormedEducationWrappedModel.RemoveRow()));
            testSubMenuF.AddMenuItem(new MenuItem(0, "Вернуться", () => MenuPresenter.Instance.SetCurrentMenu(parentMenu)));
            // Конец меню по форме обучения

            // Меню по форме по направлению
            testSubMenuQ.AddMenuItem(new MenuItem(1, "Вывести общий список по форме обучения", () => _payload.MySqlAgent.QualificationWrappedModel.DisplayInto()));
            testSubMenuQ.AddMenuItem(new MenuItem(2, "Добавить формеу обучения", () => _payload.MySqlAgent.QualificationWrappedModel.AddRow()));
            testSubMenuQ.AddMenuItem(new MenuItem(3, "Редактировать", () => _payload.MySqlAgent.QualificationWrappedModel.UpdateRow()));
            testSubMenuQ.AddMenuItem(new MenuItem(4, "Удалить", () => _payload.MySqlAgent.QualificationWrappedModel.RemoveRow()));
            testSubMenuQ.AddMenuItem(new MenuItem(0, "Вернуться", () => MenuPresenter.Instance.SetCurrentMenu(parentMenu)));
            // Конец меню по направлению

            MenuPresenter.Instance.SetCurrentMenu(parentMenu);
            OnDeclared();
        }

        private void OnDeclared() => _payload.StateDemander.DemandNextState();

        public override void Exit(Action onComplete) => onComplete?.Invoke();

        public override void Dispose() { }
    }
}