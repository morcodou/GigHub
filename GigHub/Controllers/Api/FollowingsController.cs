using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;


namespace GigHub.Controllers.Api
{

    [Authorize]
    public class FollowingsController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public FollowingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userid = User.Identity.GetUserId();

            if (_unitOfWork.Followings.GetFollowing(userid, dto.FolloweeId) != null)
            {
                return BadRequest("Following already exists.");
            }

            var following = new Following()
            {
                FollowerId = userid,
                FolloweeId = dto.FolloweeId
            };

            _unitOfWork.Followings.Add(following);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult UnFollow(string id)
        {
            var userid = User.Identity.GetUserId();
            var following = _unitOfWork.Followings.GetFollowing(userid, id);

            if (following == null)
                return NotFound();

            _unitOfWork.Followings.Remove(following);
            _unitOfWork.Complete();

            return Ok(id);
        }

    }
}
