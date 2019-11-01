using InterprocessRPC.Wrappers.ClientWrappers;

namespace InterprocessRPC.Wrappers
{
    public static class ClientFactory
    {
        public static IClientWrapper GetClientWrapper()
        {
#if STREAMJSON
            return new StreamJsonClientWrapper();
#elif WCF
            return new WCFClientWrapper();
#elif IPCFRAMEWORK
            return new IpcFrameworkClientWrapper();
#elif ENETER
            return new EneterClientWrapper();
#elif GRPC
            return new gRPCClientWrapper();
#else
            return null;
#endif
        }
    }
}