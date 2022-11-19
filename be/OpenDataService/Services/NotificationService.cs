using OpenDataService.Enums;
using OpenDataService.Interfaces;
using OpenDataService.Models;

namespace OpenDataService.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IFireBaseNotificationService _firebaseService;
        private readonly ISlackNotificationService _slackNotificationService;
        private readonly NotificationProvider _notificationProvider;

        public NotificationService(
            IFireBaseNotificationService firebaseService,
            ISlackNotificationService slackNotificationService,
            IConfiguration configuration)
        {
            _firebaseService = firebaseService;
            _slackNotificationService = slackNotificationService;
            _notificationProvider = (NotificationProvider)configuration.GetValue(typeof(NotificationProvider), "NotificationProviderId");

        }

        public async Task<ServiceResult> SendNotificationAsync(NotificationData notification)
        {
            switch (_notificationProvider)
            {
                case NotificationProvider.Firebase:
                    return await _firebaseService.SendNotificationAsync(notification);
                case NotificationProvider.Slack:
                    return await _slackNotificationService.SendNotificationAsync(notification);
                default:
                    return new ServiceResult(false, "Provider implementation not yet implemented");
            }
        }
    }
}
