using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.AccountController.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;
        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userid = User.Identity.GetUserId();
            var gig = _context
                .Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == id && g.ArtistId == userid);

            if (gig.IsCanceled)
            {
                return NotFound();
            }

            gig.Cancel();

            _context.SaveChanges();
            return Ok();
        }

        ////[HttpPut]
        ////public IHttpActionResult Update(Gig gig)
        ////{
        ////    var userid = User.Identity.GetUserId();
        ////    var originalgig = _context
        ////        .Gigs
        ////        .Include(g => g.Attendances.Select(a => a.Attendee))
        ////        .Single(g => g.Id == gig.Id && g.ArtistId == userid);

        ////    if (gig.IsCanceled)
        ////    {
        ////        return NotFound();
        ////    }

        ////    gig.Update(originalgig);

        ////    _context.SaveChanges();
        ////    return Ok();
    ////}
}


}
