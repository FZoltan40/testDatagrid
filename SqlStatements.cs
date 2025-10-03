using MySql.Data.MySqlClient;
using System.Data;

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
    }
}
