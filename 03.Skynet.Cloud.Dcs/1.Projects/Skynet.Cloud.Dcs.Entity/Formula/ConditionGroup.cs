using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Linq;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    /// <summary> 
    /// 描述：条件关系组
    /// </summary> 
    
    public class ConditionGroup
    {
        public string GroupID
        {
            set;
            get;
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ConditionGroup()
        {
            Logic = FilterCompositionLogicalOperator.And;
            ConditionFieldList = new List<QueryFilter>();
            ConditionGroupList = new List<ConditionGroup>();
        }

        private FilterCompositionLogicalOperator _relationship = FilterCompositionLogicalOperator.And;
        /// <summary>
        /// 与、或
        /// </summary>
        
        public FilterCompositionLogicalOperator Logic
        {
            get
            {
                return _relationship;
            }
            set
            {
                _relationship = value;
            }
        }

        private int _dataSource = 0;
        /// <summary>
        /// 数据源类型
        /// </summary>
        
        public int DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
            }
        }

        private List<QueryFilter> _conditionFieldList = new List<QueryFilter>();
        /// <summary>
        /// 条件列表
        /// </summary>
        
        public List<QueryFilter> ConditionFieldList
        {
            get
            {
                return _conditionFieldList;
            }
            set
            {
                _conditionFieldList = value;
            }
        }

        private List<ConditionGroup> _conditionGroupList = new List<ConditionGroup>();
        /// <summary>
        /// 子组列表
        /// </summary>
        
        public List<ConditionGroup> ConditionGroupList
        {
            get
            {
                return _conditionGroupList;
            }
            set
            {
                _conditionGroupList = value;
            }
        }

        
        public string GroupName
        {
            get;set;
        }

       
    }
}
