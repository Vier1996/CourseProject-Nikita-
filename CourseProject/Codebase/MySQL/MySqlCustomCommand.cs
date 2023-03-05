namespace CourseProject.Codebase.MySQL
{
    public class MySqlCustomCommand
    {
        private MySqlCommandTranslator _commandTranslator;
        
        private string _executeCommand = "";

        public MySqlCustomCommand() => _commandTranslator = new MySqlCommandTranslator();

        public string GetExecuteCommand() => _executeCommand;
        
        public void SelectOption(string tablePropertyName) => 
            _executeCommand += _commandTranslator.Selection(tablePropertyName) + "\n";
    }
}