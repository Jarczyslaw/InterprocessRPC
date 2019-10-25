using System;

namespace InterprocessRPC
{
    public class ListeningInfo
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Exception Exception { get; set; }
        public TimeSpan Duration => EndTime - StartTime;
    }
}