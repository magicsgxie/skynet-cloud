

using Microsoft.AspNetCore.Hosting;
using System;
using System.Threading;

namespace UWay.Skynet.Cloud.Nom.XUnitTest
{
    public class TestApplicationLifetime : IApplicationLifetime
    {
        public CancellationToken ApplicationStarted => throw new NotImplementedException();

        public CancellationToken ApplicationStopping => new CancellationTokenSource().Token;

        public CancellationToken ApplicationStopped => throw new NotImplementedException();

        public void StopApplication()
        {
            throw new NotImplementedException();
        }
    }
}
