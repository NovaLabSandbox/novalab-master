using MongoDB.Bson.Serialization.Attributes;

namespace Novalab.Master.Domain.Entities
{
    public class Service
    {
        [BsonId]
        public string Id { get; set; } = string.Empty;

        [BsonRequired]
        public string Name { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public bool IsEnabled { get; set; } = false;

        [BsonRequired]
        public string Uri { get; set; } = string.Empty;

        [BsonRequired]
        public string Version { get; set; } = string.Empty;

        [BsonRequired]
        public DateTime InstallAt { get; set; } = DateTime.UtcNow;

        [BsonRequired]
        public DateTime LoadAt { get; set; } = DateTime.UtcNow;

        [BsonRequired]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}
