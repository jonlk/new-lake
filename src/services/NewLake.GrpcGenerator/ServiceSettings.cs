namespace NewLake.GrpcGenerator
{
    public class ServiceSettings
    {
        public int DelayInterval { get; set; }
        public int MessageId { get; set; }
        public string? ServerUrl { get; set; }
        public int RetryCount { get; set; }
        public int RetryInterval { get; set; }
    }
}
