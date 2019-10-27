using InterprocessRPC.WCF;

namespace InterprocessRPC.Wrappers
{
    public class WCFAddressProvider : AddressProvider
    {
        public WCFAddressProvider()
        {
            ApplicationName = "Interprocess";
            ServiceName = "Service";
        }
    }
}