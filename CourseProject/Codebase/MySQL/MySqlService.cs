using System.Data;
using CourseProject.Codebase.Disposable;

namespace CourseProject.Codebase.MySQL
{
    public class MySqlService : IDispose
    {
        public event Action ConnectionSuccessfullyEstablished;
        public static MySqlService Instance
        {
            get
            {
                if (_instance == null) 
                    _instance = new MySqlService();
                return _instance;
            }
        }

        public bool IsReady
        {
            get
            {
                bool state = false;

                try { state = _connector.ConnectionState == ConnectionState.Open; }
                catch (Exception E) { state = false; }

                return state;
            }
        }

        private readonly MySqlConnector _connector;
        private static MySqlService _instance;

        private MySqlService()
        {
            _connector = new MySqlConnector();
            _connector.ConnectionStateChanged += OnConnectionStateChanged;
        }
        
        public void ConnectToDataBase()
        {
            if(IsReady) return;
            
            _connector.CreateConnection();
        }

        public void ExecuteCommand(MySqlCustomCommand customCommand) => 
            _connector.ExecuteCommand(customCommand.GetExecuteCommand());

        private void OnConnectionStateChanged(ConnectionState state)
        {
            switch (state)
            {
                case ConnectionState.Open: ConnectionSuccessfullyEstablished?.Invoke(); break;
                case ConnectionState.Fetching: break;
                case ConnectionState.Executing: break;
                case ConnectionState.Connecting: break;
                case ConnectionState.Broken: break;
                case ConnectionState.Closed: break;
            }
        }

        public void Dispose()
        {
            _connector.ConnectionStateChanged -= OnConnectionStateChanged;
        }
    }
}