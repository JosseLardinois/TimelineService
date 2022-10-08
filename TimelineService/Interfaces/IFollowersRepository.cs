using TimelineService.Model;

namespace TimelineService.Interfaces
{
    public interface IFollowersRepository
    {
        Task<IEnumerable<UsersFollowers>> All(Guid userId);
        Task Add(FollowerInputModel entity);
        Task Remove(FollowerInputModel follower);
    }
}