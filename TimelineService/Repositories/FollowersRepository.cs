using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using TimelineService.Interfaces;
using TimelineService.Model;
using Amazon;

namespace TimelineService.Repositories
{
    public class FollowersRepository : IFollowersRepository
    {

        private readonly AmazonDynamoDBClient _client;
        private readonly DynamoDBContext _context;

        public FollowersRepository()
        {


            _client = new AmazonDynamoDBClient(RegionEndpoint.EUWest1);
            _context = new DynamoDBContext(_client);
        }

        public async Task Add(FollowerInputModel entity)
        {
            var follower = new UsersFollowers
            {
                userId = entity.UserId,
                followerId = entity.FollowerId,
            };

            await _context.SaveAsync(follower);
        }

        public async Task<IEnumerable<UsersFollowers>> All(Guid userId)
        {
            return await _context.QueryAsync<UsersFollowers>(userId).GetRemainingAsync();
        }

        public async Task Remove(FollowerInputModel entity)
        {
            var follower = new UsersFollowers
            {
                userId = entity.UserId,
                followerId = entity.FollowerId,
            };
            var _follower = await _context.LoadAsync<UsersFollowers>(follower);
            await _context.DeleteAsync(_follower);
        }

        public async Task<UsersFollowers> Single(Guid followerId)
        {
            return await _context.LoadAsync<UsersFollowers>(followerId); //.QueryAsync<Follower>(FollowerId.ToString()).GetRemainingAsync();
        }
    }
}
