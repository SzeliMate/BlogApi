using Scalar.AspNetCore;
using System.Runtime.CompilerServices;

namespace BlogApi
{
    public class Blog
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Password { get; set; } = string.Empty;
        public DateTime RegistrationTime { get; set; }
    
public Blog(int Id, string Email, int Age, string Password, DateTime RegistrationTime)
        {
            this.Id = Id;
            this.Email = Email;
            this.Age = Age;
            this.Password = Password;
            this.RegistrationTime = RegistrationTime;
        }
    }
}   
