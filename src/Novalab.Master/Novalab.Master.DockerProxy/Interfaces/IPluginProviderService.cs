namespace Novalab.Master.DockerProxy.Interfaces
{
    public interface IPluginProviderService
    {
        Task<bool> InstallAndStartPluginAsync(string name, string imageName, IList<string> env = null);
    }
}
