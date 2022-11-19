using OpenDataService.Interfaces;
using OpenDataService.Models;

namespace OpenDataService.Repositories
{
    public class MaintenanceReportsRepository : IReportRepository<MaintenaceReport>
    {
        private Dictionary<int, MaintenaceReport> reports = new Dictionary<int, MaintenaceReport>();
        public void AddReport(MaintenaceReport report)
        {
            if (reports.ContainsKey(report.Id))
                reports[report.Id] = report;
            else
                reports.Add(report.Id, report);
        }

        public MaintenaceReport? GetReport(int id)
        {
            if (reports.ContainsKey(id))
                return reports[id];
            return null;
        }
    }
}
