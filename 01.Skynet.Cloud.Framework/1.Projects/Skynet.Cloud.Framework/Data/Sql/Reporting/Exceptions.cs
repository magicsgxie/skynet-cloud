/*----------------------------------------------------------------
// Copyright (C) 2010 深圳市优网科技有限公司
// 版权所有。 
//
// 文件名：pivot table异常
// 文件功能描述：pivot table异常
//
// 
// 创建标识：
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/

using System;

namespace UWay.Skynet.Cloud.Data.Reporting
{
	/// <summary>
	/// pivot table异常
	/// </summary>
	public class PivotTableException : ApplicationException
	{
		/// <summary>
        /// 创建一个新的pivot table异常
		/// </summary>
		/// <param name="text"></param>
		public PivotTableException(string text) : base(text) {}
	}

	/// <summary>
    /// pivot Transform异常
	/// </summary>
	public class PivotTransformException : ApplicationException 
	{
		/// <summary>
        /// 创建pivot Transform异常
		/// </summary>
		/// <param name="text"></param>
		public PivotTransformException(string text) : base(text) {}
	}
}
