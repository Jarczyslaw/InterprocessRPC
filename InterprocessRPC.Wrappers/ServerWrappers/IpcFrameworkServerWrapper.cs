using InterprocessRPC.Common;
using JKang.IpcServiceFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace InterprocessRPC.Wrappers.ServerWrappers
{
    public class IpcFrameworkServerWrapper : IServerWrapper
    {
        public Action ListeningStart { get; set; }
        public Action ListeningStop { get; set; }
        public Action ClientConnected { get; set; }
        public Action ClientDisconnected { get; set; }

        private readonly IServiceCollection services;

        public IpcFrameworkServerWrapper()
        {
            services = ConfigureServices(new ServiceCollection());
        }

        public async Task Start()
        {
            var host = new IpcServiceHostBuilder(services.BuildServiceProvider())
                .AddNamedPipeEndpoint<IProxy>("ipcFrameworkEndpoint", "ipcFrameworkPipeName")
                .Build();
            await host.RunAsync();
        }

        public Task Stop()
        {
            return Task.CompletedTask;
        }

        private static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            return services.AddIpc(builder =>
            {
                builder.AddNamedPipe(options => options.ThreadCount = 10)
                    .AddService<IProxy, Proxy>();
            });
        }
    }
}