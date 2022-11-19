using OpenDataService.Models;

namespace OpenDataService.Interfaces
{
    public interface INotificationService
    {
        Task<ServiceResult> SendNotificationAsync(NotificationData notification);
    }

    public interface IFireBaseNotificationService 
    {
        Task<ServiceResult> SendNotificationAsync(NotificationData notification);
    }

    public interface ISlackNotificationService 
    {
        Task<ServiceResult> SendNotificationAsync(NotificationData notification);
    }
}
