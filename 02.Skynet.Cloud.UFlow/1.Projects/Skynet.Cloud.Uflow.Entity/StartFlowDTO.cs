using System;
using System.Collections.Generic;
using System.Text;
using UWay.Skynet.Cloud.Request;
using UWay.Skynet.Cloud.Extensions;

namespace UWay.Skynet.Cloud.Uflow.Entity
{
    /// <summary>
    /// 流程启动信息
    /// </summary>
    public class StartFlowDTO
    {
        /// <summary>
        /// 工作组ID
        /// </summary>
        public string GroupID;
        /// <summary>
        /// 发起人
        /// </summary>
        public string Operator;
        /// <summary>
        /// 流程模板编号
        /// </summary>
        public string FlowCode;
        /// <summary>
        /// 实例编号
        /// </summary>
        public string InstanceCode = null;
        /// <summary>
        /// 实例初始数据
        /// </summary>
        public string InitData = null;
        /// <summary>
        /// 开始步骤的启动数据
        /// </summary>
        public string StartData = null;
        /// <summary>
        /// 指派的实例ID
        /// </summary>
        public Guid? InstanceID = null;
        /// <summary>
        /// 指派的实例开始步骤ID
        /// </summary>
        public Guid? InstanceStepID = null;

        ///// <summary>
        ///// 构造方法，流程启动信息
        ///// </summary>
        ///// <param name="AGroupID">工作组织ID</param>
        ///// <param name="AOperator">操作人</param>
        ///// <param name="AFlowCode">流程编号</param>
        ///// <param name="AInstanceCode">流程实例编号</param>
        ///// <param name="AInitData">初始数据</param>
        //public StartFlowInfo(string AGroupID, string AOperator, string AFlowCode, string AInstanceCode = null, string AInitData = null)
        //{
        //    GroupID = AGroupID;
        //    Operator = AOperator;
        //    FlowCode = AFlowCode;
        //    InstanceCode = AInstanceCode;
        //    InitData = AInitData;
        //}

        /// <summary>
        /// 数据验证
        /// </summary>
        /// <returns></returns>
        public R<string> Validate()
        {
            R<string> vResMsg = new R<string>();
            vResMsg.Code = 1;
            if (string.IsNullOrWhiteSpace(GroupID))
            {
                vResMsg.Msg = "启动流程的方法参数[工作组织ID]不能为空。";
                return vResMsg;
            }

            if (string.IsNullOrWhiteSpace(Operator))
            {
                vResMsg.Msg = "启动流程的方法参数[操作人]不能为空。";
                return vResMsg;
            }

            if (string.IsNullOrWhiteSpace(FlowCode))
            {
                vResMsg.Msg = "启动流程的方法参数[流程编号]不能为空。";
                return vResMsg;
            }

            if (!string.IsNullOrWhiteSpace(InitData) && !InitData.IsXml())
            {
                vResMsg.Msg = "传入的初始数据不是正确的XML格式。";
                return vResMsg;
            }

            if (!string.IsNullOrWhiteSpace(StartData) && !StartData.IsXml())
            {
                vResMsg.Msg = "传入的初始数据不是正确的XML格式。";
                return vResMsg;
            }

            vResMsg.Code = 0;

            return vResMsg;
        }
    }

}
