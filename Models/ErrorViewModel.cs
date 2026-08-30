namespace MB_2.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class Commonresponse
    {
        public bool Status { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
