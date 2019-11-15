using System;
using System.Collections.Generic;
using System.Text;
using UWay.Skynet.Cloud.Request;
using UWay.Skynet.Cloud.Uflow.Entity;

namespace UWay.Skynet.Cloud.Uflow.Service.Interface
{
    public interface IDispatchFlowService
    {
        R<string> StartFlow(StartFlowDTO startFlowInfo);

        R<string> DealFlowStep(DealStepDTO dealStepDTO);

        R<string> TerminateFlow(Guid guid);


    }
}
