using Renci.SshNet;
using System.Data.SqlClient;

namespace DataAccess
{
    public class DataAccess
    {
        bool isConnected = false;
        private string connectionString =
        "Server=.\\SQLEXPRESS; Database = Skepta; Integrated Security = true;";
        // $"Server={SshHostName},{LocalForwardedPort};Database=Skepta;User Id={SshUserName};Password={SshPassword};"
        private const string SshHostName = "145.44.233.245";
        private const string SshUserName = "student";
        private const string SshPassword = "LerenTP321!";

        private const string SqlServerHostName = "localhost";
        private const int SqlServerPort = 1433;
        private const int LocalForwardedPort = 3333;

        private SshClient _sshClient;
        private ForwardedPortLocal _forwardedPort;
        //private const string connectionString =  "data source=localhost;Initial Catalog=Skepta; User ID=sa;Password=Sekrap40";
        public SqlConnection Connection { get; set; }

        public void CreateTunnel()
        {
            _sshClient = new SshClient(SshHostName, SshUserName, SshPassword);
            _sshClient.Connect();

            _forwardedPort = new ForwardedPortLocal("127.0.0.1", LocalForwardedPort, SqlServerHostName, (uint)SqlServerPort);
            _sshClient.AddForwardedPort(_forwardedPort);
            _forwardedPort.Start();
        }

        public void CloseTunnel()
        {
            _forwardedPort?.Stop();
            _sshClient?.Disconnect();
        }
        

        public void TableLerenTypenConnection()
        {
            this.Connection = new SqlConnection(connectionString);
        }       
        
        public List<string> ObTainTexts(string level, int length)
        {
            using (Connection = new SqlConnection(connectionString))
            {
                try
                {
                    Connection.Open();
                    List<string> teksten = new List<string>();
                    string query = "SELECT [content] FROM [dbo].[text] WHERE [level] = @Level AND [length] = @Length";
                    using (SqlCommand command = new SqlCommand(query, Connection))
                    {
                        command.Parameters.AddWithValue("@level", level);
                        command.Parameters.AddWithValue("@length", length);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string tekst = reader["content"].ToString();
                                teksten.Add(tekst);
                            }
                        }
                        return teksten;
                    }
                }
                finally
                {
                    Connection.Close();
                }
            }
        } 

        public bool UsernameExists(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM [dbo].[user] WHERE [username] = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        public string GetPassword(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT [password] FROM [dbo].[user] WHERE [username] = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    object result = command.ExecuteScalar();

                    return result != null ? result.ToString() : null;
                }
            }
        }

        public bool Register(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO [dbo].[user] ([username], [password]) VALUES (@Username, @Password)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
    }
}