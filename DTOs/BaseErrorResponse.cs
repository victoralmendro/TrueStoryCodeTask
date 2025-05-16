namespace TrueStoryCodeTask.DTOs
{
    public class BaseErrorResponse
    {
        public string Code { get; set; }
        public string? Message { get; set; }
        public string TraceId { get; set; }

        public Dictionary<string, IEnumerable<string>>? Errors { get; set; }
    }
}
