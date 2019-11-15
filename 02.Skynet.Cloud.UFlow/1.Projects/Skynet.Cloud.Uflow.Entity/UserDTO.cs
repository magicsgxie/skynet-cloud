using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace UWay.Skynet.Cloud.Uflow.Entity
{
    /// <summary>
    /// 提供的用户信息
    /// </summary>
    [XmlRoot(ElementName = "USERINFO", Namespace = "")]
    public class USERINFO
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [XmlElement(ElementName = "USERCODE", Namespace = "")]
        public string UserCode;

        /// <summary>
        /// 用户名称
        /// </summary>
        [XmlElement(ElementName = "USERNAME", Namespace = "")]
        public string UserName;

        /// <summary>
        /// 用户类型,0主办,1汇签,2协办,3抄送
        /// </summary>
        [XmlElement(ElementName = "USERTYPE", Namespace = "")]
        public int UserType;

        /// <summary>
        /// 用户类型名称,0主办,1汇签,2协办,3抄送
        /// </summary>
        [XmlElement(ElementName = "USERTYPENAME", Namespace = "")]
        public string UserTypeName;

        /// <summary>
        /// 用户处理状态：0未处理,1通过,2退回,3转办,4不通过,5查看,6保存
        /// </summary>
        [XmlElement(ElementName = "USERSTATUS", Namespace = "")]
        public int UserStatus;
    }

}
