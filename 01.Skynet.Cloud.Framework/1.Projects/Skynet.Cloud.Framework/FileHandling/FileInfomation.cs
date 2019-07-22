using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.FileHandling
{
    /// <summary>
    ///  文件信息
    /// </summary>
    public class FileInfomation
    {
        /// <summary>
        /// 目录或文件的完整目录。
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifyDate { get; set; }

        /// <summary>
        /// 文件长度
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// 是否存在
        /// </summary>
        public bool Exists { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string Extension { get; set; }
    }
}
