namespace InterviewExpoApplication.Shared.CommonModels
{
    public class BadRequestDto
    {
        public Dictionary<string, List<string>> Errors { get; set; }
        public int Status { get; set; }
        public string Title { get; set; }
        public string TraceId { get; set; }
        public string Type { get; set; }
    }
}
