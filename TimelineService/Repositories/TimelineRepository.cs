using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using PostService.Repositories;
using TimelineService.Model;
using System.Text.Json;
using Newtonsoft.Json;

namespace TimelineService.Repositories
{
    public class TimelineRepository
    {
        public async Task<string> MakeTimeline(Guid userId)
        {
            FollowersRepository followersRepository = new FollowersRepository();
            var userFollowers = followersRepository.All(userId);
            List<UserPosts> testposts = new List<UserPosts>();
            foreach (var user in userFollowers.Result)
            {
               // Console.WriteLine(user.followerId);
                var singlePostFromUser = GetPostsFromUser(user.followerId);
                testposts.AddRange(singlePostFromUser);
            };
            List<UserPosts> SortedList = testposts.OrderByDescending(o => o.creationTime).ToList();

            //var firstFiveItems = SortedList.Take(5);
            var json = JsonConvert.SerializeObject(SortedList.Take(100));
            //Console.WriteLine(json);
            return json;
        }

        public List<UserPosts> GetPostsFromUser(Guid userId)
        {
            PostsRepository postsRepository = new PostsRepository();
            var userPosts = postsRepository.All(userId);
            List<UserPosts> userPost = new List<UserPosts>();
            foreach (var post in userPosts.Result)
            {
                userPost.Add(post);
            };
            List<UserPosts> SortedList = userPost.OrderBy(o=>o.creationTime).ToList();

            return SortedList;
        }
    }
}
