using OpenDataService.Interfaces;
using OpenDataService.Models;

namespace OpenDataService.Services
{
    public class SlackNotificationService : ISlackNotificationService
    {
        public async Task<ServiceResult> SendNotificationAsync(NotificationData notification)
        {
            throw new NotImplementedException();
        }
    }
}
