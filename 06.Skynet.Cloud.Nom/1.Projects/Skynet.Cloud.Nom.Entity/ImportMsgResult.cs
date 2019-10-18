using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Nom.Entity
{
    public class ImportMsgResult
    {
        /// <summary>
        /// 导入过程是否发生异常
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 成功条数
        /// </summary>
        public long Success { get; set; }

        /// <summary>
        /// 失败条数
        /// </summary>
        public long Faile { set; get; }

        /// <summary>
        /// 重复条数
        /// </summary>
        public long Multi { get; set; }
    }
}
