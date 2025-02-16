
using System.Net.Http.Json;

using Novalab.Master.Register.Contracts.Request;
using Novalab.Master.Register.Interfaces;

namespace Novalab.Master.Register.Services
{
    public class RegisterService(HttpClient _httpClient) : IRegisterService
    {
        public async Task<bool> RegisterAsync(RegisterRequest registerRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("plugins/register", registerRequest);
            return response.IsSuccessStatusCode;
        }
    }
}
