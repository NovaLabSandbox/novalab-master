namespace Novalab.Master.Core.Results
{
    public class ForbiddenResult
    {
        public ForbiddenResult()
        {

        }

        public ForbiddenResult(string entityType, Guid id)
        {
            EntityType = entityType;
            Id = id;
        }

        public string EntityType { get; set; }
        public Guid Id { get; set; }
    }
}
