using OpenDataService.Models;

namespace OpenDataService.Interfaces
{
    public interface IAreaMappingService
    {
        Location? MapReportToArea(Report report);
    }
}
