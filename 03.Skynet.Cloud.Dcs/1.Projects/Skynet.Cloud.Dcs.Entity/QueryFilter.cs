using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;
using UWay.Skynet.Cloud.Linq;
using System.Runtime.Serialization;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    /// <summary>
    /// 指标过滤条件字段
    /// </summary>
    
    public class QueryFilter: QueryBase
    {
        private FilterOperator _operator = FilterOperator.IsLessThanOrEqualTo;
        /// <summary>
        /// 操作符
        /// </summary>
        
        public FilterOperator Operator
        {
            get { return _operator; }
            set
            {
                _operator = value;
            }
        }

        /// <summary>
        /// 值
        /// </summary>
        
        public string Value
        {
            set;
            get;
        }

        private FilterCompositionLogicalOperator _logic = FilterCompositionLogicalOperator.And;
        /// <summary>
        /// 逻辑运算
        /// </summary>
        
        public FilterCompositionLogicalOperator Logic
        {
            set
            {
                _logic = value;
            }
            get
            {
                return _logic;
            }
        }

        /// <summary>
        /// 分组ID
        /// </summary>
        
        public string GroupID
        {
            set; get;
        }

        /// <summary>
        /// 
        /// </summary>
        protected string _valueType;

        /// <summary>
        /// 值类型
        /// </summary>
        
        public string ValueType
        {
            get
            {
                if (_valueType == null && Value != null)
                    return Value.GetType().Name;
                return _valueType;
            }
            set { _valueType = value; }
        }

    }

}
