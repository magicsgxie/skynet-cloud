using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Uflow.Entity
{
    public class DealStepDTO
    {
        /// <summary>
        /// 工作组织ID
        /// </summary>
        public string GroupID;

        /// <summary>
        /// 业务用户编号
        /// </summary>
        public string UserCode;

        /// <summary>
        /// 当前实例ID
        /// </summary>
        public string InstanceID;

        /// <summary>
        /// 当前实例步骤ID
        /// </summary>
        public string InstanceStepID;

        /// <summary>
        /// 处理结果:0未处理,1通过,2退回,3转办,4不通过,5查看,6保存
        /// </summary>
        public int Status;

        /// <summary>
        /// 处理意见
        /// </summary>
        public string Content;

        /// <summary>
        /// 表单名称
        /// </summary>
        public string Form_Name;

        /// <summary>
        /// 业务数据
        /// </summary>
        public string Data;

        /// <summary>
        /// 指定的下一个处理步骤序号
        /// </summary>
        public string NextStepSeq;

        /// <summary>
        /// 指定的下一步个处理人
        /// </summary>
        public List<USERINFO> NextUserList;

        /// <summary>
        /// 扩展字段列表
        /// </summary>
        public List<ExtendField> Exfields;
    }
}
