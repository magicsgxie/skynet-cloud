﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UWay.Skynet.Cloud.IE.Core.Models;

namespace UWay.Skynet.Cloud.IE.Core
{

    public interface BaseImportResult
    {
        /// <summary>
        ///     验证错误
        /// </summary>
        IList<DataRowErrorInfo> RowErrors { get; set; }

        /// <summary>
        ///     模板错误
        /// </summary>
        IList<TemplateErrorInfo> TemplateErrors { get; set; }

        /// <summary>
        ///     导入异常信息
        /// </summary>
        Exception Exception { get; set; }

        /// <summary>
        ///     是否存在导入错误
        /// </summary>
        bool HasError {  get; }
    }

    public class ImportDataTableResult : BaseImportResult
    {
        /// <summary>
        ///     导入数据
        /// </summary>
        public virtual DataTable Data { get; set; }

        /// <summary>
        ///     验证错误
        /// </summary>
        public virtual IList<DataRowErrorInfo> RowErrors { get; set; }

        /// <summary>
        ///     模板错误
        /// </summary>
        public virtual IList<TemplateErrorInfo> TemplateErrors { get; set; }

        /// <summary>
        ///     导入异常信息
        /// </summary>
        public virtual Exception Exception { get; set; }

        /// <summary>
        ///     是否存在导入错误
        /// </summary>
        public virtual bool HasError => Exception != null ||
                                        (TemplateErrors?.Count(p => p.ErrorLevel == ErrorLevels.Error) ?? 0) > 0 ||
                                        (RowErrors?.Count ?? 0) > 0;

        
    }


    /// <summary>
    ///     导入结果
    /// </summary>
    public class ImportResult<T>:BaseImportResult where T : class
    {
        /// <summary>
        ///     导入数据
        /// </summary>
        public virtual ICollection<T> Data { get; set; }


        /// <summary>
        ///     验证错误
        /// </summary>
        public virtual IList<DataRowErrorInfo> RowErrors { get; set; }

        /// <summary>
        ///     模板错误
        /// </summary>
        public virtual IList<TemplateErrorInfo> TemplateErrors { get; set; }

        /// <summary>
        ///     导入异常信息
        /// </summary>
        public virtual Exception Exception { get; set; }

        /// <summary>
        ///     是否存在导入错误
        /// </summary>
        public virtual bool HasError => Exception != null ||
                                        (TemplateErrors?.Count(p => p.ErrorLevel == ErrorLevels.Error) ?? 0) > 0 ||
                                        (RowErrors?.Count ?? 0) > 0;
    }
}
