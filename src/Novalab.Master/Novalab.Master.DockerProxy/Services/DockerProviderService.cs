using Docker.DotNet;

using Novalab.Master.DockerProxy.Interfaces;

namespace Novalab.Master.DockerProxy.Services
{
    public class DockerProviderService : IPluginProviderService
    {
        public async Task<bool> InstallAndStartPluginAsync(string name, string imageName, IList<string> env = null)
        {
            var client = new DockerClientConfiguration().CreateClient();
            var containerConfiguration = new Docker.DotNet.Models.CreateContainerParameters()
            {
                Image = imageName,
                Name = name,
                Env = env
            };

            var createdContainer = await client.Containers.CreateContainerAsync(containerConfiguration);

            if (createdContainer != null)
            {
                var startedContainer = await client.Containers.StartContainerAsync(createdContainer.ID, new Docker.DotNet.Models.ContainerStartParameters());
                return startedContainer;
            }

            return false;
        }
    }
}
