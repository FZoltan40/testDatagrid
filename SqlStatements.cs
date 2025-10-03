using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;

namespace ComputerShop
{
    internal class SqlStatements
    {
        Connect conn = new Connect();
        public bool LoginUser(string username, string userpassword)
        {
            conn.Connection.Open();

            string sql = "SELECT Id FROM users  WHERE `UserName`=@username AND `Password`=@password;";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", userpassword);

            MySqlDataReader dr = cmd.ExecuteReader();
            bool isValid = dr.Read();

            dr.Close();
            conn.Connection.Close();
            return isValid;
        }

        public string registerUser(string username, string password, string fullname, string email)
        {
            try
            {
                conn.Connection.Open();

                string sql = "INSERT INTO `users`(`UserName`, `Password`, `FullName`, `Email`) VALUES (@username,@password,@fullname,@email)";

                MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@fullname", fullname);
                cmd.Parameters.AddWithValue("@email", email);

                cmd.ExecuteNonQuery();

                conn.Connection.Close();

                return "Sikeres regisztráció";
            }
            catch (System.Exception ex)
            {

                return ex.Message;
            }

        }

        public DataView GetAllUsers()
        {
            conn.Connection.Open();

            string sql = "SELECT * FROM users";

            MySqlDataAdapter data = new MySqlDataAdapter(sql, conn.Connection);

            DataTable dt = new DataTable();
            data.Fill(dt);

            conn.Connection.Close();

            return dt.DefaultView;

        }

        public void DeleteUser(object id)
        {
            try
            {
                conn.Connection.Open();

                string sql = "DELETE FROM users WHERE id = @id";

                MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();

                conn.Connection.Close();

                MessageBox.Show("Sikeres törlés.");

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateUser(object user)
        {
            try
            {
                conn.Connection.Open();

                var result = user.GetType().GetProperties();

                string sql = "UPDATE `users` SET `UserName`= @username,`Password`= @password,`FullName`= @fullname,`Email`= @email,`RegDate`= @date WHERE Id = @id ";

                MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

                cmd.Parameters.AddWithValue("@id", result[0].GetValue(user));
                cmd.Parameters.AddWithValue("@username", result[1].GetValue(user));
                cmd.Parameters.AddWithValue("@password", result[2].GetValue(user));
                cmd.Parameters.AddWithValue("@fullname", result[3].GetValue(user));
                cmd.Parameters.AddWithValue("@email", result[4].GetValue(user));
                cmd.Parameters.AddWithValue("@date", result[5].GetValue(user));

                cmd.ExecuteNonQuery();

                conn.Connection.Close();

                MessageBox.Show("Sikeres módosítás.");

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
