using Scalar.AspNetCore;
using System.Runtime.CompilerServices;

namespace BlogApi
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Password { get; set; } = string.Empty;
        public DateTime RegistrationTime { get; set; }

    }
}
