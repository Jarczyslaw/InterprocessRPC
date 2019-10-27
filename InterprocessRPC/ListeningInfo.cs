using System;

namespace InterprocessRPC
{
    public class ListeningInfo
    {
        public DateTime StartTime { get; internal set; }
        public DateTime EndTime { get; internal set; }
        public Exception Exception { get; internal set; }
        public TimeSpan Duration => EndTime - StartTime;
        public string PipeName { get; internal set; }
    }
}