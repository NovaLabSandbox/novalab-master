namespace Novalab.Master.Core.Results
{
    public class BusinessErrorResult
    {
        public BusinessErrorResult()
        {

        }
        public BusinessErrorResult(string error)
        {
            Error = error;
        }

        public string Error { get; set; }
    }
}
