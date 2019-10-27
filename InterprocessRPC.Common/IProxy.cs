using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace InterprocessRPC.Common
{
    [ServiceContract]
    public interface IProxy
    {
        [OperationContract]
        Task<bool> CheckConnection();

        [OperationContract]
        Task<string> GetHelloMessage(string name);

        [OperationContract]
        Task<DateTime> GetServerTime();
    }
}