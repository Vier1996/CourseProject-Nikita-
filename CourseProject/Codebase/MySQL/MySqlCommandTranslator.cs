namespace CourseProject.Codebase.MySQL
{
    public class MySqlCommandTranslator
    {
        public string Selection(string tablePropertyName)
        {
            string command = $"SELECT * FROM {tablePropertyName};";
            return command;
        }
    }
}