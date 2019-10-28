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
#else
            return null;
#endif
        }
    }
}