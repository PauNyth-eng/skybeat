using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using skybeat.Models;
using MySql.Data.MySqlClient;


namespace skybeat.Data
{
    public class SkybeatContext
    {
        public string ConnectionString { get; set; }
        public SkybeatContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }


        public List<UserSetting> GetAllUsersSetting()
        {
            List<UserSetting> list = new List<UserSetting>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select * `user` where id < 10, conn");
                using (var reader = cmd.ExecuteReader())
                {
                    list.Add(new UserSetting()
                    {
                        id_user = Convert.ToInt32(reader["id_user"]),
                        username = reader["username"].ToString(),
                        password = reader["password"].ToString(),
                        email = reader["email"].ToString(),
                        id_role = Convert.ToInt32(reader["id_role"])
                    });
                }
            }
            return list;
        }
    }
}
