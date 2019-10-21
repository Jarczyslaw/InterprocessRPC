using System;

namespace InterprocessRPC.Common
{
    public class ServerInfo
    {
        public DateTime ServerTime { get; set; }
        public int ConnectionsCount { get; set; }

        public override string ToString()
        {
            return $"Connections count: {ConnectionsCount}\nServer time: {ServerTime}";
        }
    }
}