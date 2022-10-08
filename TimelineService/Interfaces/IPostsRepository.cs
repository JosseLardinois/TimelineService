using PostService.Models;
using TimelineService.Model;

namespace PostService.Interfaces
{
    public interface IPostsRepository
    {
        Task<IEnumerable<UserPosts>> All(Guid userId);
        Task Add(PostInputModel entity);
        Task Remove(PostInputModel posts);
    }
}
