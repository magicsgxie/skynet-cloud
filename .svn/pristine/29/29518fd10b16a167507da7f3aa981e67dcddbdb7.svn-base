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

namespace UWay.Skynet.Cloud.Data.Reporting
{
	/// <summary>
	/// 时间转换操作类
	/// </summary>
	public class FiscalQuarter
	{
		DateTime[] quarters = new DateTime[4];
		int year;

		static int startDay = 1, startMonth = 1;

		/// <summary>
		/// Creates a new FiscalQuarter
		/// </summary>
		public FiscalQuarter()
		{
			DateTime today = DateTime.Today;
			int fiscalQuarterMonth = startMonth;
			int fiscalQuarterDay = startDay;
			year = today.Year;
			quarters[0] = new DateTime(year, fiscalQuarterMonth, fiscalQuarterDay);
			quarters[1] = quarters[0].AddMonths(3);
			quarters[2] = quarters[0].AddMonths(6);
			quarters[3] = quarters[0].AddMonths(9);
		}

		/// <summary>
		/// 获取当前季度
		/// </summary>
        /// <returns>第几季度</returns>
		public int GetCurrentQuarter()
		{
			return GetQuarterForDate(DateTime.Today);
		}
		
		/// <summary>
		/// 获取天所在的季度
		/// </summary>
		/// <param name="date">时间，例如：2010-10-11</param>
        /// <returns>第几季度</returns>
		public int GetQuarterForDate(DateTime date)
		{
			DateTime thisYearDate = new DateTime(year, date.Month, date.Day);
			for (int i = 0; i < 3; i++)
			{
				if (quarters[i] <= thisYearDate && thisYearDate < quarters[i+1])
					return i + 1;
			}
			return 4;
		}

		/// <summary>
		/// 获取季度的开始时间 例如：2010年第3季度返回2010-09-01
		/// </summary>
		/// <param name="quarter">季度</param>
		/// <param name="year">年</param>
        /// <returns>季度的开始时间</returns>
		public DateTime GetStartDate(int quarter, int year)
		{
			DateTime date = quarters[quarter - 1];
			return new DateTime(year, date.Month, date.Day);
		}

		/// <summary>
        /// 天的开始天数，举例：有部分公司的考勤是以10号开始的，将StartDay设置为10，那么1月的统计周期将从1月10日到2月10日
		/// </summary>
		public static int StartDay
		{
			get { return startDay; }
			set { startDay = value; }
		}

		/// <summary>
        ///月的开始月份，举例：有部分公司的年报是以2月开始的，将StartMonth设置为2，那么2010年的统计周期将从2010年2月到2011年2月
		/// </summary>
		public static int StartMonth
		{
			get { return startMonth; }
			set { startMonth = value; }
		}
	}
}
