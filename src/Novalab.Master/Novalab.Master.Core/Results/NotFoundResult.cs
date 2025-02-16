namespace Novalab.Master.Core.Results
{
    public class NotFoundResult
    {
        public NotFoundResult()
        {

        }

        public NotFoundResult(string entityType, string id)
        {
            EntityType = entityType;
            Id = id;
        }

        public string EntityType { get; set; }
        public string Id { get; set; }
    }
}
