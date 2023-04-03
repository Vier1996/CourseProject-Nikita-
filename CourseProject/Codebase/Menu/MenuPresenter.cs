namespace CourseProject.Codebase.Menu
{
    public class MenuPresenter // синглтон класс обрабатывающий логику меню
    {
        public static MenuPresenter Instance // ссылка на экземпляр обработчика меню
        {
            get
            {
                if (_instance == null) // проверка экземпляра на существование
                    _instance = new MenuPresenter(); // созданеи экземпляра обработчика меню
                return _instance; // возвращение экземпдяра
            }
        }
     
        private static MenuPresenter _instance; // приватный экземпдяр на обработчик меню
        
        private MenuView _currentMenu = null; // экземпляр текущей меню

        public void SetCurrentMenu(MenuView menuView) => _currentMenu = menuView; // метод смены текущей меню

        public void ShowMenu() // метод показа меню
        {
            MenuView currentView = _currentMenu; // кеширование меню
            List<MenuItem> items = currentView.GetItems(); // получение элементов меню

            string consoleLog = "[" + currentView.GetTitle() + "]\n"; // формирование строки меню

            for (int i = 0; i < items.Count; i++) // проходим по элементам меню
                consoleLog += items[i].GetIndex() + ".-> " + items[i].GetName() + "\n"; // добавляем данные элемента в логи

            consoleLog += "--> Чтобы продолжить, введите число и нажмите клавишу (Enter)\n"; // добавляем конечную информацию в лог
            
            Console.WriteLine(consoleLog); // выводим лог
        }

        public void ApplyCallback(int index) // метод применения действия
        {
            MenuView currentView = _currentMenu; // кеширование меню
            MenuItem item = currentView.GetItems().FirstOrDefault(it => it.GetIndex() == index); // проходим по элементам и выбираем тот у которого индекс соответсвует передаваемому

            if (item == default) // проверка найден ли эллемент
                throw new ArgumentException($"Menu view not contains menuItem with index:{index}"); // выводим исключение о не найденном эллементе
            
            Console.Clear(); // чистим логи в консоли
            
            item.GetCallback()?.Invoke(); // выполняем действие
        }
    }
}