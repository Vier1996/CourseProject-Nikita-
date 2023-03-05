#define RUDOLF
//#define VIER

using System.Data;
using CourseProject.Codebase.Disposable;
using CourseProject.Utils.Logging;
using MySqlConnector;

namespace CourseProject.Codebase.MySQL
{
    public class MySqlConnector : IDispose
    {
        public event Action<ConnectionState> ConnectionStateChanged;
        
        public ConnectionState ConnectionState => _connection.State;

        private readonly MySqlConnectionStringBuilder _creditionals = new MySqlConnectionStringBuilder();
        private MySqlConnection _connection;

        public MySqlConnector()
        {
            #if RUDOLF
            _creditionals.Server = "127.0.0.1";
            _creditionals.Port = 3306;
            _creditionals.UserID = "root";
            _creditionals.Password = "viprudolf";
            _creditionals.Database = "projectdatabase";
            #endif

            #if VIER
            _creditionals.Server = "127.0.0.1";
            _creditionals.Port = 57736;
            _creditionals.UserID = "root";
            _creditionals.Password = "admin";
            _creditionals.Database = "coursedatabase";
            #endif

            // Server=127.0.0.1;Port=57736;User ID=root;Password=admin;Database=taskdatabase
            // Server=127.0.0.1;Port=57736;User ID=root;Password=admin;Database=coursedatabase
            _connection = new MySqlConnection(_creditionals.ToString());
        }

        public void CreateConnection()
        {
            _connection.StateChange += OnvConnectionStateChange;

            try
            {
                _connection.Open();
            }
            catch (MySqlException ex)
            {
                Logger.Log(ex.Message);
            }
        }
        
        public void ExecuteCommand(string command)
        {
            if (ConnectionState != ConnectionState.Open)
                throw new ArgumentException($"Can not execute customCommand with [ConnectionState:{ConnectionState}]");
            
            MySqlCommand innerCustomCommand = new MySqlCommand(command, _connection);
            
            innerCustomCommand.ExecuteNonQuery();
            
            using MySqlDataReader rdr = innerCustomCommand.ExecuteReader();
            string info = "";
            
            while (rdr.Read())
            {
                info = "";

                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    var asd = rdr.GetData(i);
                    info += rdr.GetData(i) + (i == rdr.FieldCount - 1 ? " || " : "");
                }
                
                Console.WriteLine(info);
            }
        }

        private void OnvConnectionStateChange(object sender, StateChangeEventArgs args) => 
            ConnectionStateChanged?.Invoke(args.CurrentState);


        public void Dispose() => _connection.Close();
    }
}