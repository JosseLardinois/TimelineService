namespace TimelineService.Model
{
    public class AppConfig
    {

        public string? TimelinePostsQueueUrl { get; set; }
        public string? AccessKeyId { get; set; }
        public string? SecretAccessKey { get; set; }
        public string? TimelineFollowerQueueUrl { get; set; }
    }
}
