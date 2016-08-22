using GigHub.Core;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.AccountController.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var gig = _unitOfWork.Gigs.GetGigWithAttendees(id);
            var userid = User.Identity.GetUserId();

            if (gig == null || gig.IsCanceled)
            {
                return NotFound();
            }

            if (gig.ArtistId != userid)
            {
                return Unauthorized();
            }

            _unitOfWork.Gigs.Remove(gig);
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
