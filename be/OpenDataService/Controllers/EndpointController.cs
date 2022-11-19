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
        public async Task<IActionResult> AddReportIncidentAsync(IncidentReport report)
        {
            _incidentRepository.AddReport(report);
            var result = await GenerateAndSendNotificationAsync(report);
            if (result.IsSuccess)
            {
                return Ok("Notifications sent");
            }
            return Problem("Unable to sent notification.Location could not be detected or notification could not be sent");
        }

        [Route("maintenace")]
        [HttpPost]
        public async Task<IActionResult> AddMaintenanceAsync(MaintenaceReport report)
        {
            _maintenaceRepository.AddReport(report);
            var result = await GenerateAndSendNotificationAsync(report);
            if (result.IsSuccess)
            {
                return Ok("Notifications sent");
            }
            return Problem("Unable to sent notification.Location could not be detected or notification could not be sent");
        }



        #region Edit

        [Route("incident/{id}")]
        [HttpPut]
        public async Task<IActionResult> EditIncidentReportAsync(int reportId, bool resolved)
        {
            var report = _incidentRepository.GetReport(reportId);
            if (report != null)
            {
                report.IsResolved = resolved;
                report.ResolveTime = DateTime.Now;
                var result = await GenerateAndSendNotificationAsync(report, true);

                if (result.IsSuccess)
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
        public async Task<IActionResult> EditMaintenanceAsync(int reportId, bool resolved, DateTime resolutionTime)
        {
            var report = _maintenaceRepository.GetReport(reportId);
            if (report != null)
            {
                report.IsResolved = resolved;
                report.DateTimeEnd = resolutionTime;
                var result = await GenerateAndSendNotificationAsync(report, true);

                if (result.IsSuccess)
                {
                    return Ok("Notifications sent");
                }
                return Problem("Unable to sent notification.Location could not be detected or notification could not be sent");
            }

            _logger.LogError($"Report {reportId} is not found");
            return BadRequest("ReportId is not valid");
        }
        #endregion

        private async Task<ServiceResult> GenerateAndSendNotificationAsync(IncidentReport report, bool isUpdate = false)
        {
            var location = _areaMappingService.MapReportToArea(report);
            if (location == null)
            {
                _logger.LogError($"Location for {report.Id} is not found");
                return new ServiceResult(false);
            }
            var notificationData = NotificationCreatorHelper.GetNotification(report, location, isUpdate);
            return await _notificationService.SendNotificationAsync(notificationData);
        }

        private async Task<ServiceResult> GenerateAndSendNotificationAsync(MaintenaceReport report, bool isUpdate = false)
        {
            var location = _areaMappingService.MapReportToArea(report);
            if (location == null)
            {
                _logger.LogError($"Location for {report.Id} is not found");
                return new ServiceResult(false);
            }
            var notificationData = NotificationCreatorHelper.GetNotification(report, location, isUpdate);
            return await _notificationService.SendNotificationAsync(notificationData);
        }
    }
}
