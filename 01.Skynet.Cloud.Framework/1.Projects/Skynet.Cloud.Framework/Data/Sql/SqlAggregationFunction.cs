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
	/// Specifies which function should be applied on a column
	/// </summary>
	public enum SqlAggregationFunction 
	{ 
		/// <summary>No function</summary>
		None, 
		/// <summary>Sum</summary>
		Sum, 
		/// <summary>Count rows</summary>
		Count, 
		/// <summary>Avarage</summary>
		Avg, 
		/// <summary>Minimum</summary>
		Min, 
		/// <summary>Maximum</summary>
		Max,
		/// <summary>Returns true is the current row is a super-aggregate row when used with ROLLUP or CUBE</summary>
		/// <remarks>Grouping functions is not supported in all databases</remarks>
		Grouping,
	}
}
