using System;

namespace InterprocessRPC
{
    public class ClientInfo
    {
        public DateTime ConnectedTime { get; internal set; }
        public DateTime DisconnectedTime { get; internal set; }
        public TimeSpan Duration { get; internal set; }
        public Guid Guid { get; internal set; }
        public Exception Exception { get; internal set; }
    }
}