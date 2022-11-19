namespace OpenDataService.Models
{
    public class ServiceResult
    {
        public bool IsSuccess { get;private set; }
        public string Message { get; private set; }

        public ServiceResult(bool success, string message = "")
        {
            IsSuccess = success;
            Message = message;
        }
    }
}
