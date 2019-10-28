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
#else
            return null;
#endif
        }
    }
}