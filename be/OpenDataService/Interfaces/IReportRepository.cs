namespace OpenDataService.Interfaces
{
    public interface IReportRepository<T>
    {
        void AddReport(T report);
        T? GetReport(int id);
    }
}
