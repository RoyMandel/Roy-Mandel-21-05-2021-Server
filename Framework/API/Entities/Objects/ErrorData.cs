namespace Framework.API.Entities.Objects
{
    public class ErrorData
    {
        public string Message { get; set; }
        public string Data { get; set; }
        public string StackTrace { get; set; }
        public string ErrorCode { get; set; }
    }
}
