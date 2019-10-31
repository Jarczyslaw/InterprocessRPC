using System;

namespace InterprocessRPC.Common
{
    public interface IEneterProxy
    {
        bool CheckConnection();

        string GetHelloMessage(string name);

        DateTime GetServerTime();
    }
}