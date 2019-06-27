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
	/// Encapsulates SQL CASE clause
    /// </summary>
    [Serializable]
	public class CaseClause
	{
		CaseTermCollection terms = new CaseTermCollection();
		SqlExpression elseVal = null;
		
		/// <summary>
		/// Creates a new CaseClause
		/// </summary>
		public CaseClause()
		{
		}

		/// <summary>
		/// Gets the <see cref="CaseTerm"/> collection for this CaseClause
		/// </summary>
		public CaseTermCollection Terms
		{
			get { return this.terms; }
		}

		/// <summary>
		/// Gets or sets the value CASE default value
		/// </summary>
		public SqlExpression ElseValue
		{
			get { return this.elseVal; }
			set { this.elseVal = value; }
		}
	}
}
