namespace InterprocessRPC.WCF
{
    public interface IAddressProvider
    {
        string BaseAddress { get; }
        string ServiceName { get; }
        string ServiceAddress { get; }
    }
}