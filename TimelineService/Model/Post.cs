namespace TimelineService.Model
{

    public class Post
    {
        // ? is used to allow a value type to set to null
        public int? Id { get; set; }
        public string? Text { get; set; }
        public string? UserId { get; set; }
        public string? Likes { get; set; }
    }
}