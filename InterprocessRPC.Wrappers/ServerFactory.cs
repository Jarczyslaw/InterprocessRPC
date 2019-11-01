using InterprocessRPC.Wrappers.ServerWrappers;

namespace InterprocessRPC.Wrappers
{
    public static class ServerFactory
    {
        public static IServerWrapper GetServerWrapper()
        {
#if STREAMJSON
            return new StreamJsonServerWrapper();
#elif WCF
            return new WCFServerWrapper();
#elif IPCFRAMEWORK
            return new IpcFrameworkServerWrapper();
#elif ENETER
            return new EneterServerWrapper();
#elif GRPC
            return new gRPCServerWrapper();
#else
            return null;
#endif
        }
    }
}