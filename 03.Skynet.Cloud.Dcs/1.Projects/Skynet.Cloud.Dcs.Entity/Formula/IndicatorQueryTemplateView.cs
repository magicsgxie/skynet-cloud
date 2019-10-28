using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;


namespace UWay.Skynet.Cloud.Dcs.Entity
{
    /// <summary>
    /// 性能查询模板
    /// </summary>
    
    public class IndicatorQueryTemplateView : IndicatorQueryTemplate
    {
      
        /// <summary>
        /// 树节点显示名称
        /// </summary>
        
        public int ShowName
        {
            set;
            get;
        }

        /// <summary>
        /// 用来控制保存按钮是否可用
        /// </summary>

        
        public bool IsShowTemplate
        {
            set;
            get;
        }



        /// <summary>
        /// 网元条件
        /// </summary>
        
        public NeQuery NeQuery
        {
            set;
            get;
        }

        /// <summary>
        /// 时间条件
        /// </summary>
        
        public TimerQuery TimerQuery
        {
            set;
            get;
        }

        private List<IndicatorQueryField> _queryFields = new List<IndicatorQueryField>();
        /// <summary>
        /// 显示字段
        /// </summary>
        
        public List<IndicatorQueryField> QueryFields
        {
            set
            {
                _queryFields = value;
            }
            get
            {
                return _queryFields;
            }
        }


        private List<IndicatorQueryField> _baseQueryFields = new List<IndicatorQueryField>();
        /// <summary>
        /// 基础数据显示字段
        /// </summary>
         
        public List<IndicatorQueryField> BaseQueryFields
        {
            set
            {
                _baseQueryFields = value;
            }
            get
            {
                return _baseQueryFields;
            }
        }

        private  List<ConditionGroup> _outFilterGroup = new List<ConditionGroup>();
        /// <summary>
        /// 外过滤字段、汇总后过滤字段
        /// </summary>
        
        public List<ConditionGroup> OutFilterGroup
        {
            get { return _outFilterGroup; }
            set
            {
                _outFilterGroup = value;
            }
        }

        private List<ConditionGroup> _filterGroup = new List<ConditionGroup>();
        /// <summary>
        /// 过滤字段
        /// </summary>
        
        public List<ConditionGroup> FilterGroup
        {
            get { return _filterGroup; }
        }

        private ConditionGroup _currentEditGroup = new ConditionGroup();
        /// <summary>
        /// 当前正在编辑的分组条件
        /// </summary>
        
        public ConditionGroup CurrentEditGroup
        {
            get { return _currentEditGroup; }
            set
            {
                _currentEditGroup = value;
            }
        }

        private ConditionGroup _selectedGroup = new ConditionGroup();
        /// <summary>
        /// 当前选中的条件分组
        /// </summary>
        
        public ConditionGroup SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                _selectedGroup = value;
            }
        }
        private bool _isReloadTemplate = false;
        /// <summary>
        /// 是否重新加载模板
        /// </summary>
        
        public bool IsReloadTemplate
        {
            get
            {
                return _isReloadTemplate;
            }
            set
            {
                _isReloadTemplate = value;
            }
        }
        private bool _isReloading = false;
        /// <summary>
        /// 是否正在加载中
        /// </summary>
        
        public bool IsReloading
        {
            get
            {
                return _isReloading;
            }
            set
            {
                _isReloading = value;
            }
        }

        private List<ColorLevelSetting> _colorLevelCfgInfos = new List<ColorLevelSetting>();

        /// <summary>
        /// 色阶设置
        /// </summary>
        
        public List<ColorLevelSetting> ColorLevelCfgInfos
        {
            get
            {
                return _colorLevelCfgInfos;
            }
            set
            {
                _colorLevelCfgInfos = value;
            }
        }

        /// <summary>
        /// 指标结构名称，与数据库无关
        /// </summary>
        
        public string IndexTreeName
        {
            set;get;
        }

        /// <summary>
        /// 指标结构名称，与数据库无关
        /// </summary>
        
        public string IndexTreeUIType
        {
            set;
            get;
        }

        

        /// <summary>
        /// 网元信息
        /// </summary>
        
        public string NeParameter
        {
            set;
            get;
        }

        
        public string NeNames
        {
            set;get;
        }
    }
}
