using OpenDataService.Models;

namespace OpenDataService.Services
{
    public static class NotificationCreatorHelper
    {
        public static NotificationData GetNotification(IncidentReport report, Location location, bool isUpdate)
        {
            var notification = new NotificationData();
            var updateText = isUpdate ? "Update:" : "New:";
            notification.Title = $"{updateText} Incident near you";

            var resolutionText = report.IsResolved ? " Issue is now resolved" : $"Estimated time of resolution {report.ResolveTime}";
            var streetText = "Incident affects " +
               (location.StreetNames.Any() ? string.Join(", ", location.StreetNames) : location.Area);
            notification.Message = $"{report.NetworkType}, {report.Type}, {location.Area}, {resolutionText}. {streetText}";

            return notification;
        }

        public static NotificationData GetNotification(MaintenaceReport report, Location location, bool isUpdate)
        {
            var notification = new NotificationData();
            var updateText = isUpdate ? "Update:" : "New:";
            notification.Title = $"{updateText} Maintenace near you";
            var resolutionText = report.IsResolved ? " Maintenance is now ended" : $"Estimated time of resolution {report.DateTimeEnd}";
            var streetText = "Maintenace affects " +
             (location.StreetNames.Any() ? string.Join(", ", location.StreetNames) : location.Area);
            notification.Message = $"{report.NetworkType}, {report.Type}, {location.Area}, {resolutionText}. {streetText}";

            return notification;
        }
    }
}
