using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using skybeat.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Http;

namespace skybeat.Controllers
{
    public class LoginController : Controller
    {
        //Connection string
        private string MyConnection = "datasource=localhost;port=3306;database=skybeat;user=root;password=";
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
        public IActionResult Index(LoginModel model)
        {
            //try
            //{
                //hashedData will be Hashed password, in your case
                string hashedData = ComputeSha256Hash(model.Password);
                //Query for login   
                string Query = "select username, email, password from user where email = `"+ model.email+"` and password = `"+ model.Password +"`";
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
                if(MyReader.HasRows)
                {
                    CookieOptions option = new CookieOptions();
                    Response.Cookies.Append("username", MyReader["username"].ToString(), option);
                    Response.Cookies.Append("email", model.email, option);
                    Response.Cookies.Append("username", hashedData, option);

                    ViewBag.message = model.email;
                }
                return RedirectToAction("Login", "Home");
                
           // }
            /*catch
            {
                return RedirectToAction("Error", "Home");
            }*/
        }
    }
}
