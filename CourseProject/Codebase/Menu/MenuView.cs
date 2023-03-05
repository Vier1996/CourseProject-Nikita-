namespace CourseProject.Codebase.Menu
{
    public class MenuView
    {
        private List<MenuItem> _menuItems;
        private string _title;
        
        public string GetTitle() => _title;
        
        public List<MenuItem> GetItems() => _menuItems;
        
        public MenuView(string title)
        {
            _title = title;
            _menuItems = new List<MenuItem>();
        }

        public void AddMenuItem(MenuItem item)
        {
            if(!_menuItems.Contains(item))
                _menuItems.Add(item);
        }
    }
}