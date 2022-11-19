using Microsoft.AspNetCore.Mvc;
using OpenDataService.Interfaces;
using OpenDataService.Models;

namespace OpenDataService.Controllers
{
    
    public class EndpointController : Controller
    {
        private readonly ILogger<EndpointController> _logger;
        private readonly IAreaMappingService _areaMappingService;
        public EndpointController(ILogger<EndpointController> logger,
                                  IAreaMappingService areaMappingService)
        {
            _logger = logger;
            _areaMappingService = areaMappingService;
        }

        [Route("incident")]
        [HttpPost]
        public IActionResult AddReportIncident(Report report)
        {
            _areaMappingService.MapReportToArea(report);

            return Ok("Notifications sent");
        }

        [Route("incident/{id}")]
        [HttpPut]
        public IActionResult EditReportIncident(int reportId, Report report)
        {
            _areaMappingService.MapReportToArea(report);

            return Ok("Notifications sent");
        }

        [Route("maintenace")]
        [HttpPost]
        public IActionResult AddMaintenance(MaintenaceReport report)
        {
            _areaMappingService.MapReportToArea(report);

            return Ok("Notifications sent");
        }

        [Route("maintenace/{id}")]
        [HttpPut]
        public IActionResult AddMaintenance(int reportId, MaintenaceReport report)
        {
            _areaMappingService.MapReportToArea(report);

            return Ok("Notifications sent");
        }

    }
}
