namespace InterprocessRPC.WCF
{
    public class AddressProvider : IAddressProvider
    {
        public string ApplicationName { get; set; }
        public string ServiceName { get; set; }

        public string BaseAddress => $"net.pipe://localhost/{ApplicationName}";
        public string ServiceAddress => $"{BaseAddress}/{ServiceName}";
    }
}