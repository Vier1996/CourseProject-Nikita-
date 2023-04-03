namespace CourseProject.Codebase.Menu
{
    public class MenuView // класс который представляет собой меню
    {
        private List<MenuItem> _menuItems; // список эллементов
        private string _title; // заглавное название
        
        public string GetTitle() => _title; // получение заглавного названия
        
        public List<MenuItem> GetItems() => _menuItems; // получение эллементов
        
        public MenuView(string title) //конструктор класса
        {
            _title = title; // присваивание заглавного названия
            _menuItems = new List<MenuItem>(); // присвоение эллементов
        }

        public void AddMenuItem(MenuItem item) // метод добавленя эллемента меню
        {
            if(!_menuItems.Contains(item)) // проверяем есть ли уже такой эллемент, если нет
                _menuItems.Add(item); // то добавляем эллемент
        }
    }
}