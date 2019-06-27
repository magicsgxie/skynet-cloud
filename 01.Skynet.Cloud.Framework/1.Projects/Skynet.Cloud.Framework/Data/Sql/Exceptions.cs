/*----------------------------------------------------------------
// Copyright (C) 2010 深圳市优网科技有限公司
// 版权所有。 
//
// 文件名：
// 文件功能描述：
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

namespace UWay.Skynet.Cloud.Data
{
	/// <summary>
	/// InvalidQueryException exception can be thrown when the renderer decides that a query is invalid or incompatible with the target database.
    /// </summary>
    [Serializable]
	public class InvalidQueryException : ApplicationException
	{
		/// <summary>
		/// Creates a new InvalidQueryException
		/// </summary>
		/// <param name="text">Text of the exception</param>
		public InvalidQueryException(string text) : base(text) {}
	}
}
