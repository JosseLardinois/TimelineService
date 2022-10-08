using Amazon.DynamoDBv2.DataModel;

namespace TimelineService.Model
{
    public class UserPosts
    {
        public string? text { get; set; }
        public Guid userId { get; set; }
        public DateTime creationTime { get; set; }


    }
}