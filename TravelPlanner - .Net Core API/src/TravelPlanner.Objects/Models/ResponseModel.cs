namespace TravelPlanner.Objects.Models
{
    public class ResponseModel
    {
        public bool IsError { get; set; }
        public string Message { get; set; }

        public ResponseModel()
        {
            IsError = false;
            Message = string.Empty;
        }
    }
}
