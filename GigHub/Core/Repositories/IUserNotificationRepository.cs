namespace GigHub.Core.Repositories
{
    public interface IUserNotificationRepository
    {
        void ChangeNotificationsAsRead(string userid);
    }
}