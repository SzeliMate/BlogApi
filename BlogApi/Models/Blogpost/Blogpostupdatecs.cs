namespace BlogApi.Models.Blogpost
{
    public class Blogpostupdatecs
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime postTime { get; set; }
        public DateTime updateTime { get; set; }
        public int blogId { get; set; }
    }
}
