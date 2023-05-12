using Newtonsoft.Json;

namespace Models
{
    public interface IMessage
    {
        Guid Id { get; }
    }
    public class Event : IMessage
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        public int Version { get; set; }

        public string? Type { get; set; }

        public Guid AggregateId { get; set; }

        public string? AggregateType { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string? CorrelationId { get; set; }

        public string? CausationId { get; set; }

        public string? AuthenticatedAppId { get; set; }

        public string? AuthenticatedUserName { get; set; }

        public string? Source { get; set; }

        public string? DataVersion { get; set; }

        public object? Data { get; set; }

        public Event()
        {
        }

        public Event(
            Guid aggregateId,
            string source,
            string dataVersion,
            string causationId,
            string? correlationId)
        {
            this.Id = Guid.NewGuid();
            this.Type = this.GetType().Name;
            this.AggregateId = aggregateId;
            this.CreatedAt = DateTimeOffset.UtcNow;
            this.CorrelationId = correlationId;
            this.CausationId = causationId;
            this.Source = source;
            this.DataVersion = dataVersion;
        }
    }
}
