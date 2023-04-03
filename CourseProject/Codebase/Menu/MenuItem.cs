namespace CourseProject.Codebase.Menu
{
    public class MenuItem // класс представляющий элемент менюхи
    {
        private int _index = -1; // индекс меню
        private string _name = ""; // название меню
        private Action _callback = null; // обратный вызов (действие)

        public MenuItem(int index, string name, Action callback) // конструктор класса
        {
            _index = index; // присваивание индекса
            _name = name; // присваивание названия
            _callback = callback; // присваивание дествия
        }

        public int GetIndex() => _index; // получение индекса
        public string GetName() => _name; // получение названия
        public Action GetCallback() => _callback; // получения дествия
    }
}