using Renci.SshNet;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DataAccess
{
    public class DataAccess
    {
        bool isConnected = false;

        private string connectionString =
            //"Server=.\\SQLEXPRESS; Database = Skepta; Integrated Security = true;";
            $"Server=localhost,{LocalForwardingPort};Database=Skepta;User Id={SshUsername};Password={SshPassword};";
        private const string SshServerHost = "145.44.233.245";
        private const string SshUsername = "student";
        private const string SshPassword = "LerenTP321!";

        private const string LocalHost = "127.0.0.1";
        private const string RemoteSqlServerHost = "localhost";
        private const int RemoteSqlServerPort = 1433;
        private const int LocalForwardingPort = 3333;

        private SshClient _sshClient;
        private ForwardedPortLocal _forwardedPort;
        //private const string connectionString =  "data source=localhost;Initial Catalog=Skepta; User ID=sa;Password=Sekrap40";
        public SqlConnection Connection { get; set; }

        public void CreateTunnel()
        {
            _sshClient = new SshClient(SshServerHost, SshUsername, SshPassword);
            _sshClient.Connect();

            _forwardedPort = new ForwardedPortLocal(LocalHost, RemoteSqlServerHost, (uint)RemoteSqlServerPort);
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
            List<string> teksten = new List<string>();
            using (Connection = new SqlConnection(connectionString))
            {
                try
                {
                    Connection.Open();
                    string query = "SELECT [content] FROM [dbo].[text] WHERE [level] = @Level AND [length] = @Length";
                    using (SqlCommand command = new SqlCommand(query, Connection))
                    {
                        command.Parameters.AddWithValue("@level", level);
                        command.Parameters.AddWithValue("@length", length);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                teksten.Add(reader["content"].ToString());

                            }
                        }
                    }
                }
                finally
                {
                    Connection.Close();
                }
            }
            return teksten;
        } 

        public bool UsernameExists(string username)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var query = "SELECT COUNT(*) FROM [dbo].[user] WHERE [username] = @Username";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        return count > 0;
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public string GetPassword(string username)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var query = "SELECT [password] FROM [dbo].[user] WHERE [username] = @Username";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        object result = command.ExecuteScalar();

                        return result?.ToString();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool Register(string username, string password)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var query = "INSERT INTO [dbo].[user] ([username], [password]) VALUES (@Username, @Password)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}