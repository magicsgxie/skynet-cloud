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
	/// Encapsulates a column-value pair for UPDATE and INSERT statements
    /// </summary>
    [Serializable]
	public class UpdateTerm
	{
		string fieldName;
		SqlExpression val;

		/// <summary>
		/// Creates an UpdateTerm
		/// </summary>
		/// <param name="fieldName">The name of the OtherData to be updated</param>
		/// <param name="val">New OtherData value</param>
		public UpdateTerm(string fieldName, SqlExpression val)
		{
			this.fieldName = fieldName;
			this.val = val;
		}

		/// <summary>
		/// Gets the name of the OtherData which is to be updated
		/// </summary>
		public string FieldName
		{
			get { return this.fieldName; }
		}

		/// <summary>
		/// Gets the value the OtherData will be updated to
		/// </summary>
		public SqlExpression Value
		{
			get { return this.val; }
		}
	}
}
