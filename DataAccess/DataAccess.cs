using Skepta.DataAcces.HistoryFolder;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Renci.SshNet;
using Renci.SshNet.Common;
using System.Diagnostics;

namespace DataAccess
{
    public class DataAccess
    {
        bool isConnected = false;
        //private const string connectionString = "Server=localhost,11433;Database=Skepta;User ID=sa;Password=DDSL@1379ESA;Trusted_Connection=True;Encrypt=True;Connection Timeout=5;";

        private const string connectionString =
            "Server=tcp:145.44.233.245,443;Initial Catalog=Skepta;User ID = sa; Password = DDSL@1379ESA; Connect Timeout=12;";
        //private static KnownHostStore _knownHosts;

        //"Server=.\\SQLEXPRESS; Database = Skepta; Integrated Security = true;"
        //"Server=tcp:127.0.0.1,11433;Initial Catalog=Skepta;User ID = sa; Password = DDSL@1379ESA; Connect Timeout=12;";
        //private const string connectionString =  "data source=localhost;Initial Catalog=Skepta; User ID=sa;Password=Sekrap40";
        public SqlConnection Connection { get; set; }

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

        public string ObtainTextId(string level, string length, string content)
        {
            string textId = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT [id] FROM [dbo].[text] WHERE [level] = @Level AND [length] = @Length AND [content] = @Content";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Level", level);
                        command.Parameters.AddWithValue("@Length", length);
                        command.Parameters.AddWithValue("@Content", content);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                textId = reader["id"].ToString();

                            }
                        }
                    }

                    return textId;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public History ObtainHistory(string username)
        {
            if (username == null)
            {
                return null;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    History history = new History();

                    // Use TOP 10 to limit the result to a maximum of 10 rows
                    string query = @"
                SELECT TOP 10 t.level, t.length, h.typespeed, h.worstMistake, h.spoken
                FROM text t
                LEFT JOIN history h ON t.id = h.TextID
                WHERE h.username = @username
                ORDER BY h.historyID DESC;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable historyTable = new DataTable();
                            historyTable.Load(reader);
                            history.HistoryTable = historyTable;
                        }

                        return history;
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public void InsertHistoryData(History history)
        {
            if (history == null)
            {
                // Handle invalid input if needed
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Use a parameterized query for better security
                    string query = @"
                INSERT INTO [dbo].[history] 
                ([username], [textID], [typespeed], [worstMistake], [spoken]) 
                VALUES 
                (@username, @textId, @typeSpeed, @worstMistake, @spoken)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", history.Username);
                        command.Parameters.AddWithValue("@textId", history.TextId);
                        command.Parameters.AddWithValue("@typeSpeed", history.TypeSpeed);
                        command.Parameters.AddWithValue("@worstMistake", history.WorstMistake);
                        command.Parameters.AddWithValue("@spoken", history.IsSpoken);

                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }

    public class KnownHostsManager
    {
        private Dictionary<string, byte[]> knownHosts;

        public KnownHostsManager()
        {
            // Initialize your known hosts here
            // For example, load them from a file or database
            knownHosts = new Dictionary<string, byte[]>();
        }

        public void AddKnownHost(string hostname, byte[] hostKey)
        {
            knownHosts[hostname] = hostKey;
        }

        public bool IsHostKnown(string hostname, byte[] hostKey)
        {
            return knownHosts.TryGetValue(hostname, out var knownKey) && AreKeysEqual(knownKey, hostKey);
        }

        private bool AreKeysEqual(byte[] key1, byte[] key2)
        {
            if (key1.Length != key2.Length)
                return false;

            for (int i = 0; i < key1.Length; i++)
            {
                if (key1[i] != key2[i])
                    return false;
            }
            return true;
        }
    }

    public class SshConnection
    {
        public static void Connect(string host, int port, string username, string password)
        {
            KnownHostsManager knownHostsManager = new KnownHostsManager();
            // Populate knownHostsManager with your known hosts

            using (var client = new SshClient(host, port, username, password))
            {
                client.HostKeyReceived += (sender, e) =>
                {
                    if (!knownHostsManager.IsHostKnown(host , e.HostKey))
                    {
                        e.CanTrust = false; // Or handle this according to your policy
                    }
                };

                client.Connect();
                // Perform your SSH operations
                client.Disconnect();
            }
        }
    }





}
    
