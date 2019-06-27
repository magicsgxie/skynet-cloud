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
	/// Encapsulates a single WHEN ... THEN ... statement
    /// </summary>
    [Serializable]
	public class CaseTerm
	{
		WhereClause cond;
		SqlExpression val;

		/// <summary>
		/// Creates a new CaseTerm
		/// </summary>
		/// <param name="condition">Condition for the WHEN clause</param>
		/// <param name="val">Value for the THEN clause</param>
		public CaseTerm(WhereClause condition, SqlExpression val)
		{
			this.cond = condition;
			this.val = val;
		}

		internal WhereClause Condition
		{
			get { return this.cond; }
		}

		internal SqlExpression Value
		{
			get { return this.val; }
		}
	}
}
