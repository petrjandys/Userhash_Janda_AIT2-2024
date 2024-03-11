using System;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Hesla
{
    [Serializable]
    public class Users
    {
        public string userName { get; set; }
        public string password { get; set; }
        public bool isAdmin { get; set; }

        public static void SaveToDatabase()
        {
            try
            {
                Users currentUser = DataHandler.currentUser;

                if (currentUser != null)
                {
                    using (SQLiteConnection connection = new SQLiteConnection(DataHandler.connectionString))
                    {
                        connection.Open();

                        string insertQuery = "INSERT INTO Users (UserName, Password, IsAdmin) VALUES (@UserName, @Password, @IsAdmin);";
                        using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@UserName", currentUser.userName);
                            command.Parameters.AddWithValue("@Password", currentUser.password);
                            command.Parameters.AddWithValue("@IsAdmin", currentUser.isAdmin ? 1 : 0);

                            command.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                 //   MessageBox.Show("Aktuální uživatel není nastaven.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba při ukládání uživatele: " + ex.Message);
            }
        }


        public static void SaveXML()
        {
            // Ponechte tuto metodu pro zpětnou kompatibilitu, i když nyní používáme databázi.
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}