using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Mvc
{
    /// <summary>
    /// 按钮操作类型
    /// 最好不要动
    /// </summary>
    public enum ButtonType
    {
        /// <summary>
        /// 查看
        /// </summary>
        View = 1,
        /// <summary>
        /// 新增
        /// </summary>
        Add = 2,
        /// <summary>
        /// 编辑
        /// </summary>
        Edit = 3,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 4,
        /// <summary>
        /// 打印
        /// </summary>
        Print = 5,
        /// <summary>
        /// 审核
        /// </summary>
        Check = 6,
        /// <summary>
        /// 作废
        /// </summary>
        Cancle = 7,
        /// <summary>
        /// 结束
        /// </summary>
        Finish = 8,
        /// <summary>
        /// 扩展
        /// </summary>
        Extend = 9
    }
}
