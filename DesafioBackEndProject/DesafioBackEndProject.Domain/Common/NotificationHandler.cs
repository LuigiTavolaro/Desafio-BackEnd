using System.Diagnostics.CodeAnalysis;

namespace DesafioBackEndProject.Domain.Common
{
    [ExcludeFromCodeCoverage]
    public class NotificationHandler
    {
        private readonly List<Notification> _notifications = new List<Notification>();

        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }

        public IEnumerable<Notification> GetNotifications()
        {
            return _notifications;
        }

        public bool HasNotifications()
        {
            return _notifications.Any();
        }
    }
}
