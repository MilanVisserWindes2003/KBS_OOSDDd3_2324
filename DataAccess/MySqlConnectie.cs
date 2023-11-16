using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataAccess
{
    public class MySqlConnectie
    {
        private const string ConnectionDatabase = "server=localhost;user=root;database=Skepta;password=";

        public MySqlConnection TableLerenTypenConnection()
        {
            return new MySqlConnection(ConnectionDatabase);
        }

        public String Login(String username, String password)
        {
            using (MySqlConnection connect = TableLerenTypenConnection())
            {
                try
                {
                    connect.Open();
                    // Login with Values for Colum @Gebruikersnaam and @Wachtwoord (if the same)
                    MySqlCommand command = new MySqlCommand("SELECT * FROM inloggen WHERE Username = @Gebruikersnaam AND Password = @Wachtwoord", connect);
                    command.Parameters.AddWithValue("@Gebruikersnaam", username);
                    command.Parameters.AddWithValue("@Wachtwoord", password);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return "Login successful";
                        }
                    }
                    return "Onjuiste gebruikersnaam en/of wachtwoord";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Login Exception: {ex}");
                    return "Login exception occurred";
                }
            }
        }
        public String Register(String username, String password)
        {
            using (MySqlConnection connect = TableLerenTypenConnection())
            {
                try
                {
                    connect.Open();
                    // Register with Values for Colum @Gebruikersnaam and @Wachtwoord
                    MySqlCommand command = new MySqlCommand("INSERT INTO inloggen (Gebruikersnaam, Wachtwoord) VALUES (@Gebruikersnaam, @Wachtwoord)", connect);
                    command.Parameters.AddWithValue("@Gebruikersnaam", username);
                    command.Parameters.AddWithValue("@Wachtwoord", password);
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if the registration was successful
                    if (rowsAffected > 0)
                    {
                        return "Registration successful";
                    }
                    else
                    {
                        return "Deze gebruikersnaam is al in gebruik";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Registration Exception: {ex}");
                    return "Registration exception occurred";
                }
            }
        }
        public void GrantAccess()
        {
            //StartScreen start = new StartScreen();
            //start.Show();
        }
    }
}
