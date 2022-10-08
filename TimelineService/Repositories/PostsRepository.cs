using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using PostService.Interfaces;
using Amazon;
using PostService.Models;
using TimelineService.Model;

namespace PostService.Repositories
{
    public class PostsRepository : IPostsRepository
    {

        private readonly AmazonDynamoDBClient _client;
        private readonly DynamoDBContext _context;

        public PostsRepository()
        {
            _client = new AmazonDynamoDBClient(RegionEndpoint.EUWest1);
            _context = new DynamoDBContext(_client);
        }
        public async Task Add(PostInputModel entity)
        {
            var post = new UserPosts
            {
                userId = entity.UserId,
                text = entity.Text,
                creationTime = DateTime.Now,

            };

            await _context.SaveAsync(post);
        }

        public async Task<IEnumerable<UserPosts>> All(Guid postId)
        {
            return await _context.QueryAsync<UserPosts>(postId).GetRemainingAsync();
        }

        public async Task Remove(PostInputModel entity)
        {
            var post = new UserPosts
            {
                userId=entity.UserId,
                creationTime = entity.CreationTime,
                text = entity.Text
            };
            var _follower = await _context.LoadAsync<UserPosts>(post);
            await _context.DeleteAsync(_follower);
        }

        public async Task<UserPosts> Single(Guid postId)
        {
            return await _context.LoadAsync<UserPosts>(postId); //.QueryAsync<Follower>(FollowerId.ToString()).GetRemainingAsync();
        }



    }
}
