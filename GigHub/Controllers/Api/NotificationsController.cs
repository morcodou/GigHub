using GigHub.Core;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<Notification> GetNewNotifications()
        {
            return _unitOfWork.Notifications.GetNewNotifications(User.Identity.GetUserId());


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
            _unitOfWork.UserNotifications.ChangeNotificationsAsRead(User.Identity.GetUserId());
            _unitOfWork.Complete();
            return Ok();
        }

    }
}
