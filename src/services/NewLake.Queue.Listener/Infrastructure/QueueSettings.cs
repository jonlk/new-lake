namespace NewLake.Queue.Listener.Infrastructure
{
    public class QueueSettings
    {
        public string QueueName { get; set; }
        public string HostName { get; set; }
        public string Exchange { get; set; }
        public string Topic { get; set; }
        public string RoutingKey { get; set; }
    }
}
