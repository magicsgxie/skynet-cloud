using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    /// <summary>
    /// Operator used in <see cref="FilterDescriptor"/>
    /// </summary>
    public enum FilterOperator
    {
        /// <summary>
        /// Left operand must be smaller than the right one.
        /// </summary>
        [Description("<")]
        IsLessThan = 0,
        /// <summary>
        /// Left operand must be smaller than or equal to the right one.
        /// </summary>
        [Description("<=")]
        IsLessThanOrEqualTo = 1,
        /// <summary>
        /// Left operand must be equal to the right one.
        /// </summary>
        [Description("=")]
        IsEqualTo = 2,
        /// <summary>
        /// Left operand must be different from the right one.
        /// </summary>
        [Description("!=")]
        IsNotEqualTo = 3,
        /// <summary>
        /// Left operand must be larger than the right one.
        /// </summary>
        [Description(">=")]
        IsGreaterThanOrEqualTo = 4,
        /// <summary>
        /// Left operand must be larger than or equal to the right one.
        /// </summary>
        [Description(">")]
        IsGreaterThan = 5,

        /// <summary>
        /// Like
        /// </summary>
        [Description("匹配")]
        Like = 6,
        /// <summary>
        /// Left operand must start with the right one.
        /// </summary>
        [Description("开头为")]
        StartsWith = 7,
        /// <summary>
        /// Left operand must end with the right one.
        /// </summary>
        [Description("结尾为")]
        EndsWith = 8,
        /// <summary>
        /// Left operand must contain the right one.
        /// </summary>
        [Description("包含")]
        Contains = 9,
        /// <summary>
        /// Left operand must be contained in the right one.
        /// </summary>
        [Description("列表包含")]
        In = 10,
        /// <summary>
        /// Left operand must not contain the right one.
        /// </summary>
        [Description("不包含")]
        DoesNotContain = 11,

        [Description("开头不包括")]
        IsNotStartsWith,

        [Description("结尾不包括")]
        IsNotEndsWith,

        [Description("为空")]
        IsNull,

        [Description("非空")]
        IsNotNull,

        [Description("不在列表中")]
        NotIn,

        DateTimeLessThanOrEqual,

        DateBlock, //日期用到，其余用不到
    }


    public static class FilterOperatorHelper
    {
        public static string Where(this FilterOperator filterOperator, string fieldName, object value)
        {
            return Where(filterOperator, "", fieldName, value);
        }

        public static string Where(this FilterOperator filterOperator, string prefix, string fieldName, object paramName)
        {
            string formate = string.Empty;
            prefix = prefix.IsNullOrEmpty() ? "" : prefix + ".";
            switch (filterOperator)
            {
                case FilterOperator.IsGreaterThanOrEqualTo:
                    formate = string.Format(" {0}{1} >= @{2} ", prefix, fieldName, paramName);
                    break;
                case FilterOperator.IsGreaterThan:
                    formate = string.Format(" {0}{1} > @{2} ", prefix, fieldName, paramName);
                    break;
                case FilterOperator.IsLessThan:
                    formate = string.Format(" {0}{1} < @{2} ", prefix, fieldName, paramName);
                    break;
                case FilterOperator.IsLessThanOrEqualTo:
                    formate = string.Format(" {0}{1}  <= @{2}", prefix, fieldName, paramName);
                    break;
                case FilterOperator.StartsWith:
                    formate = string.Format(" {0}{1}  like ''||@{2}||'%' ", prefix, fieldName, paramName);
                    break;
                case FilterOperator.EndsWith:
                    formate = string.Format(" {0}{1}  like '%'||@{2}||'' ", prefix, fieldName, paramName);
                    break;
                case FilterOperator.Contains:
                    formate = string.Format(" {0}{1}  like '%'||@{2}||'%' ", prefix, fieldName, paramName);
                    break;
                case FilterOperator.In:
                    formate = string.Format(" instr(','||@{2}||',',','||{0}{1} ||',') > 0 ", prefix, fieldName, paramName);
                    break;
                case FilterOperator.DoesNotContain:
                    formate = string.Format(" {0}{1}  not like '%'||@{2}||'%' ", prefix, fieldName, paramName);
                    break;
                case FilterOperator.IsNotEqualTo:
                    formate = string.Format(" {0}{1}  <> @{2} ", prefix, fieldName, paramName);
                    break;
                default:
                    formate = string.Format(" {0}{1}  = @{2} ", prefix, fieldName, paramName);
                    break;
            }
            return formate;
        }

        public static string SqlOperator(this FilterOperator filterOperator)
        {
            string formate = string.Empty;
            switch (filterOperator)
            {
                case FilterOperator.IsGreaterThanOrEqualTo:
                    formate = ">=";
                    break;
                case FilterOperator.IsGreaterThan:
                    formate = ">";
                    break;
                case FilterOperator.IsLessThan:
                    formate = "<";
                    break;
                case FilterOperator.IsLessThanOrEqualTo:
                    formate = "<=";
                    break;
                case FilterOperator.StartsWith:
                    formate = "like";
                    break;
                case FilterOperator.EndsWith:
                    formate = "like"; ;
                    break;
                case FilterOperator.Contains:
                    formate = "like"; ;
                    break;
                case FilterOperator.In:
                    formate = "IN";
                    break;
                case FilterOperator.DoesNotContain:
                    formate = " not like ";
                    break;
                case FilterOperator.IsNotEqualTo:
                    formate = "<>";
                    break;
                default:
                    formate = "=";
                    break;
            }
            return formate;
        }
    }
}