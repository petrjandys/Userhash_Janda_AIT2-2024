using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Hesla
{
    internal class DataHandler
    {
        public static Users currentUser = null;
        public static string connectionString = "Data Source=|DataDirectory|\\myDatabase.db;Version=3;";
        public static List<Users> seznamUzivatelu = new List<Users>();

        public static void InitializeDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = "CREATE TABLE IF NOT EXISTS Users (UserName TEXT, Password TEXT, IsAdmin INTEGER);";
                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void LoadFromDatabase()
        {
            seznamUzivatelu.Clear();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Users;";
                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Users user = new Users
                            {
                                userName = reader["UserName"].ToString(),
                                password = reader["Password"].ToString(),
                                isAdmin = Convert.ToBoolean(reader["IsAdmin"])
                            };

                            seznamUzivatelu.Add(user);
                        }
                    }
                }
            }
        }
    }
}
