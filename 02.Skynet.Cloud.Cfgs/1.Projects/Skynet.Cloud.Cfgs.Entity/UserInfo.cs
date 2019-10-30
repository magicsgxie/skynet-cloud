using System;
using System.Collections.Generic;
using System.Text;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud;

namespace UWay.Skynet.Cloud.Upms.Entity
{
    public class UserInfo
    {
        /// <summary>
        /// 用户级别
        /// </summary>
        public virtual UserLevelEnum UserLevel
        {
            set;
            get;
        }

        /// <summary>
        /// 是否有效0:有效;1:无效
        /// </summary>
        public virtual int Invalid
        {
            set;
            get;
        }
        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password
        {
            set;
            get;
        }


        /// <summary>
        /// 用户ID
        /// </summary>
        
        public int UserID
        {
            set;
            get;
        }

        /// <summary>
        /// 用户登录名
        /// </summary>
        public virtual string UserNo
        {
            set;
            get;
        }

        /// <summary>
        /// 用户名称
        /// </summary>
        public virtual string UserName
        {
            set;
            get;
        }


        /// <summary>
        /// 组织ID
        /// </summary>
        public virtual int OrgID
        {
            set;
            get;
        }


        /// <summary>
        /// 性别
        /// </summary>
        public virtual int Sex
        {
            set;
            get;
        }

        /// <summary>
        /// 移动电话
        /// </summary>
        public virtual string Mobile
        {
            set;
            get;
        }

        /// <summary>
        /// 邮件
        /// </summary>
        public virtual string Email
        {
            set;
            get;
        }

        /// <summary>
        /// 固定电话
        /// </summary>
        public virtual string Phone
        {
            set;
            get;
        }

        /// <summary>
        /// 传真
        /// </summary>
        public virtual string Fax
        {
            set;
            get;
        }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public virtual int IsLocked
        {
            set;
            get;
        }
        /// <summary>
        /// 锁定日期
        /// </summary>
        public virtual DateTime? LockDate
        {
            set;
            get;
        }

        /// <summary>
        /// 创建者
        /// </summary>
        public virtual string Author
        {
            set;
            get;
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        public virtual DateTime CreateDate
        {
            set;
            get;
        }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public virtual string Mender
        {
            set;
            get;
        }

        /// <summary>
        /// 修改日期
        /// </summary>
        public virtual DateTime? UpdateDate
        {
            set;
            get;
        }

        /// <summary>
        /// 通信地址
        /// </summary>
        public virtual string Address
        {
            set;
            get;
        }

        //[ComputedColumn("floor(to_number((update_date-create_date))*24*60*60)")]
        //public long TimeLength
        //{
        //    set; get;
        //}


        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark
        {
            set;
            get;
        }

        /// <summary>
        /// 岗位技能认证级别
        /// </summary>
        public virtual int PostLevel
        {
            set;
            get;
        }

        /// <summary>
        /// 学历
        /// </summary>
        public virtual int Educational
        {
            set;
            get;
        }

        /// <summary>
        /// 职称
        /// </summary>
        public virtual int PositionalTitle
        {
            set;
            get;
        }

        /// <summary>
        /// 网优奖项
        /// </summary>
        public virtual string Prize
        {
            set;
            get;
        }

        /// <summary>
        /// 是否内训师
        /// </summary>
        public virtual int IsInternalTrainer
        {
            set;
            get;
        }

        /// <summary>
        /// 岗位
        /// </summary>
        public virtual int Post
        {
            set;
            get;
        }

        /// <summary>
        /// 出生年月
        /// </summary>
        public virtual DateTime? Birthday
        {
            set;
            get;
        }

        /// <summary>
        /// QQ
        /// </summary>
        public virtual string QQNumber
        {
            set;
            get;
        }

        /// <summary>
        /// 其他网络联系方式
        /// </summary>
        public virtual string OtherContactWay
        {
            set;
            get;
        }

        /// <summary>
        /// 资质
        /// </summary>
        public virtual int Aptitude
        {
            set;
            get;
        }

        /// <summary>
        /// 易信
        /// </summary>
        public virtual string Yixin
        {
            set;
            get;
        }

        /// <summary>
        /// 微信
        /// </summary>
        public virtual string Wechat
        {
            set;
            get;
        }

        /// <summary>
        /// 城市
        /// </summary>
        public virtual int CityID
        {
            get;
            set;
        }

        /// <summary>
        /// 省份
        /// </summary>
        public virtual string ProvinceName
        {
            get;
            set;
        }

        /// <summary>
        /// 是否网优人员
        /// </summary>
        public virtual int IsNetworkOptimizetor
        {
            set;
            get;
        }

        /// <summary>
        /// 人员归属
        /// </summary>
        public virtual int UserOptimizstionTypeCode
        {
            set;
            get;
        }

        /// <summary>
        /// 专项接口
        /// </summary>
        public virtual string Port
        {
            set;
            get;
        }

        /// <summary>
        /// 0:开启;1:禁用
        /// </summary>
        public virtual int IsEnable
        {
            set;
            get;
        }


        /// <summary>
        /// 总积分
        /// </summary>
        public virtual int TotalScore
        {
            set;
            get;
        }
        /// <summary>
        /// 可用积分
        /// </summary>
        public virtual int AvailableCredits
        {
            set;
            get;
        }
    }
}
