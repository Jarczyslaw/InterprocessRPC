using System;

namespace InterprocessRPC.Common
{
    public class EneterProxy : IEneterProxy
    {
        public bool CheckConnection()
        {
            return true;
        }

        public string GetHelloMessage(string name)
        {
            return $"Hello {name}!";
        }

        public DateTime GetServerTime()
        {
            return DateTime.Now;
        }
    }
}