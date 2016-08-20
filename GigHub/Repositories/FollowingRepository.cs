using GigHub.Models;
using System.Linq;

namespace GigHub.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;
        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string userid, string artistId)
        {
            return _context
                .Followings
                .SingleOrDefault(x => x.FolloweeId == artistId && x.FollowerId == userid);
        }
    }
}