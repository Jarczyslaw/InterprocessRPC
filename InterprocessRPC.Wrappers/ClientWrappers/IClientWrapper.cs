﻿using System;
using System.Threading.Tasks;

namespace InterprocessRPC.Wrappers.ClientWrappers
{
    public interface IClientWrapper
    {
        Task Start();

        Task Stop();

        Task<bool> CheckConnection();

        Task<string> GetHelloMessage(string name);

        Task<DateTime> GetServerTime();
    }
}