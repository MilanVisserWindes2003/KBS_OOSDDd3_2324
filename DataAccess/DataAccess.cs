using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using MySql.Data.MySqlClient;

namespace DataAccess
{
    public class DataAccess
    {
        bool isConnected = false;
        private const string ConnectionDatabase = "Server=localhost;Database=Skepta;User ID=root;Passord=;";
        public MySqlConnection Connection { get; set; }

        public void TableLerenTypenConnection()
        {
            this.Connection = new MySqlConnection(ConnectionDatabase);
        }

        public bool UsernameExists(string username)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionDatabase))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM inloggegevens WHERE gebruikersnaam = @gebruikersnaam", connection);
                    command.Parameters.AddWithValue("@gebruikersnaam", username);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                finally { connection.Close(); }
            }
        }
        public string GetPassword(string username)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionDatabase))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT wachtwoord FROM inloggegevens WHERE gebruikersnaam = @gebruikersnaam", connection);
                    command.Parameters.AddWithValue("@gebruikersnaam", username);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Return the password instead of the username
                            return reader["wachtwoord"].ToString();
                        }
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"GetPassword Exception: {ex}");
                    return null;
                }
                finally { connection.Close(); }
            }
        }


        public bool Register(String username, String password)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionDatabase))
            {
                try
                {
                    connection.Open();
                    // Register with Values for Colum @Gebruikersnaam and @Wachtwoord
                    MySqlCommand command = new MySqlCommand("INSERT INTO inloggegevens (gebruikersnaam, wachtwoord) VALUES (@gebruikersnaam, @wachtwoord)", connection);
                    command.Parameters.AddWithValue("@gebruikersnaam", username);
                    command.Parameters.AddWithValue("@wachtwoord", password);
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if the registration was successful
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Registration Exception: {ex}");
                    return false;
                }
                finally { connection.Close(); }
            }
        }
    }
}