using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("bloggers")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        public object GetAllBlogger()
        {
            return new
            { message = "Sikeres lekérdezés" };


            {
            }
        }
    }
}
