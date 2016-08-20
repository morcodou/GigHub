using GigHub.Core.Models;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;
        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IEnumerable<Notification> GetNewNotifications()
        {
            var userid = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userid && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            return notifications;

            ////var config = new MapperConfiguration(cfg =>
            ////{
            ////    cfg.CreateMap<ApplicationUser, UserDto>();
            ////    cfg.CreateMap<Gig, GigDto>();
            ////    cfg.CreateMap<Notification, NotificationDto>();
            ////});
            ////var mapper = config.CreateMapper();

            ////try
            ////{
            ////    var selected=  notifications.Select(mapper.Map<Notification, NotificationDto>);
            ////}
            ////catch (System.Exception ex)
            ////{

            ////    throw ex;
            ////}

            ////return notifications.Select(mapper.Map<Notification, NotificationDto>);
        }


        [HttpPost]
        public IHttpActionResult NotificationsAsRead()
        {
            var userid = User.Identity.GetUserId();

            var notifications = _context.UserNotifications.Where(un => un.UserId == userid && !un.IsRead).ToList();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            _context.SaveChanges();
            return Ok();
        }

    }
}
