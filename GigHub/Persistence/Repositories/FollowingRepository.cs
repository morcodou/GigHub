using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Models;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;
        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        public Following GetFollowing(string userid, string artistId)
        {
            return _context
                .Followings
                .SingleOrDefault(x => x.FolloweeId == artistId && x.FollowerId == userid);
        }

        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }
    }
}