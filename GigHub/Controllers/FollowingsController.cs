using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;


namespace GigHub.Controllers
{

    [Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userid = User.Identity.GetUserId();

            if (_context.Followings.Any(f => f.FollowerId == userid && f.FolloweeId == dto.FolloweeId))
            {
                return BadRequest("Following already exists.");
            }

            var following = new Following()
            {
                FollowerId = userid,
                FolloweeId = dto.FolloweeId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }
    }
}
