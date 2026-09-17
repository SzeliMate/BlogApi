using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;

namespace BlogApi.Controllers
{
    [Route("bloggers")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private string ConnectionString = "Server=localhost;Database=Blog;uid=root;password=;";

        [HttpGet("all")]
        public object GetAllBlogger()
        {
            List<Blog> bloggers = new List<Blog>();
            using var connector = new MySqlConnection(ConnectionString);
            connector.Open();

            string sql = @"SELECT * FROM blogger";

            using var cmd = new MySqlCommand(sql, connector);
            using var datareader = cmd.ExecuteReader();
            while (datareader.Read())
            {
                var blogger = new Blog
                {
                    Id = datareader.GetInt32("Id"),
                    Name = datareader.GetString("Name"),
                    Email = datareader.GetString("Email"),
                    Age = datareader.GetInt32("Age"),
                    Password = datareader.GetString("Password"),
                    RegistrationTime = datareader.GetDateTime("RegistrationTime")
                };
                bloggers.Add(blogger);
            }
            return new { message = "Sikeres lekérdezés", result = bloggers };
        }

        [HttpGet("byid")]
        public object GetBloggerId(int id)
        {
            using var connector = new MySqlConnection(ConnectionString);
            connector.Open();
            string sql = @"SELECT * FROM blogger WHERE Id=@id";
            using var cmd = new MySqlCommand(sql, connector);
            cmd.Parameters.AddWithValue("@id", id);
            using var datareader = cmd.ExecuteReader();
            if (!datareader.Read())
            {
                return new { message = "Nem található", result = (Blog?)null };
            }
            var blogger = new Blog
            {
                Id = datareader.GetInt32("Id"),
                Name = datareader.GetString("Name"),
                Email = datareader.GetString("Email"),
                Age = datareader.GetInt32("Age"),
                Password = datareader.GetString("Password"),
                RegistrationTime = datareader.GetDateTime("RegistrationTime")
            };
            return new { message = "Sikeres találat", result = blogger };
        }
    }
}
