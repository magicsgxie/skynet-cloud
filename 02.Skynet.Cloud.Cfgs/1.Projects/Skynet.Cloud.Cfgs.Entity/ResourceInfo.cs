using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Upms.Entity
{
    /// <summary>
    /// 菜单信息表
    /// </summary>
    public class ResourceInfo
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public virtual int MenuID
        {
            set;
            get;
        }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public virtual string MenuName
        {
            set;
            get;
        }


        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual int IsEffect
        {
            set;
            get;
        }


        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Url
        {
            set;
            get;
        }

        /// <summary>
        /// 类型: 1：菜单， 2： 功能按钮
        /// </summary>
        public virtual int ResourceType
        {
            set;
            get;
        }


        /// <summary>
        /// 父级别菜单ID
        /// </summary>
        public virtual int? ParentID
        {
            set;
            get;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int MenuOrder
        {
            set;
            get;
        }


        /// <summary>
        /// 图标
        /// </summary>
        public virtual string MenuIcon
        {
            set;
            get;
        }


        /// <summary>
        /// 编码
        /// </summary>
        public virtual string MenuCode
        {
            set;
            get;
        }


        /// <summary>
        /// 提示
        /// </summary>
        public virtual string TipText
        {
            set;
            get;
        }

        /// <summary>
        /// 应用程序ID
        /// </summary>
        public virtual int? AppID
        {
            set;
            get;
        }

        public virtual int? IsSystem
        {
            set;
            get;
        }
        public virtual int? FuncKind
        {
            set;
            get;
        }


        public virtual int PermissSuper
        {
            set;
            get;
        }


        public virtual int PermissUser
        {
            set;
            get;
        }

        public virtual int PermissUnit
        {
            set;
            get;
        }
    }
}
