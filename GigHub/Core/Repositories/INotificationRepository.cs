using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNewNotifications(string userid);
    }
}