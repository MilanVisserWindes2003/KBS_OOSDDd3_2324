using DataAccess.Mistakes;
using Skepta.DataAcces.HistoryFolder;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class DataAccess
    {
        private const string connectionString = "Server=tcp:145.44.233.245,443;Initial Catalog=Skepta;User ID = sa; Password = DDSL@1379ESA; Connect Timeout=12;";

        public SqlConnection Connection { get; set; }
        
        //Gets all texts with given level and length from the database
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

        //Checks if username exists in the database, returns true if exists
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

        //Gets the password from the given usernam
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

        //Adds a new account to the acccount table in the database
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

        //Returns the textId from a text with the given  level, length and content. Content is the text itself
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

        //Gets the last 10 rows from the history table from the given username
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

        //Adds a new exercise made in the history table.
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

        //Changes the password in the database with the given username and the new password
        public bool ChangePassword(string username, string newPassword)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(newPassword))
            {
                //When there is bad info
                return false;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE [dbo].[user] SET [password] = @NewPassword WHERE [username] = @Username";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NewPassword", newPassword);
                        command.Parameters.AddWithValue("@Username", username);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0; // (password has changed)
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        //Removes an account from the whole database
        public bool RemoveAccount(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Searches for the account
                    string checkQuery = "SELECT COUNT(*) FROM [dbo].[user] WHERE [username] = @Username";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Username", username);
                        int count = (int)checkCommand.ExecuteScalar();
                        if (count == 0)
                        {
                            //Account doesn't exist
                            return false;
                        }
                    }
                    // Delete the history from the account
                    string deleteHistoryQuery = "DELETE FROM [dbo].[history] WHERE [username] = @Username";
                    using (SqlCommand deleteHistoryCommand = new SqlCommand(deleteHistoryQuery, connection))
                    {
                        deleteHistoryCommand.Parameters.AddWithValue("@Username", username);
                        deleteHistoryCommand.ExecuteNonQuery();
                    }

                    //Remove account
                    string deleteQuery = "DELETE FROM [dbo].[user] WHERE [username] = @Username";

                    using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@Username", username);

                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        //Gets all the texts that have the worstmistake of the user with the given level and length. If no mistakes were found it takes a random text.
        public List<string> GetPersonalizedTexts(string level, string length, string username)
        {
            List<string> texts = new List<string>();
            
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(level) || string.IsNullOrEmpty(length))
            {
                return null;
            }
            List<Mistake> mistakes = GetWorstMistake(username);
            if (mistakes == null || mistakes.Count == 0)
            {
                return null;
            }
            foreach (Mistake mistake in mistakes) 
            {
                texts = ExecutePersonalizedTexts(level, length, mistake.CharMistake);
                if (texts == null || texts.Count == 0)
                {
                    return null;
                }
                texts = ExecutePersonalizedTexts(level, length, mistake.CharMistake);
                return texts;   
            }
            return null;
        }

        //Gets the worst mistakes from the username in order from most made mistake to the least.
        private List<Mistake> GetWorstMistake(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            List<Mistake> mistakes = new List<Mistake>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT worstMistake, COUNT(*) AS MistakeCount FROM history WHERE username = @username AND worstMistake <> '-' GROUP BY worstMistake ORDER BY MistakeCount DESC;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username); 
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                return null;
                            }
                            while (reader.Read())
                            {
                                Mistake mistake = new Mistake();
                                mistake.CharMistake = reader["worstMistake"].ToString();
                                mistake.Count = int.Parse(reader["MistakeCount"].ToString());
                                mistakes.Add(mistake);
                            }
                        }
                    }
                    return mistakes;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        //Executes the query to get the personalized texts with given level, length and the character
        private List<string> ExecutePersonalizedTexts(string level, string length, string character)
        {
            List<string> texts = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT content FROM text WHERE CHARINDEX(@Character, content) > 0 AND length = @Length AND level = @Level;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Character", character);
                        command.Parameters.AddWithValue("@Level", level);
                        command.Parameters.AddWithValue("@Length", length);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                return null;
                            }
                            while (reader.Read())
                            {
                                string tekst = reader["content"].ToString();
                                texts.Add(tekst);
                            }
                        }
                    }

                    return texts;
                }
              finally 
                {
                    connection.Close();
                }
            }
        }       
    }
}
    
