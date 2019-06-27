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
	/// Determines the kind of a time period
	/// </summary>
	public enum TimePeriodType 
	{ 
		/// <summary>Days</summary>
		Day, 
		/// <summary>Weeks</summary>
		Week, 
		/// <summary>Months</summary>
		Month, 
		/// <summary>Fiscal quarters (see <see cref="FiscalQuarter"/>)</summary>
		FiscalQuarter, 
		/// <summary>Calendar quarters</summary>
		CalendarQuarter, 
		/// <summary>Years</summary>
		Year 
	}

	/// <summary>
	/// Encapsulates a time period.
	/// </summary>
	/// <remarks>
	/// Use the <see cref="TimePeriod"/> class to make calculations in terms of days, weeks, months, quarters or years.
	/// </remarks>
	public class TimePeriod
	{
		TimePeriodType periodType;
		DateTime periodStartDate;
		
		private TimePeriod(TimePeriodType periodType, DateTime periodStartDate)
		{
			this.periodType = periodType;
			this.periodStartDate = periodStartDate;
		}

		/// <summary>
		/// Returns the start date of this TimePeriod
		/// </summary>
		public DateTime PeriodStartDate
		{
			get { return periodStartDate; }
		}

		/// <summary>
		/// Returns the type of this TimePeriod
		/// </summary>
		public TimePeriodType PeriodType
		{
			get { return periodType;}
		}

		/// <summary>
		/// Adds or sutracts time periods
		/// </summary>
		/// <param name="count">The number of periods of the same time to add</param>
		/// <returns>The resulting TimePeriod</returns>
		/// <remarks>Use negative <paramref name="count"/> values to substract periods</remarks>
		public TimePeriod Add(int count)
		{
			return new TimePeriod(periodType, Add(periodType, count));
		}

		/// <summary>
		/// Returns the difference in periods between two TimePeriods
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <returns>The difference in number of periods</returns>
		/// <remarks>The types if both periods must be the same.</remarks>
		public static int GetDifference(TimePeriod from, TimePeriod to)
		{
			if (from.PeriodType != to.PeriodType)
				throw new ApplicationException("Can't calculate difference between TimePeriods with different types.");

            TimePeriodType periodType = from.PeriodType;
			int offset = 0;
			if (periodType == TimePeriodType.Day)
				offset = Convert.ToInt32((to.PeriodStartDate - from.PeriodStartDate).TotalDays);
			else if (periodType == TimePeriodType.Month)
				offset = GetDifferenceInMonths(from.PeriodStartDate, to.PeriodStartDate);
			else if (periodType == TimePeriodType.CalendarQuarter || periodType == TimePeriodType.FiscalQuarter)
				offset = GetDifferenceInMonths(from.PeriodStartDate, to.PeriodStartDate) / 3;
			else if (periodType == TimePeriodType.Year)
				offset = to.PeriodStartDate.Year - from.PeriodStartDate.Year;
			else
				throw new ApplicationException("Unknown TimePeriodType: " + periodType.ToString());

			return offset;	
		}

		/// <summary>
		/// Gets the difference in months between two TimePeriod
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <returns></returns>
		static int GetDifferenceInMonths(DateTime from, DateTime to)
		{
			bool ordered = from <= to;
			DateTime early = (ordered) ? from : to;
			DateTime late = (ordered) ? to : from;
			int sign = (ordered) ? 1 : -1;
			int absDiff;
			if (late.Year == early.Year)
				absDiff = late.Month - early.Month;
			else
				absDiff = (12 - early.Month) + (late.Year - early.Year - 1) * 12 + late.Month;

			return absDiff * sign;
		}

		/// <summary>
		/// Returns the difference in periods between today's period and self
		/// </summary>
		/// <returns></returns>
		public int OffsetFromToday()
		{
			return GetDifference(FromToday(periodType), this);
		}

		/// <summary>
		/// Returns the index in year of this TimePeriod
		/// </summary>
		/// <returns></returns>
		/// <remarks>The index is zero based.</remarks>
		public int GetIndexInYear()
		{
			int index = 0;
			if (periodType == TimePeriodType.Day)
				index = periodStartDate.DayOfYear;
			else if (periodType == TimePeriodType.Month)
				index = periodStartDate.Month;
			else if (periodType == TimePeriodType.CalendarQuarter)
				index = ((periodStartDate.Month - 1) / 3)  + 1;
			else if (periodType == TimePeriodType.FiscalQuarter)
			{
				FiscalQuarter fq = new FiscalQuarter();
				index = fq.GetQuarterForDate(periodStartDate);
			}

			return index;	
		}

		/// <summary>
		/// Adds or subtract a number of periods of a specified type
		/// </summary>
		/// <param name="addPeriodType">Type of period to add</param>
		/// <param name="addValue">The number of periods to add</param>
		/// <returns>DateTime value which is the result of the addition</returns>
		/// <remarks>Use negative <paramref name="values"/> to substract.</remarks>
		public DateTime Add(TimePeriodType addPeriodType, int addValue)
		{
			DateTime baseDate = periodStartDate;
			DateTime dateVal = baseDate;
			if (addPeriodType == TimePeriodType.Day)
				dateVal = baseDate.AddDays(addValue);
			else if (addPeriodType == TimePeriodType.Month)
				dateVal = baseDate.AddMonths(addValue);
			else if (addPeriodType == TimePeriodType.CalendarQuarter || addPeriodType == TimePeriodType.FiscalQuarter)
				dateVal= baseDate.AddMonths(addValue * 3);
			else if (addPeriodType == TimePeriodType.Year)
				dateVal = baseDate.AddYears(addValue);
			else
				throw new ApplicationException("Unknown TimePeriodType: " + addPeriodType.ToString());
			return dateVal;
		}

		/// <summary>
		/// Create a time period based on a specified date.
		/// </summary>
		/// <param name="type">The type of the TimePeriod to create</param>
		/// <param name="date">Any date which falls into the desired period</param>
		/// <returns>New TimePeriod instance</returns>
		public static TimePeriod FromDate(TimePeriodType type, DateTime date)
		{
			DateTime val;
			TimePeriodType baseDateType = type;
			if (baseDateType == TimePeriodType.Day)
				val = date.Date;
			else if (baseDateType == TimePeriodType.CalendarQuarter)
			{
				int quarter = (date.Month - 1) / 3;
				int month = (quarter * 3) + 1;
				val = new DateTime(date.Year, month, 1);
			}
			else if (baseDateType == TimePeriodType.FiscalQuarter)
			{
				FiscalQuarter fq = new FiscalQuarter();
				val = fq.GetStartDate(fq.GetQuarterForDate(date), date.Year);
			}
			else if (baseDateType == TimePeriodType.Month)
				val = new DateTime(date.Year, date.Month, 1);
			else if (baseDateType == TimePeriodType.Year)
				val = new DateTime(date.Year, 1, 1);
			else if (baseDateType == TimePeriodType.Week)
			{
				val = date;
				val.AddDays(-1 * (int)val.DayOfWeek);
			}
			else 
				throw new ApplicationException("Unknown TimePeriodType: " + type.ToString());
			
			return new TimePeriod(type, val);
		}

		/// <summary>
		/// Creates a new TimePeriod based on today's date.
		/// </summary>
		/// <param name="type">The type of the TimePeriod to create</param>
		/// <returns>A new TimePeriod instance</returns>
		public static TimePeriod FromToday(TimePeriodType type)
		{
			return FromDate(type, DateTime.Today);
		}

		/// <summary>
		/// Serializes this TimePeriod into a string
		/// </summary>
		/// <returns>String representation of this TimePeriod</returns>
		/// <remarks>Returns a string which can be understood by Parse</remarks>
		public string Serialize()
		{
			string periodName;
			TimePeriod currentPeriod = FromToday(periodType);
			if (periodType == TimePeriodType.FiscalQuarter)
				periodName = "NextFiscalQuarter";
			else if (periodType == TimePeriodType.CalendarQuarter)
				periodName = "NextCalendarQuarter";
			else if (periodType == TimePeriodType.Month)
				periodName = "NextMonth";
			else if (periodType == TimePeriodType.Year)
				periodName = "NextYear";
			else if (periodType == TimePeriodType.Day)
				periodName = "NextDay";
			else
				throw new ApplicationException("Unsupported period for serialization");

			int offset = OffsetFromToday();
			return string.Format("{0}.{1}", periodName, offset);
		}

		/// <summary>
		/// Creates a time period from a string.
		/// </summary>
		/// <param name="periodString"></param>
		/// <returns></returns>
		public static TimePeriod Parse(string periodString)
		{
			string[] parts = periodString.Split('.');
			if (parts.Length != 2)
				return null;

			TimePeriod period = null;
			string periodName = parts[0];
			if (string.Compare(periodName, "NextFiscalQuarter", true) == 0)
				period = FromToday(TimePeriodType.FiscalQuarter);
			else if (string.Compare(periodName, "NextCalendarQuarter", true) == 0)
				period = FromToday(TimePeriodType.CalendarQuarter);
			else if (string.Compare(periodName, "NextMonth", true) == 0)
				period = FromToday(TimePeriodType.Month);
			else if (string.Compare(periodName, "NextYear", true) == 0)
				period = FromToday(TimePeriodType.Year);
			else if (string.Compare(periodName, "NextDay", true) == 0)
				period = FromToday(TimePeriodType.Day);

			if (period != null)
			{
				int offset = Convert.ToInt32(parts[1]);
				period = period.Add(offset);
			}

			return period;
		}

		/// <summary>
		/// Returns a human readable representation of TimePeriod
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string val;
			if (periodType == TimePeriodType.Day)
				val = periodStartDate.ToShortDateString();
			else if (periodType == TimePeriodType.CalendarQuarter)
				val = string.Format("Q{0},{1}", GetIndexInYear(), periodStartDate.Year);
			else if (periodType == TimePeriodType.FiscalQuarter)
				val = string.Format("Q{0},{1}", GetIndexInYear(), periodStartDate.Year);
			else if (periodType == TimePeriodType.Month)
				return string.Format("{0}/{1}", GetIndexInYear(), periodStartDate.Year);
			else if (periodType == TimePeriodType.Year)
				return string.Format("{0}", periodStartDate.Year);
			else if (periodType == TimePeriodType.Week)
			{
				DateTime periodEndDate = periodStartDate.AddDays(6);
				val = string.Format("{0}/{1}-{2}/{3}", periodStartDate.Month, periodStartDate.Day, periodEndDate.Month, periodEndDate.Day);
			}
			else 
				throw new ApplicationException("Unknown TimePeriodType: " + periodType.ToString());
			return val;
		}
	}
}
