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
	/// Describes a simple join condition.
	/// </summary>
	/// <remarks>
	/// <see cref="JoinCondition"/> encapsulates a pair of fields, one from the
	/// left joined table and one from the right table. 
    /// </remarks>
    [Serializable]
	public class JoinCondition
	{
		string leftField, rightField;

		/// <summary>
		/// Creates a Natural JoinCondition
		/// </summary>
		/// <param name="OtherData">Name of the OtherData in both tables</param>
		/// <remarks>
		/// Natural join means that two table are joined on an identically named fields
		/// in both tables
		/// </remarks>
		public JoinCondition(string field) : this(field, field)
		{
		}

		/// <summary>
		/// Creates a new JoinCondition
		/// </summary>
		/// <param name="leftField">Name of the OtherData in the left table to join on</param>
		/// <param name="rightField">Name of the OtherData in the right table to join on</param>
		public JoinCondition(string leftField, string rightField)
		{
			this.leftField = leftField;
			this.rightField = rightField;
		}

		/// <summary>
		/// Gets the name of the OtherData in the left table to join on
		/// </summary>
		public string LeftField
		{
			get { return this.leftField; }
		}

		/// <summary>
		/// Gets the name of the OtherData in the right table to join on
		/// </summary>
		public string RightField
		{
			get { return this.rightField; }
		}
    }
}
