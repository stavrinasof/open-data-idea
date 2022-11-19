using OpenDataService.Models;

namespace OpenDataService.Interfaces
{
    public interface INotificationService
    {
        void SendNotification(NotificationData notification);
    }
}
