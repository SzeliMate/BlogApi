using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using BlogApi.Models.DTOs;
using System.Diagnostics;

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

        [HttpPost("register")]
        public object AddNewBlogger(AddNewBloggerDTos addNewBloggerDTos)
        {
            using var connector = new MySqlConnection(ConnectionString);
            connector.Open();
            string sql = @"INSERT INTO `blogger`(`Name`, `Email`, `Age`, `Password`, `RegistrationTime`) VALUES (@name, @email, @age, @password, @registrationTime)";
            using var cmd = new MySqlCommand(sql, connector);
            cmd.Parameters.AddWithValue("@name", addNewBloggerDTos.Name);
            cmd.Parameters.AddWithValue("@email", addNewBloggerDTos.Email);
            cmd.Parameters.AddWithValue("@age", addNewBloggerDTos.Age);
            cmd.Parameters.AddWithValue("@password", addNewBloggerDTos.Password);
            cmd.Parameters.AddWithValue("@registrationTime", DateTime.Now);
            cmd.ExecuteNonQuery();
            connector.Close();
            return new { message = "Sikeres hozzáadás", result = addNewBloggerDTos };
        }
        [HttpPost("login")]
        public object Login(LoginBloggerDTOs loginBloggerDTos)
        {
            using var connector = new MySqlConnection(ConnectionString);
            connector.Open();
            string sql = @"SELECT * FROM blogger WHERE Email=@Email AND Password=@Password";
            using var cmd = new MySqlCommand(sql, connector);
            cmd.Parameters.AddWithValue("@Email", loginBloggerDTos.Email);
            cmd.Parameters.AddWithValue("@Password", loginBloggerDTos.Password);
            using var datareader = cmd.ExecuteReader();
            if (datareader.Read() == true)
            {
                return new { message = "Sikeres belépés", result = datareader.GetInt32("Id") };
            }
            else
            {
                return new { message = "Sikertelen belépés", result = loginBloggerDTos};
            }
        }

        [HttpDelete("delete")]
        public object DeleteBlogger([FromBody] int id)
        {
            using var connector = new MySqlConnection(ConnectionString);
            connector.Open();
            string sql = @"DELETE FROM blogger WHERE Id=@id";
            using var cmd = new MySqlCommand(sql, connector);
            cmd.Parameters.AddWithValue("@id", id);
            if (cmd.ExecuteNonQuery() > 0)
            {
                return new { message = "Sikeres törlés", result = id };
            }
            else
            {
                return new { message = "Sikertelen törlés", result = id };
            }
        }
        [HttpPut("update")]
        public object UpdateBlogger([FromQuery] int id, [FromBody] UpdateBloggerDTOs updateBloggerDTos)
        {
            using var connector = new MySqlConnection(ConnectionString);
            connector.Open();
            string sql = @"UPDATE blogger SET Name=@name, Email=@email, Age=@age, Password=@password WHERE Id=@id";
            using var cmd = new MySqlCommand(sql, connector);
            cmd.Parameters.AddWithValue("@name", updateBloggerDTos.Name);
            cmd.Parameters.AddWithValue("@email", updateBloggerDTos.Email);
            cmd.Parameters.AddWithValue("@age", updateBloggerDTos.Age);
            cmd.Parameters.AddWithValue("@password", updateBloggerDTos.Password);
            cmd.Parameters.AddWithValue("@id", updateBloggerDTos.Id);
            if (cmd.ExecuteNonQuery() > 0)
            {
                return new { message = "Sikeres frissítés", result = updateBloggerDTos };
            }
            else
            {
                return new { message = "Sikertelen frissítés", result = updateBloggerDTos };
            }
        }

        [HttpGet("count")]
        public object GetBloggerCount()
        {
            using var connector = new MySqlConnection(ConnectionString);
            connector.Open();
            string sql = @"SELECT COUNT(*) FROM blogger";
            using var cmd = new MySqlCommand(sql, connector);

            int count = Convert.ToInt32(cmd.ExecuteScalar());

                return new { message = "Regisztrált tagok száma sikeresen lekérdezve", result = count };
            }

            [HttpGet("orderedlist")]
            public object GetBloggersOrdered()
            {
                var orderedBloggers = new List<object>();
                using var connector = new MySqlConnection(ConnectionString);
                connector.Open();
                string sql = @"SELECT Name, Email FROM blogger ORDER BY Name ASC";
                using var cmd = new MySqlCommand(sql, connector);
                using var datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    orderedBloggers.Add(new
                    {
                        Name = datareader.GetString("Name"),
                        Email = datareader.GetString("Email")
                    });
                }

                return new { message = "Bloggerek listája ABC sorrendben", result = orderedBloggers };
            }

    }
}

