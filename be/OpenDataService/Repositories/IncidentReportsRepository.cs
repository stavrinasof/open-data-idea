using OpenDataService.Interfaces;
using OpenDataService.Models;

namespace OpenDataService.Repositories
{
    public class IncidentReportsRepository : IReportRepository<IncidentReport>
    {
        private Dictionary<int, IncidentReport> reports = new Dictionary<int, IncidentReport>();
        public void AddReport(IncidentReport report)
        {
            if (reports.ContainsKey(report.Id))
                reports[report.Id] = report;
            else
                reports.Add(report.Id, report);
        }

        public IncidentReport? GetReport(int id)
        {
            if (reports.ContainsKey(id))
                return reports[id];
            return null;
        }
    }
}
