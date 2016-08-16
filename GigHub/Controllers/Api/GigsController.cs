using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
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
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userid);

            if (gig.IsCanceled)
            {
                return NotFound();
            }

            gig.IsCanceled = true;

            var notification = new Notification()
            {
                DateTime = DateTime.Now,
                Gig = gig,
                Type = NotificationType.GigCanceled,
            };

            var attendees = _context
                .Attendances
                .Where(a => a.GigId == gig.Id)
                .Select(a => a.Attendee)
                .ToList();

            foreach (var attendee in attendees)
            {
                var usernotification = new UserNotification()
                {
                    User = attendee,
                    Notification = notification
                };

                _context.UserNotifications.Add(usernotification);
            }

            _context.SaveChanges();
            return Ok();
        }
    }
}
