using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace InterprocessRPC.gRPC
{
    public class ProxyServiceImpl : ProxyService.ProxyServiceBase
    {
        public override Task<CheckConnectionReply> CheckConnection(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new CheckConnectionReply
            {
                Result = true
            });
        }

        public override Task<GetHelloMessageReply> GetHelloMessage(GetHelloMessageRequest request, ServerCallContext context)
        {
            return Task.FromResult(new GetHelloMessageReply
            {
                Message = $"Hello {request.Name}!"
            });
        }

        public override Task<GetServerTimeReply> GetServerTime(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new GetServerTimeReply
            {
                Time = new Timestamp
                {
                    Seconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                }
            });
        }
    }
}