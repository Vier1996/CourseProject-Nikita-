namespace CourseProject.Codebase.Menu
{
    public class MenuItem
    {
        private int _index = -1;
        private string _name = "";
        private Action _callback = null;

        public MenuItem(int index, string name, Action callback)
        {
            _index = index;
            _name = name;
            _callback = callback;
        }

        public int GetIndex() => _index;
        public string GetName() => _name;
        public Action GetCallback() => _callback;
    }
}