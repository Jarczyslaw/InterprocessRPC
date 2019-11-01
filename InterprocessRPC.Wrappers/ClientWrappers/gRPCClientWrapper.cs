using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using InterprocessRPC.gRPC;
using System;
using System.Threading.Tasks;

namespace InterprocessRPC.Wrappers.ClientWrappers
{
    public class gRPCClientWrapper : IClientWrapper
    {
        private Channel channel;
        private ProxyService.ProxyServiceClient client;

        public async Task<bool> CheckConnection()
        {
            var result = await client.CheckConnectionAsync(new Empty());
            return await Task.FromResult(result.Result);
        }

        public async Task<string> GetHelloMessage(string name)
        {
            var result = await client.GetHelloMessageAsync(new GetHelloMessageRequest
            {
                Name = name
            });
            return await Task.FromResult(result.Message);
        }

        public async Task<DateTime> GetServerTime()
        {
            var result = await client.GetServerTimeAsync(new Empty());
            return await Task.FromResult(result.Time.ToDateTime());
        }

        public Task Start()
        {
            channel = new Channel("localhost:1234", ChannelCredentials.Insecure);
            client = new ProxyService.ProxyServiceClient(channel);
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            return channel.ShutdownAsync();
        }
    }
}