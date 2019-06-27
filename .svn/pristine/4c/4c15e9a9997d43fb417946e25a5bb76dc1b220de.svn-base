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
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Data.Reporting
{
	/// <summary>
	/// 透视图的列
	/// </summary>
	public class PivotColumn
	{
		SqlDataType dataType;
		string columnField;
		PivotColumnValueCollection values = new PivotColumnValueCollection();
		
		/// <summary>
		///创建一个新列
		/// </summary>
		public PivotColumn()
		{
		}

		/// <summary>
        ///创建一个新列
		/// </summary>
		/// <param name="columnField">字段名称</param>
		/// <param name="dataType">字段类型</param>
		public PivotColumn(string columnField, SqlDataType dataType)
		{
			this.columnField = columnField;
			this.dataType = dataType;
		}

		/// <summary>
		/// 字段名称
		/// </summary>
		public string ColumnField 
		{ 
			get { return columnField; } 
			set { columnField = value; } 
		}

		/// <summary>
		/// 值
		/// </summary>
		public PivotColumnValueCollection Values
		{ 
			get { return values; } 
		}

		/// <summary>
        /// 字段类型
		/// </summary>
		public SqlDataType DataType
		{
			get { return dataType; }
			set { dataType = value; }
		}
	}
}
