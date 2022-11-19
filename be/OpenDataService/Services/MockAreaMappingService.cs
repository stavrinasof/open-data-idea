using OpenDataService.Enums;
using OpenDataService.Interfaces;
using OpenDataService.Models;

namespace OpenDataService.Services
{
    public class MockAreaMappingService : IAreaMappingService
    {
        private readonly Dictionary<(NetworkType, int), Location> _mappings;
        private readonly ILogger<MockAreaMappingService> _logger;

        public MockAreaMappingService(ILogger<MockAreaMappingService> logger)
        {
            _mappings = new Dictionary<(NetworkType, int), Location>()
            {
                {
                    (NetworkType.Eydap, 1),
                    new Location(2345,2123443, "Gazi", new List<string>{"a","b" })
                },
                {
                    (NetworkType.Eydap, 2),
                    new Location(2345,2123443, "Gazi", new List<string>{"a","b" })
                }
            };
            _logger = logger;
        }

        public Location? MapReportToArea(Report report)
        {
            if (_mappings.ContainsKey((report.NetworkType, report.Id)))
            {
                return _mappings[(report.NetworkType, report.Id)];
            }
            _logger.LogWarning($"Unable to map report for {report.NetworkType} ,{report.Id}");
            return null;
        }
    }
}
