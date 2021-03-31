using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using skybeat.Models;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto;
using System.Text;
using System.Security.Cryptography;

namespace skybeat.Controllers
{
    public class RegisterController : Controller
    {
        //Connection string
        private string MyConnection = "datasource=localhost;port=3306;database=skybeat;user=root;password=";
        public string ConnectionString { get; set; }
        public RegisterController(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            try
            {
                //hashedData will be Hashed password, in your case
                string hashedData = ComputeSha256Hash(model.Password);
                //Query for insert registered user    
                string Query = "insert into `user`(username,`password`,email) values('" + model.Username + "','" + model.Password + "','" + model.Email + "');";
                //Connection to MySQL Database    
                MySqlConnection MyConn = new MySqlConnection(MyConnection);
                //Connect to Database using MySqlCommand class    
                MySqlCommand MyCommand = new MySqlCommand(Query, MyConn);
                //Creating reader for Query    
                MySqlDataReader MyReader;
                //Opening connetion    
                MyConn.Open();
                //Executing Query    
                MyReader = MyCommand.ExecuteReader();
                return RedirectToAction("Index","Home");
            }
            catch 
            {
                return RedirectToAction("Error", "Home");
            }
        }

    }
}
