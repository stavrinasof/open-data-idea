using OpenDataService.Enums;

namespace OpenDataService.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public int Id { get; set; }  //o metritis p tha dwsei to report gia na ginei to mapping
        public bool IsSuccessful { get; set; }
        public IncidentType Type { get; set; }
        public NetworkType NetworkType { get; set; }
        public bool IsResolved { get; set; }
    }
}
