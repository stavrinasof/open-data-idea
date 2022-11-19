using Microsoft.AspNetCore.Mvc;
using OpenDataService.Interfaces;
using OpenDataService.Models;
using OpenDataService.Services;

namespace OpenDataService.Controllers
{

    public class EndpointController : Controller
    {
        private readonly ILogger<EndpointController> _logger;
        private readonly IAreaMappingService _areaMappingService;
        private readonly IReportRepository<IncidentReport> _incidentRepository;
        private readonly IReportRepository<MaintenaceReport> _maintenaceRepository;
        private readonly INotificationService _notificationService;

        public EndpointController(ILogger<EndpointController> logger,
                                  IAreaMappingService areaMappingService,
                                  IReportRepository<IncidentReport> incidentRepository,
                                  IReportRepository<MaintenaceReport> maintenaceRepository,
                                  INotificationService notificationService)
        {
            _logger = logger;
            _areaMappingService = areaMappingService;
            _incidentRepository = incidentRepository;
            _maintenaceRepository = maintenaceRepository;
            _notificationService = notificationService;
        }

        [Route("incident")]
        [HttpPost]
        public IActionResult AddReportIncident(IncidentReport report)
        {
            _incidentRepository.AddReport(report);
            var success = GenerateAndSendNotification(report);
            if (success)
            {
                return Ok("Notifications sent");
            }
            return Problem("Unable to sent notification.Location could not be detected or notification could not be sent");
        }

        [Route("maintenace")]
        [HttpPost]
        public IActionResult AddMaintenance(MaintenaceReport report)
        {
            _maintenaceRepository.AddReport(report);
            var success = GenerateAndSendNotification(report);
            if (success)
            {
                return Ok("Notifications sent");
            }
            return Problem("Unable to sent notification.Location could not be detected or notification could not be sent");
        }



        #region Edit

        [Route("incident/{id}")]
        [HttpPut]
        public IActionResult EditIncidentReport(int reportId, bool resolved)
        {
            var report = _incidentRepository.GetReport(reportId);
            if (report != null)
            {
                report.IsResolved = resolved;
                report.ResolveTime = DateTime.Now;
                var success = GenerateAndSendNotification(report, true);

                if (success)
                {
                    return Ok("Notifications sent");
                }
                return Problem("Unable to sent notification.Location could not be detected or notification could not be sent");
            }

            _logger.LogError($"Report {reportId} is not found");
            return BadRequest("ReportId is not valid");
        }



        [Route("maintenace/{id}")]
        [HttpPut]
        public IActionResult EditMaintenance(int reportId, bool resolved, DateTime resolutionTime)
        {
            var report = _maintenaceRepository.GetReport(reportId);
            if (report != null)
            {
                report.IsResolved = resolved;
                report.DateTimeEnd = resolutionTime;
                var success = GenerateAndSendNotification(report, true);

                if (success)
                {
                    return Ok("Notifications sent");
                }
                return Problem("Unable to sent notification.Location could not be detected or notification could not be sent");
            }

            _logger.LogError($"Report {reportId} is not found");
            return BadRequest("ReportId is not valid");
        }
        #endregion

        private bool GenerateAndSendNotification(IncidentReport report, bool isUpdate = false)
        {
            var location = _areaMappingService.MapReportToArea(report);
            if (location == null)
            {
                _logger.LogError($"Location for {report.Id} is not found");
                return false;
            }
            var notificationData = NotificationCreatorHelper.GetNotification(report, location, isUpdate);
            _notificationService.SendNotification(notificationData);
            return true;
        }

        private bool GenerateAndSendNotification(MaintenaceReport report, bool isUpdate = false)
        {
            var location = _areaMappingService.MapReportToArea(report);
            if (location == null)
            {
                _logger.LogError($"Location for {report.Id} is not found");
                return false;
            }
            var notificationData = NotificationCreatorHelper.GetNotification(report, location, isUpdate);
            _notificationService.SendNotification(notificationData);
            return true;
        }
    }
}
