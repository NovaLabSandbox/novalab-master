using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Novalab.Master.Register.Interfaces;
using Novalab.Master.Register.Services;

namespace Novalab.Master.Register.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static TBuilder AddMasterRegistration<TBuilder>(this TBuilder builder) where TBuilder
            : IHostApplicationBuilder
        {
            builder.Services.AddHttpClient<IRegisterService, RegisterService>("master", (httpClient) =>
            {
                var baseUri = builder.Configuration["master:uri"] ?? "http://novalab-master";
                httpClient.BaseAddress = new Uri(baseUri);
            });

            return builder;
        }

        public static WebApplication RegisterPlugin(this WebApplication app)
        {
            var registerService = app.Services.GetRequiredService<IRegisterService>();
            var pluginName = app.Configuration["service:name"];
            var registeredResult = registerService.RegisterAsync(new Contracts.Request.RegisterRequest()
            {
                ServiceName = pluginName
            }).GetAwaiter().GetResult();

            return app;
        }
    }
}
