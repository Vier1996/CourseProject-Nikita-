using CourseProject.Codebase.Looper;
using CourseProject.Codebase.Menu;

namespace CourseProject.Codebase.Bootstrapper.States
{
    public class RegisterMenuServiceState : BootstrapState
    {
        public RegisterMenuServiceState(BootstrapPayload payload) : base(payload)
        {
        }
        
        public override void Enter(Action onComplete)
        {
            DeclareMenus();
            onComplete?.Invoke();
        }

        private void DeclareMenus()
        {
            MenuView parentMenu = new MenuView("Главное меню");
            MenuView testSubMenu = new MenuView("Вспомогательное меню");
            
            parentMenu.AddMenuItem(new MenuItem(1, "Полный отчет", () =>
            {
                Console.WriteLine("Перешли в меню поглубже...");
                MenuPresenter.Instance.SetCurrentMenu(testSubMenu);

            }));
            
            parentMenu.AddMenuItem(new MenuItem(2, "Вывести по направлению", () =>
            {
                Console.WriteLine("Перешли в меню поглубже...");
                MenuPresenter.Instance.SetCurrentMenu(testSubMenu);

            }));
            
            parentMenu.AddMenuItem(new MenuItem(3, "Вывести по колличеству студентов", () =>
            {
                Console.WriteLine("Перешли в меню поглубже...");
                MenuPresenter.Instance.SetCurrentMenu(testSubMenu);

            }));
            
            parentMenu.AddMenuItem(new MenuItem(4, "Вывеси по названию группы", () =>
            {
                Console.WriteLine("Перешли в меню поглубже...");
                MenuPresenter.Instance.SetCurrentMenu(testSubMenu);
            }));

            parentMenu.AddMenuItem(new MenuItem(5, "Редактирование", () =>
            {
                Console.WriteLine("Перешли в меню поглубже...");
                MenuPresenter.Instance.SetCurrentMenu(testSubMenu);
            }));
            
            parentMenu.AddMenuItem(new MenuItem(6, "Добавление", () =>
            {
                Console.WriteLine("Перешли в меню поглубже...");
                MenuPresenter.Instance.SetCurrentMenu(testSubMenu);
            }));
            
            parentMenu.AddMenuItem(new MenuItem(7, "Удаление", () =>
            {
                Console.WriteLine("Перешли в меню поглубже...");
                MenuPresenter.Instance.SetCurrentMenu(testSubMenu);
            }));
            
            parentMenu.AddMenuItem(new MenuItem(0, "Завершить работу.", () => ProjectLooper.Instance.CancelProjectLoop()));
            
            testSubMenu.AddMenuItem(new MenuItem(1, "Вывести в лог {ЖОПА}", () => Console.WriteLine("ЖОПА")));
            testSubMenu.AddMenuItem(new MenuItem(2, "Вернуться", () => MenuPresenter.Instance.SetCurrentMenu(parentMenu)));
            testSubMenu.AddMenuItem(new MenuItem(0, "Завершить работу.", () => ProjectLooper.Instance.CancelProjectLoop()));
            
            MenuPresenter.Instance.SetCurrentMenu(parentMenu);
            OnDeclared();
        }

        private void OnDeclared() => _payload.StateDemander.DemandNextState();

        public override void Exit(Action onComplete) => onComplete?.Invoke();
    }
}