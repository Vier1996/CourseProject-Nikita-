namespace CourseProject.Codebase.Menu
{
    public class MenuPresenter
    {
        public static MenuPresenter Instance
        {
            get
            {
                if (_instance == null) 
                    _instance = new MenuPresenter();
                return _instance;
            }
        }
     
        private static MenuPresenter _instance;
        
        private MenuView _currentMenu = null;

        public void SetCurrentMenu(MenuView menuView) => _currentMenu = menuView;

        public void ShowMenu()
        {
            MenuView currentView = _currentMenu;
            List<MenuItem> items = currentView.GetItems();

            string consoleLog = "----" + currentView.GetTitle() + "\n";

            for (int i = 0; i < items.Count; i++) 
                consoleLog += items[i].GetIndex() + ".->" + items[i].GetName() + "\n";

            consoleLog += "---- нажмите клавишу Enter\n";
            
            Console.WriteLine(consoleLog);
        }

        public void ApplyCallback(int index)
        {
            MenuView currentView = _currentMenu;
            MenuItem item = currentView.GetItems().FirstOrDefault(it => it.GetIndex() == index);

            if (item == default)
                throw new ArgumentException($"Menu view not contains menuItem with index:{index}");
            
            Console.Clear();
            
            item.GetCallback()?.Invoke();
            
            Console.WriteLine("\n");
        }
    }
}