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
            MenuView testSubMenuG= new MenuView("Меню по группам");
            MenuView testSubMenuS= new MenuView("Меню по специальности");
            MenuView testSubMenuF= new MenuView("Меню по форме обучению");
            MenuView testSubMenuQ= new MenuView("Меню по направлению");
            
            // Главное меню
            parentMenu.AddMenuItem(new MenuItem(1, "Меню по группам", ()=>MenuPresenter.Instance.SetCurrentMenu(testSubMenuG))); 
            parentMenu.AddMenuItem(new MenuItem(2, "Меню по специальности", ()=>MenuPresenter.Instance.SetCurrentMenu(testSubMenuS)));
            parentMenu.AddMenuItem(new MenuItem(3, "Меню по форме обучения", ()=>MenuPresenter.Instance.SetCurrentMenu(testSubMenuF)));
            parentMenu.AddMenuItem(new MenuItem(4, "Меню по направлению ", ()=>MenuPresenter.Instance.SetCurrentMenu(testSubMenuQ)));
            parentMenu.AddMenuItem(new MenuItem(0, "Завершить работу ", ()=> ProjectLooper.Instance.CancelProjectLoop()));
            // Конец главного меню
            
            // Меню по группам
            testSubMenuG.AddMenuItem(new MenuItem(1, "Общий список групп", ()=>{})); 
            testSubMenuG.AddMenuItem(new MenuItem(2, "Вернуться", ()=>MenuPresenter.Instance.SetCurrentMenu(parentMenu)));
            testSubMenuG.AddMenuItem(new MenuItem(3, "Добавить группу", ()=>{}));
            testSubMenuG.AddMenuItem(new MenuItem(4, "Редактировать", ()=>{}));
            testSubMenuG.AddMenuItem(new MenuItem(5, "Удалить", ()=>{}));
            // Конец меню по группам
            
            // Меню по специальности
            testSubMenuS.AddMenuItem(new MenuItem(1, "Общий список по специальности", ()=>{})); 
            testSubMenuS.AddMenuItem(new MenuItem(2, "Вернуться", ()=>MenuPresenter.Instance.SetCurrentMenu(parentMenu)));
            testSubMenuS.AddMenuItem(new MenuItem(3, "Добавить специальность", ()=>{}));
            testSubMenuS.AddMenuItem(new MenuItem(4, "Редактировать", ()=>{}));
            testSubMenuS.AddMenuItem(new MenuItem(5, "Удалить", ()=>{}));
            // Конец меню по специальности
            
            // Меню по форме обучения
            testSubMenuF.AddMenuItem(new MenuItem(1, "Общий список по форме обучения", ()=>{})); 
            testSubMenuF.AddMenuItem(new MenuItem(2, "Вернуться", ()=>MenuPresenter.Instance.SetCurrentMenu(parentMenu)));
            testSubMenuF.AddMenuItem(new MenuItem(3, "Добавить формеу обучения", ()=>{}));
            testSubMenuF.AddMenuItem(new MenuItem(4, "Редактировать", ()=>{}));
            testSubMenuF.AddMenuItem(new MenuItem(5, "Удалить", ()=>{}));
            // Конец меню по форме обучения
            
            // Меню по форме по направлению
            testSubMenuQ.AddMenuItem(new MenuItem(1, "Общий список по форме обучения", ()=>{})); 
            testSubMenuQ.AddMenuItem(new MenuItem(2, "Вернуться", ()=>MenuPresenter.Instance.SetCurrentMenu(parentMenu)));
            testSubMenuQ.AddMenuItem(new MenuItem(3, "Добавить формеу обучения", ()=>{}));
            testSubMenuQ.AddMenuItem(new MenuItem(4, "Редактировать", ()=>{}));
            testSubMenuQ.AddMenuItem(new MenuItem(5, "Удалить", ()=>{}));
            // Конец меню по направлению
            
            MenuPresenter.Instance.SetCurrentMenu(parentMenu);
            OnDeclared();
        }

        private void OnDeclared() => _payload.StateDemander.DemandNextState();

        public override void Exit(Action onComplete) => onComplete?.Invoke();
    }
}