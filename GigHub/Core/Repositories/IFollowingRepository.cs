using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string userid, string artistId);

        void Add(Following following);
        void Remove(Following following);

    }
}