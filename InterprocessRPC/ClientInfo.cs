using System;

namespace InterprocessRPC
{
    public class ClientInfo
    {
        public DateTime ConnectedTime { get; set; }
        public DateTime DisconnectedTime { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid Guid { get; set; }
        public Exception Exception { get; set; }
    }
}