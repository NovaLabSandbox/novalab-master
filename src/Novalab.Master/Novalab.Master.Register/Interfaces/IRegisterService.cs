using Novalab.Master.Register.Contracts.Request;

namespace Novalab.Master.Register.Interfaces
{
    public interface IRegisterService
    {
        Task<bool> RegisterAsync(RegisterRequest registerRequest);
    }
}
