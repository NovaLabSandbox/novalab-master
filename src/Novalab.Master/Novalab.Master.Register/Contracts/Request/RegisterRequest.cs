using System.Text.Json.Serialization;

namespace Novalab.Master.Register.Contracts.Request
{
    public class RegisterRequest
    {
        [JsonPropertyName("serviceName")]
        public string ServiceName { get; set; }
    }
}
