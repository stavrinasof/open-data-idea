using CorePush.Google;
using Microsoft.Extensions.Options;
using OpenDataService.Interfaces;
using OpenDataService.Models;
using System.Net.Http.Headers;

namespace OpenDataService.Services
{
    public class FirebaseNotificationService : IFireBaseNotificationService
    {
        private readonly FcmSettings _settings;
        private readonly ILogger<FirebaseNotificationService> _logger;
        public FirebaseNotificationService(IOptions<FirebaseSettings> options, ILogger<FirebaseNotificationService> logger)
        {
            _settings = new FcmSettings()
            {
                SenderId = options.Value.SenderId,
                ServerKey = options.Value.SenderKey

            };
            _logger = logger;
        }

        public async Task<ServiceResult> SendNotificationAsync(NotificationData notification)
        {
            try
            {
                if (notificationModel.IsAndroiodDevice)
                {

                    HttpClient httpClient = new HttpClient();

                    string authorizationKey = string.Format("keyy={0}", _settings.ServerKey);
                    string deviceToken = notificationModel.DeviceId;

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                    httpClient.DefaultRequestHeaders.Accept
                            .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var fcm = new FcmSender(_settings, httpClient);
                    var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

                    if (fcmSendResponse.IsSuccess())
                    {
                        return new ServiceResult(true, "Notification sent successfully for Android");
                    }
                    else
                    {
                        return new ServiceResult(false, fcmSendResponse.Results[0].Error);
                    }
                }
                else
                {
                    /* Code here for APN Sender (iOS Device) */
                    //var apn = new ApnSender(apnSettings, httpClient);
                    //await apn.SendAsync(notification, deviceToken);
                    return new ServiceResult(false, "Not yet implemented for Ios");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to send Notification");
                return new ServiceResult(false, ex.Message);
            }
        }
    }
}
