namespace Novalab.Master.Core.Results
{
    public class CreatedResult<T>
    {
        public string Id { get; set; }

        public T Data { get; set; }

        public string EntityPath { get; set; }
    }
}
