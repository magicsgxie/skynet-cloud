using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UWay.Skynet.Cloud;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Data.Render;
using UWay.Skynet.Cloud.Dcs.Common;
using UWay.Skynet.Cloud.Dcs.Entity;
using UWay.Skynet.Cloud.Dcs.Service.Interface;
using UWay.Skynet.Cloud.Upms.Entity;
using UWay.Skynet.Cloud.Discovery.Abstract;
using Steeltoe.Common.Discovery;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Net.Http;
using Skynet.Cloud.Noap;

namespace Skynet.Cloud.Dcs.Service
{
    public class UqlService : BaseDiscoveryService, IUqlService
    {
        //private readonly ILogger<UqlService> _logger;
        private readonly IBaseFormulaService _baseFormulaService;
        private readonly IInDicatorDictItemService _inDicatorCfgService;
        private readonly IFormulaCounterService _formulaCounterService;
        private readonly IEnumRelService _enumRelService;
        private readonly INeLevelRelService _neLevelRelService;
        private readonly IBusyService _busyService;
        private readonly ICfgDSService _cfgDSService;

        private const string CITY_URL = "http://upms/city/all";
        private const string NE_URL = "http:///nom/all";
        //private readonly INeLevelRelService _neLevelRelService;
        public UqlService(IDiscoveryClient client, ILogger<UqlService> logger, IBaseFormulaService baseFormulaService,
            IInDicatorDictItemService inDicatorCfgService,
            IFormulaCounterService formulaCounterService,
            IEnumRelService enumRelService,
            INeLevelRelService neLevelRelService,
            IBusyService busyService,
            ICfgDSService cfgDSService
            ):base(client, logger)
        {
            //_logger = logger;
            _baseFormulaService = baseFormulaService;
            _inDicatorCfgService = inDicatorCfgService;
            _formulaCounterService = formulaCounterService;
            _enumRelService = enumRelService;
            _neLevelRelService = neLevelRelService;
            _busyService = busyService;
            _cfgDSService = cfgDSService;
        }

        /// <summary>
        /// 日志记录文件目录
        /// </summary>
        protected string catgaory = "UqlIndicatorTool";

        /// <summary>
        ///SQL忙时表别名
        /// </summary>
        const string BUSY_TABLE_NAME = "busy";

        /// <summary>
        /// 忙时性能数据表忙时类型字段名
        /// </summary>
        const string BUSY_TYPE_COLNAME = "busy_type";
        /// <summary>
        /// SQL扩展表别名
        /// </summary>
        const string EXTENSION_TABLE_NAME = "extension";


        /// <summary>
        /// UserType
        /// </summary>
        protected int _useType = 0;

        /// <summary>
        /// 查询模版
        /// </summary>
        protected QueryTemplateView _template;

        /// <summary>
        /// 数据源类型
        /// </summary>
        protected int _dataSourceType = 0;

        /// <summary>
        /// 忙时查询配置
        /// </summary>
        protected BusyInfo _busyInfo = null;

        /// <summary>
        /// 当期查询包含的厂家
        /// </summary>
        protected IList<string> _vendorInfo;

        /// <summary>
        /// 网元粒度相关的信息
        /// </summary>
        protected NeLevelRelInfo _neRelationInfo;

        /// <summary>
        /// 时间聚合显示配置
        /// </summary>
        protected Enum_Relation _timeAggregationRelation = null;

        /// <summary>
        /// 网元聚合显示配置
        /// </summary>
        protected Enum_Relation _neAggregationRelation = null;

        /// <summary>
        /// 是否厂家聚合
        /// </summary>
        protected bool _bNeedVendorAggregation = false;

        /// <summary>
        /// 是否需要网元聚合
        /// </summary>
        protected bool _bNeedNeAggregation = false;

        /// <summary>
        /// 是否需要时间聚合
        /// </summary>
        protected bool _bNeedTimeAggregation = false;

        /// <summary>
        /// 数据时间粒度
        /// </summary>
        protected int _dataTimeGranularity = 0;

        /// <summary>
        /// Union 子查询指标公式
        /// </summary>
        protected IDictionary<string, BaseFormula> _subQueryFormulas = new Dictionary<string, BaseFormula>();

        /// <summary>
        /// 存储不同厂家不同指标，解析后的公式Key =VENDOR_ATT_ENNAME,Value=formula.PhraseStorage
        /// </summary>
        protected Dictionary<string, PhraseStorage> _dictVendorFormula = new Dictionary<string, PhraseStorage>();

        /// <summary>
        /// 存储不同厂家不同指标，解析后的公式Key =VENDOR_ATT_ENNAME,Value=formula.PhraseStorage
        /// </summary>
        protected Dictionary<string, PhraseStorage> _dictVendorFilterFormula = new Dictionary<string, PhraseStorage>();

        /// <summary>
        /// Key=表名，Value=索引
        /// </summary>
        protected Dictionary<string, int> _tableCollection = new Dictionary<string, int>();

        /// <summary>
        /// 异常消息
        /// </summary>
        protected StringBuilder _strErrMessage = new StringBuilder();

        private async Task<IList<int>> GetCityIdAsync()
        {
            
            var request = GetRequest(HttpMethod.Get, $"{CITY_URL}");
            return await Invoke<IList<int>>(request);

        }

        #region 0.输入输出入口
        public SelectQuery BuildSQL(UqlType uqlType, int useType, QueryTemplateView template, IDictionary<string, object> parameters, bool isUseOrderby = true)
        {
            var dataSource = _cfgDSService.GetByUqlType(uqlType);
            if(dataSource != null)
            {
                _dataSourceType = dataSource.SourceType;
            }
            template.OutFilterGroup.AddRange(template.FilterGroup);
            //template.FilterGroup = List<Entity.ConditionGroup>;
            template.FilterGroup.Clear();

            //初始化异常消息
            _strErrMessage.Clear();
            //计时器
            int tick = Environment.TickCount;
            
            _template = template;
            _useType = useType;

            //获取全部formula和counter数据

            //1.时间粒度和网元粒度转换，以及该粒度下关联的网元粒度信息,时间冗余，字段信息
            NeAndTimeGranularityChange(template);

            //LoggingManager.GetLogger(catgaory).Debug(string.Format("[性能查询SQL拼装]获取配置耗时：{0}", Environment.TickCount - tick));
            //重置计时器
            tick = Environment.TickCount;



            //2.获取厂家版本信息
            if (template.VendorVersion == DefaultData.PUBLIC_VENDOR_CODE)
            {
                GetVendorVersion(template.NeType.GetPerfNetType(), template);
            }
            else
            {
                if (_vendorInfo == null)
                    _vendorInfo = new List<string>();

                if (!_vendorInfo.Contains(template.VendorVersion))
                    _vendorInfo.Add(template.VendorVersion);
            }

            //检查聚合方式类型
            CheckAggregation(template);

            _logger.LogDebug(string.Format("[性能查询SQL拼装]获取厂家耗时：{0}", Environment.TickCount - tick));
            //重置计时器
            tick = Environment.TickCount;

            //3.公式解析,公式允许嵌套多层，利用递归的方式去解析 
            //Sql sqlQuery = new Sql();
            SelectQuery query = null;
            //WhereClause outWhereCluase = null;
            //拆分查询指标
            _subQueryFormulas = GetSubQueryFormulas(template);

            //拆分汇总后过滤指标，及生成过滤条件
            WhereClause outWhereClause = GetOutFilterWhereClause(template);
            //List<QueryParameter> outQueryParameterList = new List<QueryParameter>();
            //outQueryParameterList.Add(outQueryParameter);
            foreach (string vendorversion in _vendorInfo)
            {
                ParseFormula(template, vendorversion);
            }

            _logger.LogDebug(string.Format("[性能查询SQL拼装]拆分公式耗时：{0}", Environment.TickCount - tick));
            //重置计时器
            tick = Environment.TickCount;

            if (!_bNeedVendorAggregation)
            {
                query = BuildSQLByVendor(template, template.VendorVersion == "ZY0000" ? _vendorInfo[0] : template.VendorVersion);
                if (outWhereClause != null)
                {
                    SelectQuery tmpQuery = new SelectQuery();
                    tmpQuery.FromClause.BaseTable = FromTerm.SubQuery(query, "");
                    tmpQuery.Columns = GetOutQueryColumns(template);
                    tmpQuery.WherePhrase = outWhereClause;
                    query = tmpQuery;
                }
            }
            else
            {
                query = BuildSQLByMuliVendor(template);
                if (outWhereClause != null)
                {
                    query.HavingPhrase = outWhereClause;
                }
            }

            _logger.LogDebug(string.Format("[性能查询SQL拼装]组装SQL耗时：{0}", Environment.TickCount - tick));
            tick = Environment.TickCount;

            OrderByTermCollection orderbycollect = BuildOrderByTerm(template, isUseOrderby);

            if (orderbycollect.Count >= 1)
                query.OrderByTerms = orderbycollect;

            _logger.LogDebug(string.Format("[性能查询SQL拼装]组装ORDER BY SQL耗时：{0}", Environment.TickCount - tick));
            if (_strErrMessage.Length > 0)
            {//写异常日志
                _logger.LogError(_strErrMessage.ToString());
            }
            return query;
        }

        /// <summary>
        /// 检查聚合方式
        /// </summary>
        /// <param name="template"></param>
        private void CheckAggregation(QueryTemplateView template)
        {
            _bNeedVendorAggregation = template.VendorVersion == DefaultData.PUBLIC_VENDOR_CODE & _vendorInfo.Count > 1;
            var neType = template.NeType.GetPerfNetType();
            if (neType == NetType.GSM || neType == NetType.WCDMA)
            {
                if (template.VendorVersion == DefaultData.PUBLIC_VENDOR_CODE & _vendorInfo.Count > 1)
                {
                    _bNeedVendorAggregation = false;
                }
            }
            if (template.NeQuery.NeAggregation == 0 && template.NeLevel == _neRelationInfo.DataNeLevel)
                _bNeedNeAggregation = false;
            else
                _bNeedNeAggregation = true;

            if (template.TimerQuery.TimeAggregation == 0)
                _bNeedTimeAggregation = false;
            else
                _bNeedTimeAggregation = true;
        }

        #endregion

        #region 1.时间粒度和网元粒度转换
        public void NeAndTimeGranularityChange(QueryTemplateView template)
        {
            if (template.TimerQuery.IsBusy)
            {
                _busyInfo = GetBusyInfo(template);
            }

            _neRelationInfo = _neLevelRelService.GetNeLevelRelInfos().FirstOrDefault(a => a.QueryNeLevel % 100 == template.NeLevel && a.NeType == template.NeType);

            _dataTimeGranularity = template.TimerQuery.TimeGranularity;

            var timeItem = _inDicatorCfgService.GetInDicatorCfgs().Values.FirstOrDefault(a => a.DictType == "Time_Aggregation" && Convert.ToInt32(a.DictCode) == template.TimerQuery.TimeGranularity);

            if (timeItem != null)
                _dataTimeGranularity = Convert.ToInt32(timeItem.RelateInfo);

            //时间聚合显示字段配置
            _timeAggregationRelation = _enumRelService.GetEnumRels().FirstOrDefault(a => a.Source_Type == "TIME_LEVEL"
                            && a.Dict_Code == template.TimerQuery.TimeGranularity.ToString()
                            && a.Dest_Type == "TIME_AGGREGATION" && a.Dest_Value == template.TimerQuery.TimeAggregation.ToString()).DeepClone();

            //替换时间聚合配置中的忙时CounterGroupTimeField
            if (IsBusyCounterGroupData(template, _busyInfo))
            {
                _timeAggregationRelation.KeyField = _timeAggregationRelation.KeyField.Replace(_neRelationInfo.TimeFieldName, _busyInfo.Time_Field_Name);
                foreach (var item in _timeAggregationRelation.Redundance_Fields)
                {
                    item.DictCode = item.DictCode.Replace(_neRelationInfo.TimeFieldName, _busyInfo.Time_Field_Name);
                }
            }

            //获取显示的网元聚合字段
            switch (template.NeQuery.NeAggregation)
            {
                case 0:
                    _neAggregationRelation = _enumRelService.GetEnumRels().FirstOrDefault(a => a.Source_Type == "NE_LEVEL" &&
                         a.Dict_Code == template.NeType.ToString() + (template.NeLevel % 100).ToString("00") &&
                         a.Dest_Type == "NE_AGGREGATION" && a.Dest_Value == "0").DeepClone();

                    break;
                case -1:
                    _neAggregationRelation = _enumRelService.GetEnumRels().FirstOrDefault(a => a.Source_Type == "NE_LEVEL" &&
                        a.Dict_Code == template.NeType.ToString() + (template.NeLevel % 100).ToString("00") &&
                        a.Dest_Type == "NE_AGGREGATION" && a.Dest_Value == "-1").DeepClone();

                    break;
                default:
                    _neAggregationRelation = _enumRelService.GetEnumRels().FirstOrDefault(a => a.Source_Type == "NE_LEVEL" &&
                        a.Dict_Code == template.NeType.ToString() + (template.NeLevel % 100).ToString("00") &&
                        a.Dest_Type == "NE_AGGREGATION" &&
                        a.Dest_Value == template.NeType.ToString() + (template.NeQuery.NeAggregation % 100).ToString("00")).DeepClone();

                    break;
            }

            //获取合并显示的聚合字段关系
            if (template.NeQuery.RelationNeAggregation != 0 && template.NeQuery.NeAggregation != -1 && template.NeQuery.RelationNeAggregation != template.NeQuery.NeAggregation && template.NeQuery.RelationNeAggregation.ToString() != template.NeType.ToString() + (template.NeLevel % 100).ToString("00"))
            {
                _neAggregationRelation = _neAggregationRelation.DeepClone();
                var mergeNeAggregationRelation = _enumRelService.GetEnumRels().FirstOrDefault(a => a.Source_Type == "NE_LEVEL" &&
                                       a.Dict_Code == template.NeType.ToString() + (template.NeLevel % 100).ToString("00") &&
                                       a.Dest_Type == "NE_AGGREGATION" && a.Dest_Value == template.NeQuery.RelationNeAggregation.ToString());
                foreach (var item in mergeNeAggregationRelation.Redundance_Fields)
                {
                    var fs = _neAggregationRelation.Redundance_Fields.Where(t => t.DictCode == item.DictCode);
                    if (fs.Count() > 0)
                    {
                        _neAggregationRelation.Redundance_Fields.Remove(fs.FirstOrDefault());
                    }
                }
                _neAggregationRelation.Redundance_Fields.InsertRange(0, mergeNeAggregationRelation.Redundance_Fields);
                _neAggregationRelation.Hide_Fields.AddRange(mergeNeAggregationRelation.Hide_Fields);
                _neAggregationRelation.ExtensionFields = mergeNeAggregationRelation.ExtensionFields;
                _neAggregationRelation.ExtensionRelationFields = mergeNeAggregationRelation.ExtensionRelationFields;
                _neAggregationRelation.ExtensionTableView = mergeNeAggregationRelation.ExtensionTableView;
            }
        }

        #endregion

        #region 2.根据SQL语句，获取厂家版本
        protected void GetVendorVersion(NetType netType, QueryTemplateView template)
        {
            if (string.IsNullOrEmpty(template.NeQuery.NeCondition))
            {
                template.NeQuery.NeCondition = "1=1";
            }
            var task = GetVendorsAsync(netType,_neRelationInfo != null ? _neRelationInfo.BaseDataTableName : string.Empty,
                    _neAggregationRelation == null ? string.Empty : _neAggregationRelation.ExtensionTableView,
                    _neAggregationRelation == null ? string.Empty : _neAggregationRelation.Dest_Value,
                    template.NeQuery.NeCondition
                );
            task.Wait();
            _vendorInfo = task.Result;
            //_vendorInfo = PerfManager.GetVendorInfo(netType, _neRelationInfo, template.NeQuery.NeCondition, _neAggregationRelation);
        }

        private async Task<IList<string>> GetVendorsAsync(NetType netType, string baseT, string extendT, string destValue, string where)
        {
            
            var requestURL = string.Format("{0}/{1}?{2}extendT={3}&destvalue={4}&baseWhere={5}",
                                NE_URL,
                                netType,
                                extendT.IsNullOrEmpty() && !destValue.Equals("-1") ? "" : @"baseT =" + baseT + @"&",
                                extendT,
                                destValue,
                                where.ToBase64());


            var request = GetRequest(HttpMethod.Get, requestURL);
            return await Invoke<IList<string>>(request);

        }
        #endregion

        #region 3.1.公式拆分,公式允许嵌套多层，利用递归的方式去解析

        /// <summary>
        /// 创建外过滤后查询列
        /// </summary>
        /// <param name="Template"></param>
        /// <returns></returns>
        protected SelectColumnCollection GetOutQueryColumns(QueryTemplateView Template)
        {

            SelectColumnCollection ColumnCollection = new SelectColumnCollection();

            //添加时间冗余字段  
            if ((Template.TimerQuery.TimeAggregation != 0 || _vendorInfo.Count >= 1) &&
                Template.TimerQuery.TimeAggregation != -1)
                ColumnCollection.Add(new SelectColumn("TIME_KEY_FIELD"));
            foreach (DictItem item in _timeAggregationRelation.Redundance_Fields)
            {
                if (item.DictCode.ToUpper().IndexOf(@" AS ") >= 0)
                    ColumnCollection.Add(new SelectColumn(item.DictCode.Substring(item.DictCode.ToUpper().IndexOf(@" AS ") + 4).Trim()));
                else
                    ColumnCollection.Add(new SelectColumn(item.DictCode));
            }
            //添加网元冗余字段  
            if ((Template.NeQuery.NeAggregation != 0 || _vendorInfo.Count >= 1) &&
             Template.NeQuery.NeAggregation != -1)
                ColumnCollection.Add(new SelectColumn("NE_KEY_FIELD"));
            //建立冗余字段信息
            foreach (DictItem item in _neAggregationRelation.Redundance_Fields)
            {
                if (item.DictCode.ToUpper().IndexOf(@" AS ") >= 0)
                    ColumnCollection.Add(new SelectColumn(item.DictCode.Substring(item.DictCode.ToUpper().IndexOf(@" AS ") + 4).Trim()));
                else
                    ColumnCollection.Add(new SelectColumn(item.DictCode));
            }
            //建立其他公式表
            SelectColumn column = null;
            foreach (QueryField item in Template.QueryFields)
            {
                if (!item.Alias.IsNullOrEmpty())
                {
                    //重新组装成原有的公式
                    column = new SelectColumn(item.Alias);
                }
                else
                {
                    var formula = _baseFormulaService.GetById(item.FieldID);
                    //重新组装成原有的公式
                    column = new SelectColumn(formula.AttEnName);
                }


                ColumnCollection.Add(column);
            }

            return ColumnCollection;
        }

        /// <summary>
        /// 获取外过滤条件
        /// </summary>
        /// <returns></returns>
        protected WhereClause GetOutFilterWhereClause(QueryTemplateView template)
        {
            WhereClause clause = null;
            //添加指标条件
            foreach (var group in template.OutFilterGroup)
            {
                //foreach (ConditionGroup group in souceGroup.Value)
                //{
                if (clause == null)
                {
                    clause = new WhereClause();
                }
                WhereClause subclause = new WhereClause(group.Logic);
                //递归解析过滤字段
                RecursionBuildOutFilterFormula(subclause, group);

                clause.SubClauses.Add(subclause);
                //}
            }
            return clause;
        }

        /// <summary>
        /// 递归建立过滤条件
        /// </summary>
        /// <param name="group"></param>
        protected void RecursionBuildOutFilterFormula(WhereClause clause, ConditionGroup group, IDictionary<string, object> nameValues = null)
        {
            if (group.ConditionFieldList != null && group.ConditionFieldList.Count > 0)
            {
                foreach (QueryFilter filter in group.ConditionFieldList)
                {
                    var formauls = _baseFormulaService.GetById(filter.FieldID);
                    if (formauls != null)
                    {
                        if (_bNeedVendorAggregation)
                        {
                            AddSubFormula(_subQueryFormulas, formauls, _vendorInfo.Where(t => !t.Contains(DefaultData.PUBLIC_VENDOR_CODE)).FirstOrDefault());
                        }
                        else
                        {
                            if (!_subQueryFormulas.ContainsKey(formauls.AttCnName))
                            {
                                _subQueryFormulas[formauls.AttCnName] = formauls;
                            }
                        }

                        //拼装过滤条件 
                        double d = 0;
                        if (double.TryParse(filter.Value.ToString(), out d))
                        {
                            if (_bNeedVendorAggregation)
                            {
                                PhraseStorage sp = (_bNeedVendorAggregation ? formauls.Formula : formauls.AttCnName).SplitFormula();
                                decimal check = 0;
                                for (int i = 0; i < sp.PhraseResult.Count; i++)
                                {
                                    if (sp.PhraseTypeResult[i] != PhraseTypeEnum.number || decimal.TryParse(sp.PhraseResult[i].ToString(), out check))
                                    {
                                        continue;
                                    }
                                    sp.PhraseResult[i] = string.Format(@" {0}({1})", _subQueryFormulas[sp.PhraseResult[i].ToString().ToUpper()].VendorAggregation, _subQueryFormulas[sp.PhraseResult[i].ToString().ToUpper()].AttEnName);
                                }
                                clause.Terms.Add(
                                  WhereTerm.CreateCompare(filter.Logic, 
                                  SqlExpression.Raw(string.Format("Round({0},{1})", 
                                  FormulaUtils.PhraseStorageDecode(sp, sp.PhraseTypeResult.ToList(), formauls.IsZero), 
                                  formauls.DigitalDigit)),
                                  SqlExpression.Number(d), filter.Operator));
                            }
                            else
                            {
                                clause.Terms.Add(
                                        WhereTerm.CreateCompare(filter.Logic, SqlExpression.Raw(_subQueryFormulas[formauls.AttCnName].AttEnName),
                                        SqlExpression.Number(d), filter.Operator));
                            }

                        }
                        else
                        {
                            if (_bNeedVendorAggregation)
                            {
                                PhraseStorage sp = (_bNeedVendorAggregation ? formauls.Formula : formauls.AttCnName).SplitFormula();
                                decimal check = 0;
                                for (int i = 0; i < sp.PhraseResult.Count; i++)
                                {
                                    if (sp.PhraseTypeResult[i] != PhraseTypeEnum.number || decimal.TryParse(sp.PhraseResult[i].ToString(), out check))
                                    {
                                        continue;
                                    }
                                    sp.PhraseResult[i] = string.Format(@" {0}({1})", _subQueryFormulas[sp.PhraseResult[i].ToString().ToUpper()].VendorAggregation, _subQueryFormulas[sp.PhraseResult[i].ToString().ToUpper()].AttEnName);
                                }
                                clause.Terms.Add(WhereTerm.CreateCompare(filter.Logic,
                                                                        SqlExpression.Raw(string.Format("Round({0},{1})", FormulaUtils.PhraseStorageDecode(sp, sp.PhraseTypeResult.ToList(), formauls.IsZero), formauls.DigitalDigit)),
                                                                   SqlExpression.String(filter.Value.ToString()), filter.Operator));
                            }
                            else
                            {
                                clause.Terms.Add(
                                    WhereTerm.CreateCompare(filter.Logic, SqlExpression.Raw(_subQueryFormulas[formauls.AttCnName].AttEnName),
                                    SqlExpression.String(filter.Value.ToString()), filter.Operator));
                            }
                        }
                    }
                }
            }

            if (group.ConditionGroupList != null)
            {
                foreach (ConditionGroup subGroup in group.ConditionGroupList)
                    RecursionBuildOutFilterFormula(clause, subGroup);
            }
        }

        /// <summary>
        /// 分拆公式
        /// </summary>
        /// <param name="Template"></param>
        /// <param name="Vendor"></param>
        protected void ParseFormula(QueryTemplateView Template, string Vendor)
        {
            foreach (var formula in _subQueryFormulas.Values)
            {
                //获取Counter最原始公式
                GetCounterRawFormula(formula, Vendor);
            }
            foreach (var group in Template.FilterGroup)
            {
                //foreach (ConditionGroup group in souceGroup.Value)
                //{
                //递归解析过滤字段
                RecursionParseFilterFormula(group, Vendor);
                //}
            }
        }

        /// <summary>
        /// 获取分Union查询公式
        /// </summary>
        /// <returns></returns>
        protected IDictionary<string, BaseFormula> GetSubQueryFormulas(QueryTemplateView template)
        {
            IDictionary<string, BaseFormula> formulas = new Dictionary<string, BaseFormula>();

            if (_bNeedVendorAggregation)
            {
                var vendorVersion = _vendorInfo.Where(t => !t.Contains(DefaultData.PUBLIC_VENDOR_CODE)).FirstOrDefault();
                #region

                #endregion
                _baseFormulaService.GetFormulas(template.QueryFields.Select(t => t.FieldID)).ForEach(item =>
                {
                    AddSubFormula(formulas, item, vendorVersion);
                });

            }
            else
            {
                formulas = _baseFormulaService.GetFormulas(template.QueryFields.Select(t => t.FieldID)).ToDictionary(t => t.AttCnName);
            }

            return formulas;
        }

        /// <summary>
        /// 添加子公式到列表
        /// </summary>
        /// <param name="formulas">列表</param>
        /// <param name="formula">待拆分公式</param>
        /// <param name="vendorVersion"></param>
        protected void AddSubFormula(IDictionary<string, BaseFormula> formulas, BaseFormula formula, string vendorVersion)
        {
            PhraseStorage sp = formula.Formula.SplitFormula();
            int sub = 0;
            decimal check = 0;
            for (int i = 0; i < sp.PhraseResult.Count; i++)
            {
                if (sp.PhraseTypeResult[i] != PhraseTypeEnum.number || decimal.TryParse(sp.PhraseResult[i].ToString(), out check))
                {
                    continue;
                }
                sub++;
                BaseFormula subformula = formula.DeepClone();
                subformula.AttCnName = sp.PhraseResult[i].ToString().ToUpper();
                subformula.AttEnName = string.Format("{0}_{1}", formula.AttEnName, sub);
                subformula.Formula = sp.PhraseResult[i].ToString().ToUpper();
                var tmpformula = GetFormula(subformula.AttCnName, vendorVersion, subformula);
                if (tmpformula == null)
                {
                    _strErrMessage.AppendLine(string.Format("厂家：{0} 指标：{1} 级别：{2} 未找到！", vendorVersion, subformula.AttCnName, subformula.NeLevel));
                }
                else
                {
                    subformula.VendorAggregation = tmpformula.VendorAggregation;
                }
                if (!formulas.ContainsKey(subformula.AttCnName))
                {
                    formulas[subformula.AttCnName] = subformula;
                }
            }
        }

        /// <summary>
        /// 获取Counter最原始公式
        /// </summary>
        /// <param name="formula"></param>
        protected void GetCounterRawFormula(BaseFormula formula, string vendor, bool isFilterFormula = false)
        {
            PhraseStorage sp = new PhraseStorage();
            if (formula.Formula != null)
            {
                sp = formula.Formula.SplitFormula();
            }
            for (int i = 0; i < sp.PhraseResult.Count; i++)
            {
                if (sp.PhraseTypeResult[i] != PhraseTypeEnum.number || IsMath(sp.PhraseResult[i].ToUpper()))
                    continue;
                PhraseStorage subsp = null;
                bool Isformula = ParseFormulaRecursion(out subsp, sp.PhraseResult[i].ToUpper(), vendor, formula);

                if (Isformula)
                {
                    sp.SubPhraseStorage.Add(i, subsp);
                }

                //sp.PhraseResult[i] = ParseFormulaRecursion(sp.PhraseResult[i].ToUpper(), Vendor, formula);
            }
            //重新组装成原有的公式
            //NewFormula = BaseFormulaHelp.PhraseStorageDecode(sp, sp.PhraseTypeResult.ToList(), 0);
            var dict = isFilterFormula ? this._dictVendorFilterFormula : this._dictVendorFormula;
            if (!dict.ContainsKey(string.Format("{0}_{1}", vendor, formula.AttEnName)))
            {
                dict.Add(string.Format("{0}_{1}", vendor, formula.AttEnName), sp);
            }

        }

        /// <summary>
        /// 利用递归拆分成最原始Counter公式,最多递归20层
        /// </summary> 
        /// <param name="sp"></param>
        /// <param name="formula"></param>
        /// <param name="VendorVersion"></param>
        /// <param name="baseFormula"></param>
        /// <param name="tick"></param>
        /// <returns></returns>
        public bool ParseFormulaRecursion(out PhraseStorage sp, string formula, string VendorVersion, BaseFormula baseFormula, int tick = 0)
        {
            sp = formula.SplitFormula();

            tick++;
            if (tick > 20)
            {
                return false;
            }

            for (int i = 0; i < sp.PhraseResult.Count; i++)
            {
                if (sp.PhraseTypeResult[i] != PhraseTypeEnum.number || IsMath(sp.PhraseResult[i].ToString()))
                    continue;
                BaseFormula subformula = GetFormula(sp.PhraseResult[i].ToString(), VendorVersion, baseFormula);

                if (subformula == null)
                {
                    continue;
                }
                else
                {
                    PhraseStorage subsp = null;
                    if (ParseFormulaRecursion(out subsp, string.Format("({0})", subformula.Formula.ToUpper()), VendorVersion, baseFormula, tick))
                    {
                        sp.SubPhraseStorage.Add(i, subsp);
                    }
                }
            }

            return true;
            //重新组装成原有的公式
            //return BaseFormulaHelp.PhraseStorageDecode(sp, sp.PhraseTypeResult.ToList(), 0); 
        }

        /// <summary>
        /// 判断是否数值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsMath(string str)
        {
            decimal num;
            return decimal.TryParse(str, out num);
        }

        /// <summary>
        /// 获取公式
        /// </summary>
        /// <param name="attCNName"></param>
        /// <param name="vendorVersion"></param>
        /// <param name="baseFormula"></param>
        /// <returns></returns>
        protected BaseFormula GetFormula(string attCNName, string vendorVersion, BaseFormula baseFormula)
        {
            if (IsMath(attCNName))
            {
                return null;
            }
            attCNName = attCNName.ToUpper();

            BaseFormula subformula = null;
            if (IsMath(attCNName))
            {
                return null;
            }
            attCNName = attCNName.ToUpper();

            //string key = string.Format("{0}_{1}_{2}_{3}", _dataSourceType, baseFormula.NeType, vendorVersion, attCNName);
            var list = _baseFormulaService.GetSubByCondition(baseFormula.NeType, _dataSourceType, vendorVersion, attCNName);
            if (!list.Any())
            {
                return null;
            }
            //if (_inDicatorCfgService.GetById(_template.NeType, _template.NeLevel.ToString()) != null)
            if (HasNeLevelRelatedBusiness(_template.NeType, _template.NeLevel))
            {
                subformula = list.Where(a =>
                   ((a.UseType & _useType) == _useType) &&
                   ((a.BusinessType & _template.BusinessType) == _template.BusinessType) &&
                   ((a.NeCombination & (long)Math.Pow(2, _template.NeLevel - 1)) == (long)Math.Pow(2, _template.NeLevel - 1))).OrderBy(t => t.NeLevel).FirstOrDefault();
            }
            else
            {
                subformula = list.Where(a => ((a.UseType & _useType) == _useType) &&
                   ((a.NeCombination & (long)Math.Pow(2, _template.NeLevel - 1)) == (long)Math.Pow(2, _template.NeLevel - 1))).OrderBy(t => t.NeLevel).FirstOrDefault();
            }

            return subformula;
        }

        /// <summary>
        /// 递归解析过滤字段
        /// </summary>
        /// <param name="group"></param>
        protected void RecursionParseFilterFormula(ConditionGroup group, string Vendor)
        {
            if (group.ConditionFieldList != null && group.ConditionFieldList.Count > 0)
            {
                //foreach (QueryFilter filter in group.ConditionFieldList)
                //{
                //GetCounterRawFormula(filter.Formula, Vendor, true);
                //}
                _baseFormulaService.GetFormulas(group.ConditionFieldList.Select(C => C.FieldID)).ForEach((item) =>
                {
                    GetCounterRawFormula(item, Vendor, true);
                });
            }

            if (group.ConditionGroupList != null)
            {
                foreach (ConditionGroup subGroup in group.ConditionGroupList)
                    RecursionParseFilterFormula(subGroup, Vendor);
            }
        }
        #endregion

        #region 3.2.公式拆分,公式允许嵌套多层，利用递归的方式去解析
        /// <summary>
        /// 根据原始Counter获取原始公式
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="AggregationType"></param>
        /// <param name="Template"></param>
        /// <param name="baseformula"></param>
        /// <param name="vendor"></param>
        /// <returns></returns>
        protected string BuildForumlaByRawCounter(PhraseStorage sp, int AggregationType, QueryTemplateView Template, BaseFormula baseformula, string vendor)
        {
            for (int i = 0; i < sp.PhraseResult.Count; i++)
            {
                if (sp.PhraseTypeResult[i] != PhraseTypeEnum.number)
                    continue;
                if (sp.SubPhraseStorage.ContainsKey(i))
                {
                    PhraseStorage subsp = sp.SubPhraseStorage[i];

                    sp.PhraseResult[i] = BuildForumlaByRawCounter(subsp, AggregationType, Template, baseformula, vendor);

                }
                else
                {
                    FormulaCounter counter = ParseCounterContainTableName(sp.PhraseResult[i].ToUpper(), baseformula, vendor);

                    if (counter == null)
                        continue;
                    string FieldName = sp.PhraseResult[i];
                    if (FieldName.IndexOf("@") >= 0)
                        sp.PhraseResult[i] = counter.CltFieldName;

                    //使用指标聚合的时候，将聚合类型设置为0原因：需要将(sum（a）+sum(b))/sum(c)变成 sum((a+b)/c)
                    if (Template.AggregationWay == (int)AggregationWay.Formula)
                    {
                        AggregationType = 0;
                    }
                    switch (AggregationType)
                    {
                        case 1:
                            sp.PhraseResult[i] = string.Format(@" {0}({1})", counter.NeAggregation, sp.PhraseResult[i]);
                            break;
                        case 2:
                            sp.PhraseResult[i] = string.Format(@" {0}({1})", counter.TimeAggregation, sp.PhraseResult[i]);
                            break;
                        default://不变
                            break;

                    }
                }
            }

            return FormulaUtils.PhraseStorageDecode(sp, sp.PhraseTypeResult.ToList(), 0);
        }
        /// <summary>
        /// 递归获取原始Counter
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="counterList"></param>
        /// <param name="template"></param>
        /// <param name="baseFormula"></param>
        /// <param name="vendor"></param>
        /// <returns></returns>
        protected void GetForumlaByRawCounter(PhraseStorage sp, List<FormulaCounter> counterList, QueryTemplateView template, BaseFormula baseFormula, string vendor)
        {
            for (int i = 0; i < sp.PhraseResult.Count; i++)
            {
                if (sp.PhraseTypeResult[i] != PhraseTypeEnum.number)
                    continue;

                if (sp.SubPhraseStorage.ContainsKey(i))
                {
                    GetForumlaByRawCounter(sp.SubPhraseStorage[i], counterList, template, baseFormula, vendor);
                }
                else
                {
                    FormulaCounter counter = ParseCounterContainTableName(sp.PhraseResult[i].ToUpper(), baseFormula, vendor);
                    if (counter != null)
                    {
                        counterList.Add(counter);
                    }
                }
            }

        }


        #endregion

        #region 4.获取最原始表，组装Table列表[ Form( XXX) 里面内容]
        /// <summary>
        /// 获取忙时信息
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        protected BusyInfo GetBusyInfo(QueryTemplateView template)
        {
            BusyInfo busyInfo;
            if (HasNeLevelRelatedBusiness(_template.NeType, _template.NeLevel))
                busyInfo = _busyService.GetBusyInfos().FirstOrDefault(a => a.Ne_Type == template.NeType && a.Ne_Level == template.NeLevel && a.Business_Type == template.BusinessType && a.Busy_Type == template.TimerQuery.BusyType);
            else
                busyInfo = _busyService.GetBusyInfos().FirstOrDefault(a => a.Ne_Type == template.NeType && a.Ne_Level == template.NeLevel && a.Busy_Type == template.TimerQuery.BusyType);

            return busyInfo;
        }

        ///// <summary>
        ///// Key=表名，Value=索引
        ///// </summary>
        //protected Dictionary<string, int> TableCollection = new Dictionary<string, int>();

        ///// <summary>
        ///// 存储不同厂家不同指标，解析后的公式Key =VENDOR_ATT_ENNAME,Value=formula.PhraseStorage
        ///// </summary>
        //protected Dictionary<string, PhraseStorage> DictVendorFormula = new Dictionary<string, PhraseStorage>();

        /// <summary>
        /// 获取查询大表
        /// </summary>
        /// <param name="template"></param>
        /// <param name="vendor"></param>
        /// <returns></returns>
        protected FromTerm GetInterFormTerm(QueryTemplateView template, string vendor)
        {
            //获取表集合
            if (_tableCollection == null)
                _tableCollection = new Dictionary<string, int>();
            else
                _tableCollection.Clear();

            foreach (var formula in _subQueryFormulas.Values)
            {
                PhraseStorage sp = null;
                if (_dictVendorFormula.ContainsKey(string.Format(@"{0}_{1}", vendor, formula.AttEnName)))
                {
                    sp = _dictVendorFormula[string.Format(@"{0}_{1}", vendor, formula.AttEnName)];
                }
                if (sp == null)
                    continue;

                List<FormulaCounter> CounterList = new List<FormulaCounter>();
                GetForumlaByRawCounter(sp, CounterList, template, formula, vendor);
                if (CounterList.Count > 0)
                {
                    foreach (FormulaCounter counter in CounterList)
                    {
                        string TableName = GetTableNameByFormulaCounter(counter, template.TimerQuery.TimeGranularity);
                        if (!_tableCollection.ContainsKey(TableName))
                            _tableCollection.Add(TableName, _tableCollection.Count);
                    }
                }
                else
                {
                    continue;
                }
            }
            foreach (var group in template.FilterGroup)
            {
                //foreach (ConditionGroup group in souceGroup.Value)
                //{
                //递归获取表名
                RecursionParseFilterTableName(group, template, vendor);
                //}
            }

            string TableList = "";//数据表
            bool hasBusyGroup = false;//是否有BusyGroup表
            // 只有小时粒度才有忙时
            if (template.TimerQuery.TimeGranularity == 1 && _busyInfo != null)
            {
                switch (template.TimerQuery.BusyTimeStyle)
                {
                    case BusyTimeType.DayBusy:
                        hasBusyGroup = !string.IsNullOrWhiteSpace(_busyInfo.Busy_Group_Day_Busy_Type);
                        if (!hasBusyGroup)
                        {
                            TableList += string.Format(@"{0}  {1} ,", _busyInfo.Busy_Time_Day_Table_Name, BUSY_TABLE_NAME);
                        }
                        break;
                    case BusyTimeType.MorningBusy:
                        hasBusyGroup = !string.IsNullOrWhiteSpace(_busyInfo.Busy_Group_Morning_Busy_Type);
                        if (!hasBusyGroup)
                        {
                            TableList += string.Format(@"{0}  {1} ,", _busyInfo.Busy_Time_Morning_Table_Name, BUSY_TABLE_NAME);
                        }
                        break;
                    case BusyTimeType.EveningBusy:
                        hasBusyGroup = !string.IsNullOrWhiteSpace(_busyInfo.Busy_Group_Evening_Busy_Type);
                        if (!hasBusyGroup)
                        {
                            TableList += string.Format(@"{0}  {1} ,", _busyInfo.Busy_Time_Evening_Table_Name, BUSY_TABLE_NAME);
                        }
                        break;
                }
            }
            string tableFormat = hasBusyGroup ? @"{0}B  tb{1}," : @"{0}  tb{1},";
            foreach (KeyValuePair<string, int> kvPair in _tableCollection)
            {
                TableList += string.Format(tableFormat, kvPair.Key, kvPair.Value);
            }

            if (TableList.Length >= 1)
                TableList = TableList.Substring(0, TableList.Length - 1);

            if (!string.IsNullOrWhiteSpace(_neAggregationRelation.ExtensionTableView))
            {
                TableList = string.Format("{0},({1}) {2} ", TableList, _neAggregationRelation.ExtensionTableView, EXTENSION_TABLE_NAME);
            }
            return FromTerm.Table(TableList);
        }

        /// <summary>
        /// 解析公式表里面存在表明信息，如Counter@Tablename
        /// </summary>
        protected FormulaCounter ParseCounterContainTableName(string result, BaseFormula formula, string vendor)
        {
            if (IsMath(result))
            {
                return null;
            }
            FormulaCounter counter = null;

            if (result.IndexOf("@") >= 0)
            {
                if (HasNeLevelRelatedBusiness(_template.NeType, _template.NeLevel))
                {
                    counter = _formulaCounterService.GetFormulaCounter().Where(a => a.DataSource == this._dataSourceType 
                    && a.NeType == _template.NeType 
                    && a.NeLevel >= formula.NeLevel
                    && (a.BusinessType & _template.BusinessType) == _template.BusinessType 
                    && a.VendorVersion == vendor && a.FieldName == result.Split('@')[0] 
                    && a.CltTableName == result.Split('@')[1]).OrderBy(o => o.NeLevel).FirstOrDefault();
                }
                else
                {
                    counter = _formulaCounterService.GetFormulaCounter().Where(a => a.DataSource == this._dataSourceType && a.NeType == _template.NeType && a.NeLevel >= formula.NeLevel && a.VendorVersion == vendor && a.FieldName == result.Split('@')[0] && a.CltTableName == result.Split('@')[1]).OrderBy(o => o.NeLevel).FirstOrDefault();
                }
            }
            else
            {
                var counts = _formulaCounterService.GetFormulaCounterBy(_template.NeType, _dataSourceType, vendor, result);
                
                if (counts.Any())
                {
                    if (HasNeLevelRelatedBusiness(_template.NeType, _template.NeLevel))
                    {
                        counter = counts.Where(a => a.NeLevel >= formula.NeLevel && (a.BusinessType & _template.BusinessType) == _template.BusinessType && (a.FieldName == result || a.CltFieldName == result) && a.DataSource == _dataSourceType && a.NeType == _template.NeType && a.VendorVersion == vendor).OrderBy(o => o.NeLevel).FirstOrDefault();
                    }
                    else
                    {
                        counter = counts.Where(a => a.NeLevel >= formula.NeLevel && (a.FieldName == result || a.CltFieldName == result) && a.DataSource == _dataSourceType && a.NeType == _template.NeType && a.VendorVersion == vendor).OrderBy(o => o.NeLevel).FirstOrDefault();
                    }
                }


            }
            if (counter == null)
            {
                _strErrMessage.AppendLine(string.Format("厂家：{0} 指标或Counter：{1} 级别：{2} 未找到！", vendor, result, formula.NeLevel));
            }
            return counter;
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <param name="counter">Counter</param>
        /// <param name="timeLevel">时间粒度</param>
        /// <returns></returns>
        protected string GetTableNameByFormulaCounter(FormulaCounter counter, int timeLevel)
        {
            string TableName = "";
            if (counter.TableName.ContainsKey(string.Format(@"{0}_{1}", _neRelationInfo.NeType, _neRelationInfo.DataNeLevel)))
                TableName = counter.TableName[string.Format(@"{0}_{1}", _neRelationInfo.NeType, _neRelationInfo.DataNeLevel)];

            DictItem item = _inDicatorCfgService.GetInDicatorCfgs().Values.FirstOrDefault(a => a.DictType == "Time_LEVEL" && a.DictCode == timeLevel.ToString());
            string remark = item.Remark;
            if (item != null)
            {
                if (_template.NeLevel == (int)NeLevel.SUBITEM)
                {
                    switch ((TimeGranularity)timeLevel)
                    {
                        case TimeGranularity.Hour:
                            remark = TableName.ToUpper().EndsWith("_W") || TableName.ToUpper().EndsWith("_G") ? remark + "_H" : remark;
                            break;
                        case TimeGranularity.Day:
                            remark = TableName.ToUpper().EndsWith("_W") || TableName.ToUpper().EndsWith("_G") ? remark.Replace("D", "_D") : remark;
                            break;
                        case TimeGranularity.Week:
                            remark = TableName.ToUpper().EndsWith("_W") || TableName.ToUpper().EndsWith("_G") ? remark.Replace("D", "_D") : remark;
                            break;
                        case TimeGranularity.Month:
                            remark = TableName.ToUpper().EndsWith("_W") || TableName.ToUpper().EndsWith("_G") ? remark.Replace("M", "_M") : remark;
                            break;
                        default:
                            break;
                    }
                }
                else
                {

                    switch ((TimeGranularity)timeLevel)
                    {
                        case TimeGranularity.Hour:
                            remark = TableName.ToUpper().EndsWith("_W") || TableName.ToUpper().EndsWith("_G") ? remark + "_H" : remark;
                            break;
                        case TimeGranularity.Day:
                            remark = TableName.ToUpper().EndsWith("_G") ? remark.Replace("D", "_DAY") : TableName.ToUpper().EndsWith("_W") ? remark.Replace("D", "_D") : remark;
                            break;
                        case TimeGranularity.Week:
                            remark = TableName.ToUpper().EndsWith("_G") ? remark.Replace("D", "_W") : TableName.ToUpper().EndsWith("_W") ? remark.Replace("D", "_W") : remark;
                            break;
                        case TimeGranularity.Month:
                            remark = TableName.ToUpper().EndsWith("_G") ? remark.Replace("M", "_MONTH") : TableName.ToUpper().EndsWith("_W") ? remark.Replace("M", "_M") : remark;
                            break;
                        default:
                            break;
                    }
                }
                //支持格式化
                TableName = string.Format(remark, TableName);
                //支持替换
                while (item.Remark.LastIndexOf("Replace(") >= 0)
                {
                    string RepalceString = item.Remark.Substring(item.Remark.LastIndexOf("Replace(") + 8).Replace(")", "").Replace("\"", "");

                    if (RepalceString.Split(',').Length == 2)
                        TableName = TableName.Replace(RepalceString[0], RepalceString[1]);
                    item.Remark = item.Remark.Substring(0, item.Remark.LastIndexOf("Replace("));
                }

            }
            return TableName;
        }

        /// <summary>
        /// 递归获取表结合
        /// </summary>
        /// <param name="group">条件分组</param>
        /// <param name="template">模板</param>
        /// <param name="vendor">厂家</param>
        protected void RecursionParseFilterTableName(ConditionGroup group, QueryTemplateView template, string vendor)
        {
            if (group.ConditionFieldList != null && group.ConditionFieldList.Count > 0)
            {
                #region
                //foreach (QueryFilter filter in group.ConditionFieldList)
                //{
                //    //var formula = 
                //    PhraseStorage sp = null;
                //    if (_dictVendorFormula.ContainsKey(string.Format(@"{0}_{1}", vendor, filter.Formula.AttEnName)))
                //    {
                //        sp = _dictVendorFormula[string.Format(@"{0}_{1}", vendor, filter.Formula.AttEnName)];
                //    }
                //    if (sp == null)
                //        continue;
                //    List<FormulaCounter> CounterList = new List<FormulaCounter>();
                //    GetForumlaByRawCounter(sp, CounterList, template, filter.Formula, vendor);

                //    foreach (FormulaCounter counter in CounterList)
                //    {
                //        string TableName = GetTableNameByFormulaCounter(counter, template.TimerQuery.TimeGranularity);
                //        if (!_tableCollection.ContainsKey(TableName))
                //            _tableCollection.Add(TableName, _tableCollection.Count);
                //    }
                //}
                #endregion
                var list = _baseFormulaService.GetFormulas(group.ConditionFieldList.Select(p => p.FieldID));
                if (list.Count() > 0)
                {
                    list.ForEach(item =>
                    {
                        PhraseStorage sp = null;
                        if (_dictVendorFormula.ContainsKey(string.Format(@"{0}_{1}", vendor, item.AttEnName)))
                        {
                            sp = _dictVendorFormula[string.Format(@"{0}_{1}", vendor, item.AttEnName)];
                        }
                        if (sp != null)
                        {

                            List<FormulaCounter> CounterList = new List<FormulaCounter>();
                            GetForumlaByRawCounter(sp, CounterList, template, item, vendor);

                            foreach (FormulaCounter counter in CounterList)
                            {
                                string TableName = GetTableNameByFormulaCounter(counter, template.TimerQuery.TimeGranularity);
                                if (!_tableCollection.ContainsKey(TableName))
                                    _tableCollection.Add(TableName, _tableCollection.Count);
                            }
                        }

                    });
                }
            }

            if (group.ConditionGroupList != null)
            {
                foreach (ConditionGroup subGroup in group.ConditionGroupList)
                    RecursionParseFilterTableName(subGroup, template, vendor);
            }
        }
        #endregion
       

        public string BuildSQL(UqlType uqlType,  int userType, QueryTemplateView template, ISqlOmRenderer render, bool isUseOrderby = true)
        {
            throw new NotImplementedException();
        }



        #region 5.组装SelectColumn 获取字段列表

        /// <summary>
        /// 创建冗余字段
        /// </summary>
        /// <param name="ColumnCollection">查询列</param>
        /// <param name="Template">模板</param>
        protected void Build_Redundance_Fields(SelectColumnCollection ColumnCollection, QueryTemplateView Template)
        {
            int tableIndex = 0;
            //建立冗余字段信息
            if (_tableCollection != null && _tableCollection.Count >= 1)
                tableIndex = _tableCollection.ElementAt(0).Value;
            if (_tableCollection.Count > 1)
            {
                string use_hash = "/*+ no_expand use_hash(";

                for (int i = 0; i < _tableCollection.Count; i++)
                {
                    use_hash += "tb" + i + ",";
                }
                use_hash = use_hash.TrimEnd(',');
                use_hash += ")*/";

                ColumnCollection.Add(new SelectColumn(use_hash));
            }

            //添加时间冗余字段  
            //添加时间主键字段
            if (_timeAggregationRelation != null && !string.IsNullOrEmpty(_timeAggregationRelation.KeyField))
            {
                if (_timeAggregationRelation.KeyField.IndexOf("{0}.") >= 0)
                    ColumnCollection.Add(new SelectColumn(string.Format(_timeAggregationRelation.KeyField, "tb" + tableIndex.ToString()) + "  as TIME_KEY_FIELD"));
                else
                    ColumnCollection.Add(new SelectColumn(string.Format("tb{0}.{1} as TIME_KEY_FIELD", tableIndex, _timeAggregationRelation.KeyField)));
            }
            //添加时间其他冗余字段
            foreach (DictItem item in _timeAggregationRelation.Redundance_Fields)
            {
                if (item.DictCode.IndexOf("{0}.") >= 0)
                {
                    ColumnCollection.Add(new SelectColumn(string.Format(item.DictCode, "tb" + tableIndex.ToString())));
                }
                else
                {
                    ColumnCollection.Add(new SelectColumn(string.Format("tb{0}.{1}", tableIndex, item.DictCode)));
                }
            }
            //添加网元冗余字段 
            //添加网元主键字段
            if (_neAggregationRelation != null && !string.IsNullOrEmpty(_neAggregationRelation.KeyField))
            {
                if (_neAggregationRelation.KeyField.IndexOf("{0}.") >= 0)
                    ColumnCollection.Add(new SelectColumn(string.Format(_neAggregationRelation.KeyField, "tb" + tableIndex.ToString()) + "  as NE_KEY_FIELD"));
                else
                {
                    if (_neAggregationRelation.ExtensionFields.Contains(_neAggregationRelation.KeyField) && !string.IsNullOrEmpty(_neAggregationRelation.ExtensionTableView)) //如果是扩展字段
                        ColumnCollection.Add(new SelectColumn(string.Format("{1}.{0} as NE_KEY_FIELD", _neAggregationRelation.KeyField, EXTENSION_TABLE_NAME)));
                    else
                        ColumnCollection.Add(new SelectColumn(string.Format("tb{0}.{1} as NE_KEY_FIELD", tableIndex, _neAggregationRelation.KeyField)));
                }
            }

            //网元字段
            foreach (DictItem item in _neAggregationRelation.Redundance_Fields)
            {
                if (item.DictCode.IndexOf("{0}.") >= 0)
                    ColumnCollection.Add(new SelectColumn(string.Format(item.DictCode, "tb" + tableIndex.ToString())));
                else
                {
                    if (_neAggregationRelation.ExtensionFields.Contains(item.DictCode)) //如果是扩展字段
                    {
                        if (!string.IsNullOrEmpty(_neAggregationRelation.ExtensionTableView))
                        {
                            ColumnCollection.Add(new SelectColumn(string.Format("extension.{0}", item.DictCode)));
                        }
                        else
                        {
                            if (!_bNeedNeAggregation && !_bNeedTimeAggregation)
                            {
                                ColumnCollection.Add(new SelectColumn(string.Format("tb{0}.{1}", tableIndex, item.DictCode)));
                            }
                            else
                            {
                                ColumnCollection.Add(new SelectColumn(string.Format(" max(tb{0}.{1}) {1}", tableIndex, item.DictCode)));
                            }
                        }

                    }
                    else
                    {
                        ColumnCollection.Add(new SelectColumn(string.Format("tb{0}.{1}", tableIndex, item.DictCode)));
                    }
                }
            }
        }
        /// <summary>
        /// 创建公式没有聚合的列
        /// </summary>
        /// <param name="Template"></param>
        /// <param name="bNeLevel"></param>
        /// <returns></returns>
        protected SelectColumnCollection BuildSelectColumn_Formula_NotExistAggregation(QueryTemplateView template, string vendor)
        {
            SelectColumnCollection ColumnCollection = new SelectColumnCollection();
            Build_Redundance_Fields(ColumnCollection, template);

            //建立其他公式表
            SelectColumn column = null;
            foreach (var item in _subQueryFormulas.Values)
            {
                PhraseStorage sp = null;
                if (_dictVendorFormula.ContainsKey(string.Format(@"{0}_{1}", vendor, item.AttEnName)))
                {
                    sp = _dictVendorFormula[string.Format(@"{0}_{1}", vendor, item.AttEnName)];
                }
                if (sp == null)
                    continue;
                for (int i = 0; i < sp.PhraseResult.Count; i++)
                {
                    if (sp.PhraseTypeResult[i] != PhraseTypeEnum.number)
                        continue;
                    if (sp.SubPhraseStorage.ContainsKey(i))
                    {
                        sp.PhraseResult[i] = BuildForumlaByRawCounter(sp.SubPhraseStorage[i], 0, template, item, vendor);
                    }
                }
                //重新组装成原有的公式
                column = new SelectColumn(
                             SqlExpression.Raw(string.Format("Round({0},{1})", FormulaUtils.PhraseStorageDecode(sp, sp.PhraseTypeResult.ToList(), item.IsZero),
                            item.DigitalDigit)), item.AttEnName);
                ColumnCollection.Add(column);
            }

            return ColumnCollection;
        }
        /// <summary>
        /// 创建公式有聚合的列
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="isNeLevel">是否用网元聚合</param>
        /// <param name="vendor">厂家</param>
        /// <returns></returns>
        protected SelectColumnCollection BuildSelectColumn_Formula_ExistAggregation(QueryTemplateView template, bool isNeLevel, string vendor)
        {
            SelectColumnCollection ColumnCollection = new SelectColumnCollection();

            //建立冗余字段信息
            Build_Redundance_Fields(ColumnCollection, template);

            //建立其他公式表
            SelectColumn column = null;
            foreach (var item in _subQueryFormulas.Values)
            {
                PhraseStorage sp = null;
                if (_dictVendorFormula.ContainsKey(string.Format(@"{0}_{1}", vendor, item.AttEnName)))
                {
                    sp = _dictVendorFormula[string.Format(@"{0}_{1}", vendor, item.AttEnName)];
                }
                if (sp == null)
                    continue;

                for (int i = 0; i < sp.PhraseResult.Count; i++)
                {
                    if (sp.PhraseTypeResult[i] != PhraseTypeEnum.number)
                        continue;
                    if (sp.SubPhraseStorage.ContainsKey(i))
                    {
                        if (isNeLevel)
                            sp.PhraseResult[i] = BuildForumlaByRawCounter(sp.SubPhraseStorage[i], 1, template, item, vendor);
                        else
                            sp.PhraseResult[i] = BuildForumlaByRawCounter(sp.SubPhraseStorage[i], 2, template, item, vendor);
                    }
                    else
                    {
                        string result = sp.PhraseResult[i];
                        FormulaCounter counter = ParseCounterContainTableName(result.ToUpper(), item, vendor);

                        if (counter == null)
                            continue;

                        string FieldName = sp.PhraseResult[i];
                        if (FieldName.IndexOf("@") >= 0)
                            sp.PhraseResult[i] = counter.CltFieldName;

                        if (template.AggregationWay != (int)AggregationWay.Formula)
                        {
                            if (isNeLevel)
                                sp.PhraseResult[i] = string.Format(@" {0}({1})", counter.NeAggregation, sp.PhraseResult[i]);
                            else
                                sp.PhraseResult[i] = string.Format(@" {0}({1})", counter.TimeAggregation, sp.PhraseResult[i]);
                        }
                    }
                }
                string roundValue = FormulaUtils.PhraseStorageDecode(sp, sp.PhraseTypeResult.ToList(), item.IsZero);
                if (string.IsNullOrEmpty(roundValue))
                {
                    roundValue = "0";
                }
                //重新组装成原有的公式
                if (template.AggregationWay == (int)AggregationWay.Formula)
                {
                    column = new SelectColumn(
                                 SqlExpression.Raw(string.Format("Round({0}({1}),{2})", isNeLevel ? item.NeAggregation : item.TimeAggregation, roundValue,
                                item.DigitalDigit)), item.AttEnName);
                }
                else
                {
                    column = new SelectColumn(
                                 SqlExpression.Raw(string.Format("Round({0},{1})", roundValue,
                                item.DigitalDigit)), item.AttEnName);
                }
                ColumnCollection.Add(column);
            }

            return ColumnCollection;
        }

        /// <summary>
        /// 建立中间层的时间聚合
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="vendor">厂家</param>
        /// <returns></returns>
        protected SelectColumnCollection BuildSelectColumn_MiddleSite_Counter_TimeAggregation(QueryTemplateView template, string vendor)
        {
            SelectColumnCollection ColumnCollection = new SelectColumnCollection();

            //建立冗余字段信息
            //添加时间冗余字段  
            //添加时间主键字段
            if (_timeAggregationRelation != null && !string.IsNullOrEmpty(_timeAggregationRelation.KeyField))
            {
                ColumnCollection.Add(new SelectColumn(string.Format("TIME_KEY_FIELD")));
            }
            foreach (DictItem item in _timeAggregationRelation.Redundance_Fields)
            {
                if (item.DictCode.ToUpper().IndexOf(@" AS ") >= 0)
                    ColumnCollection.Add(new SelectColumn(item.DictCode.Substring(item.DictCode.ToUpper().IndexOf(@" AS ") + 4).Trim()));
                else
                    ColumnCollection.Add(new SelectColumn(item.DictCode));
            }
            //添加网元冗余字段  
            if (_neAggregationRelation != null && !string.IsNullOrEmpty(_neAggregationRelation.KeyField))
            {
                ColumnCollection.Add(new SelectColumn(string.Format("NE_KEY_FIELD")));
            }
            //建立冗余字段信息
            foreach (DictItem item in _neAggregationRelation.Redundance_Fields)
            {
                if (item.DictCode.ToUpper().IndexOf(@" AS ") >= 0)
                    ColumnCollection.Add(new SelectColumn(item.DictCode.Substring(item.DictCode.ToUpper().IndexOf(@" AS ") + 4).Trim()));
                else
                {
                    if (_neAggregationRelation.ExtensionFields.Contains(item.DictCode))
                    {
                        ColumnCollection.Add(new SelectColumn(string.Format(" max({0}) {0}", item.DictCode)));
                    }
                    else
                        ColumnCollection.Add(new SelectColumn(item.DictCode));
                }
            }

            //建立其他公式表
            SelectColumn column = null;
            foreach (var item in _subQueryFormulas.Values)
            {
                PhraseStorage sp = null;
                if (_dictVendorFormula.ContainsKey(string.Format(@"{0}_{1}", vendor, item.AttEnName)))
                {
                    sp = _dictVendorFormula[string.Format(@"{0}_{1}", vendor, item.AttEnName)];
                }
                if (sp == null)
                    continue;

                for (int i = 0; i < sp.PhraseResult.Count; i++)
                {
                    if (sp.PhraseTypeResult[i] != PhraseTypeEnum.number)
                        continue;
                    if (sp.SubPhraseStorage.ContainsKey(i))
                    {
                        sp.PhraseResult[i] = BuildForumlaByRawCounter(sp.SubPhraseStorage[i], 2, template, item, vendor);
                    }
                    else
                    {
                        string result = sp.PhraseResult[i];
                        FormulaCounter counter = ParseCounterContainTableName(result.ToUpper(), item, vendor);

                        if (counter == null)
                            continue;

                        string FieldName = sp.PhraseResult[i];
                        if (FieldName.IndexOf("@") >= 0)
                            sp.PhraseResult[i] = counter.CltFieldName;
                        if (template.AggregationWay != (int)AggregationWay.Formula)
                        {
                            sp.PhraseResult[i] = string.Format(@" {0}({1})", counter.TimeAggregation, sp.PhraseResult[i]);
                        }
                    }
                }

                //重新组装成原有的公式
                if (template.AggregationWay == (int)AggregationWay.Formula)
                {
                    column = new SelectColumn(
                                 SqlExpression.Raw(string.Format("Round({0}({1}),{2})", item.TimeAggregation, FormulaUtils.PhraseStorageDecode(sp, sp.PhraseTypeResult.ToList(), item.IsZero),
                                item.DigitalDigit)), item.AttEnName);
                }
                else
                {
                    column = new SelectColumn(
                                 SqlExpression.Raw(string.Format("Round({0},{1})", FormulaUtils.PhraseStorageDecode(sp, sp.PhraseTypeResult.ToList(), item.IsZero),
                                item.DigitalDigit)), item.AttEnName);
                }
                ColumnCollection.Add(column);
            }

            return ColumnCollection;
        }
        /// <summary>
        /// 创建内部Counter网元聚合的列
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="vendor">厂家</param>
        /// <returns></returns>
        protected SelectColumnCollection BuildSelectColumn_InSite_Counter_NeAggregation(QueryTemplateView template, string vendor)
        {
            SelectColumnCollection ColumnCollection = new SelectColumnCollection();

            //建立冗余字段信息
            Build_Redundance_Fields(ColumnCollection, template);

            //建立其他公式表
            SelectColumn column = null;

            //Counter避免重复
            List<string> ParseColumn = new List<string>();

            foreach (var item in _subQueryFormulas.Values)
            {
                PhraseStorage sp = null;
                if (_dictVendorFormula.ContainsKey(string.Format(@"{0}_{1}", vendor, item.AttEnName)))
                {
                    sp = _dictVendorFormula[string.Format(@"{0}_{1}", vendor, item.AttEnName)];
                }
                if (sp == null)
                    continue;

                List<FormulaCounter> CounterList = new List<FormulaCounter>();
                GetForumlaByRawCounter(sp, CounterList, template, item, vendor);

                string NewFormula = "";
                foreach (FormulaCounter counter in CounterList)
                {
                    if (ParseColumn.Contains(counter.CltFieldName.ToUpper()))
                        continue;
                    ParseColumn.Add(counter.CltFieldName.ToUpper());//避免增加重复Counter列
                    if (template.AggregationWay == (int)AggregationWay.Formula)
                    {
                        NewFormula = string.Format(@" {0}({1}) as {1}", item.NeAggregation, counter.CltFieldName.ToUpper());
                    }
                    else
                    {
                        NewFormula = string.Format(@" {0}({1}) as {1}", counter.NeAggregation, counter.CltFieldName.ToUpper());
                    }


                    //重新组装成原有的公式
                    column = new SelectColumn(SqlExpression.Raw(NewFormula), "");
                    ColumnCollection.Add(column);
                }
            }

            return ColumnCollection;
        }

        /// <summary>
        /// 创建外部OutSide,Att_EnName不聚合列
        /// </summary>
        /// <param name="template">模板</param> 
        /// <returns></returns>
        protected SelectColumnCollection BuildSelectColumn_OutSite_Att_EnName_Aggregation(QueryTemplateView template)
        {
            SelectColumnCollection ColumnCollection = new SelectColumnCollection();

            //添加时间冗余字段  
            if ((template.TimerQuery.TimeAggregation != 0 || _vendorInfo.Count > 1) &&
                template.TimerQuery.TimeAggregation != -1)
                ColumnCollection.Add(new SelectColumn("TIME_KEY_FIELD"));
            foreach (DictItem item in _timeAggregationRelation.Redundance_Fields)
            {
                if (item.DictCode.ToUpper().IndexOf(@" AS ") >= 0)
                    ColumnCollection.Add(new SelectColumn(item.DictCode.Substring(item.DictCode.ToUpper().IndexOf(@" AS ") + 4).Trim()));
                else
                    ColumnCollection.Add(new SelectColumn(item.DictCode));
            }
            //添加网元冗余字段  
            if ((template.NeQuery.NeAggregation != 0 || _vendorInfo.Count > 1) &&
             template.NeQuery.NeAggregation != -1)
                ColumnCollection.Add(new SelectColumn("NE_KEY_FIELD"));
            //建立冗余字段信息
            foreach (DictItem item in _neAggregationRelation.Redundance_Fields)
            {
                if (item.DictCode.ToUpper().IndexOf(@" AS ") >= 0)
                    ColumnCollection.Add(new SelectColumn(item.DictCode.Substring(item.DictCode.ToUpper().IndexOf(@" AS ") + 4).Trim()));
                else
                {
                    if (_neAggregationRelation.ExtensionFields.Contains(item.DictCode)) //如果是扩展字段
                    {

                        ColumnCollection.Add(new SelectColumn(string.Format(" max({0}) {0}", item.DictCode)));
                    }
                    else
                    {
                        ColumnCollection.Add(new SelectColumn(item.DictCode));
                    }
                }
            }
            //建立其他公式表
            SelectColumn column = null;
            //var list = BaseFormulaManager.GetFormulas(template.QueryFields.Select(q => q.FieldID));
            template.QueryFields.ForEach(item =>
            {
                var formula = _baseFormulaService.GetById(item.FieldID);
                //厂家聚合
                PhraseStorage sp = FormulaUtils.SplitFormula(formula.Formula);
                decimal check = 0;
                for (int i = 0; i < sp.PhraseResult.Count; i++)
                {
                    if (sp.PhraseTypeResult[i] != PhraseTypeEnum.number || decimal.TryParse(sp.PhraseResult[i].ToString(), out check))
                    {
                        continue;
                    }
                    //厂家聚合
                    sp.PhraseResult[i] = string.Format(@" {0}({1})", _subQueryFormulas[sp.PhraseResult[i].ToString().ToUpper()].VendorAggregation, _subQueryFormulas[sp.PhraseResult[i].ToString().ToUpper()].AttEnName);
                }
                var alias = item.Alias;
                if (alias.IsNullOrEmpty())
                {
                    alias = formula.AttEnName;
                }
                //重新组装成原有的公式
                column = new SelectColumn(
                            SqlExpression.Raw(string.Format("Round({0},{1})", FormulaUtils.PhraseStorageDecode(sp, sp.PhraseTypeResult.ToList(), formula.IsZero),
                           formula.DigitalDigit)), alias);

                ColumnCollection.Add(column);
            });
            #region
            //foreach (QueryField item in template.QueryFields)
            //{
            //    //厂家聚合
            //    PhraseStorage sp = BaseFormulaManager.SplitFormula(item.Formula.Formula);
            //    decimal check = 0;
            //    for (int i = 0; i < sp.PhraseResult.Count; i++)
            //    {
            //        if (sp.PhraseTypeResult[i] != PhraseTypeEnum.number || decimal.TryParse(sp.PhraseResult[i].ToString(), out check))
            //        {
            //            continue;
            //        }
            //        //厂家聚合
            //        sp.PhraseResult[i] = string.Format(@" {0}({1})", _subQueryFormulas[sp.PhraseResult[i].ToString().ToUpper()].VendorAggregation, _subQueryFormulas[sp.PhraseResult[i].ToString().ToUpper()].AttEnName);
            //    }

            //    //重新组装成原有的公式
            //    column = new SelectColumn(
            //                SqlExpression.Raw(string.Format("Round({0},{1})", BaseFormulaManager.PhraseStorageDecode(sp, sp.PhraseTypeResult.ToList(), item.Formula.IsZero),
            //               item.Formula.DigitalDigit)), item.Formula.AttEnName);

            //    ColumnCollection.Add(column);
            //}
            #endregion

            return ColumnCollection;
        }

        /// <summary>
        /// 是否存在忙时CounterGroup数据
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="busyInfo">忙时信息</param>
        /// <returns></returns>
        protected bool IsBusyCounterGroupData(QueryTemplateView template, BusyInfo busyInfo)
        {
            if (busyInfo != null && ((template.TimerQuery.BusyTimeStyle == BusyTimeType.DayBusy && !string.IsNullOrWhiteSpace(busyInfo.Busy_Group_Day_Busy_Type)) || (template.TimerQuery.BusyTimeStyle == BusyTimeType.MorningBusy && !string.IsNullOrWhiteSpace(busyInfo.Busy_Group_Morning_Busy_Type)) || (template.TimerQuery.BusyTimeStyle == BusyTimeType.EveningBusy && !string.IsNullOrWhiteSpace(busyInfo.Busy_Group_Evening_Busy_Type))))
            {
                return true;
            }
            return false;
        }


        #endregion
        #region 6.建立Where条件，不带汇聚where
        /// <summary>
        /// 判断当前粒度是否与业务类型关联
        /// </summary>
        /// <param name="netType">网络制式</param>
        /// <param name="neLevel">网元级别</param>
        /// <returns></returns>
        private bool HasNeLevelRelatedBusiness(int neType, int neLevel)
        {
            return _inDicatorCfgService.GetById(neType, neLevel.ToString()) != null;
        }
        /// <summary>
        /// 建立查询条件
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="vendor">厂家</param>
        /// <returns></returns>
        protected WhereClause BuildWherePhrase(QueryTemplateView template, string vendor)
        {
            WhereClause clause = new WhereClause();

            //添加时间条件
            if (template.TimerQuery.TimeCondition.IsNullOrEmpty() || template.TimerQuery.TimeCondition.ToUpper()
                .IndexOf(_neRelationInfo.TimeFieldName.ToUpper()) >= 0)
            {
                clause.Terms.Add(WhereTerm.CustomSql("({0})"));
            }
            else
            {
                clause.Terms.Add(WhereTerm.CustomSql(template.TimerQuery.TimeCondition));
            }

            var dictCode = template.NeType.ToString() + (template.NeLevel % 100).ToString("00");

            //添加网元条件
            Enum_Relation relation = _enumRelService.GetEnumRels().FirstOrDefault(a =>
                                a.Source_Type == "NE_LEVEL" &&
                                a.Dict_Code == dictCode &&
                                a.Dest_Type == "NE_AGGREGATION" &&
                                a.Dest_Value == "0");

            string NeWhere = string.Empty;
            List<string> defaulColumns = new List<string>();
            if (!string.IsNullOrEmpty(template.NeQuery.NeCondition))
            {
                NeWhere = string.Format(" {0} ", template.NeQuery.NeCondition.ToUpper());
                //2015-04-03  panguo 组合扩展条件

                if (!string.IsNullOrWhiteSpace(_neAggregationRelation.ExtensionTableView))
                {
                    foreach (var item in _neAggregationRelation.ExtensionFields)
                    {
                        if (NeWhere.IndexOf(string.Format(" {0} ", item.ToUpper())) >= 0)
                        {
                            NeWhere = NeWhere.SpecialReplace(string.Format(" {0} ", item.ToUpper()), string.Format(" {0}.{1} ", EXTENSION_TABLE_NAME, item.ToUpper()));
                        }
                    }
                    defaulColumns.AddRange(_neAggregationRelation.ExtensionFields);
                }

                foreach (var item in relation.Redundance_Fields.Select(t => t.DictCode).Where(x => !defaulColumns.Contains(x)))
                {
                    if (NeWhere.IndexOf(string.Format(" {0} ", item.ToUpper())) >= 0)
                    {
                        NeWhere = Helper.SpecialReplace(NeWhere, string.Format(" {0} ", item.ToUpper()), string.Format(" tb0.{0} ", item.ToUpper()));
                    }
                }
                defaulColumns.AddRange(relation.Redundance_Fields.Select(t => t.DictCode));
                foreach (var item in _neAggregationRelation.Redundance_Fields.Select(t => t.DictCode).Where(x => !defaulColumns.Contains(x)))
                {
                    if (NeWhere.IndexOf(string.Format(" {0} ", item.ToUpper())) >= 0)
                    {
                        NeWhere = Helper.SpecialReplace(NeWhere, string.Format(" {0} ", item.ToUpper()), string.Format(" tb0.{0} ", item.ToUpper()));
                    }
                }
            }
            if (!string.IsNullOrEmpty(NeWhere))
            {
                var task = GetCityIdAsync();
                task.Wait();
                var result = task.Result;
                bool hasAppendCityId = defaulColumns.Contains("CITY_ID") & NeWhere.IndexOf("CITY_ID") < 0;
                clause.Terms.Add(WhereTerm.CustomSql(string.Format("  ({0}) {1} ", NeWhere, hasAppendCityId ? string.Format(" and tb0.CITY_ID in (0,{0})", string.Join(",", result)) : string.Empty)));

            }

            var neType = template.NeType.GetPerfNetType();

            if ((neType == NetType.GSM || neType == NetType.WCDMA) && template.NeLevel != (int)NeLevel.SUBITEM && template.VendorVersion != DefaultData.PUBLIC_VENDOR_CODE)
            {
                clause.Terms.Add(WhereTerm.CustomSql(string.Format("  tb0.vendor='{0}' ", vendor.Split('V')[0])));
            }

            if (template.FrequencyBands != null && template.FrequencyBands.Length > 0 && template.NeLevel == (int)NeLevel.Cell)
            {
                clause.Terms.Add(WhereTerm.CustomSql(string.Format("  tb0.FREQUENCY_BAND in ({0}) ", template.FrequencyBands.JoinString(",", true))));
            }

            //增加载频条件
            if (HasNeLevelRelatedBusiness(template.NeType, template.NeLevel))
            {
                if (template.NeType == 1 && !string.IsNullOrEmpty(template.NeQuery.Carriers))
                {
                    clause.Terms.Add(WhereTerm.CustomSql(string.Format("tb0.CARR in({0})",
                            template.NeQuery.Carriers)));
                }
                
                    string busyType = template.NeType.GetBusinessType( template.BusinessType);

                if (busyType.IsNullOrEmpty())
                    clause.Terms.Add(WhereTerm.CustomSql(string.Format("tb0.FDDTDDIND ='{0}'", busyType)));
               
            }

            //如果存在2个以上的表，需要构建表之间的关联条件
            if (_tableCollection.Count > 1)
            {
                for (int i = 0; i < _tableCollection.Count - 1; i++)
                {
                    foreach (string RelationColumn in _neRelationInfo.TableRelationFields)
                    {
                        clause.Terms.Add(WhereTerm.CustomSql(string.Format("tb{0}.{2}=tb{1}.{2}",
                            _tableCollection.ElementAt(i).Value,
                            _tableCollection.ElementAt(i + 1).Value,
                            RelationColumn)));

                    }
                    if (IsBusyCounterGroupData(template, _busyInfo))
                    {
                        clause.Terms.Add(WhereTerm.CustomSql(string.Format("tb{0}.{2}=tb{1}.{2}",
                                                   _tableCollection.ElementAt(i).Value,
                                                   _tableCollection.ElementAt(i + 1).Value,
                                                   BUSY_TYPE_COLNAME)));
                    }
                }
            }

            if (template.TimerQuery.TimeGranularity == 1 && _busyInfo != null)
            {
                switch (template.TimerQuery.BusyTimeStyle)
                {
                    case BusyTimeType.DayBusy:
                        if (string.IsNullOrWhiteSpace(_busyInfo.Busy_Group_Day_Busy_Type))
                        {
                            clause.Terms.Add(WhereTerm.CustomSql(string.Format("tb0.{0} = {2}.{1}",
                            _neRelationInfo.TimeFieldName,
                            _busyInfo.Busy_Time_Day_Field_Name, BUSY_TABLE_NAME)));

                            if (!string.IsNullOrEmpty(_busyInfo.Ne_Field_Name))
                            {
                                clause.Terms.Add(WhereTerm.CustomSql(string.Format("tb0.{0} = {1}.{0}",
                                    _busyInfo.Ne_Field_Name, BUSY_TABLE_NAME)));
                            }
                        }
                        else
                        {
                            clause.Terms.Add(WhereTerm.CustomSql(string.Format("tb0.{1} = {0}",
                               _busyInfo.Busy_Group_Day_Busy_Type, BUSY_TYPE_COLNAME)));
                        }
                        break;
                    case BusyTimeType.MorningBusy:
                        if (string.IsNullOrWhiteSpace(_busyInfo.Busy_Group_Morning_Busy_Type))
                        {
                            clause.Terms.Add(WhereTerm.CustomSql(string.Format("tb0.{0} = {2}.{1}",
                                _neRelationInfo.TimeFieldName,
                               _busyInfo.Busy_Time_Morning_Field_Name, BUSY_TABLE_NAME)));
                            if (!string.IsNullOrEmpty(_busyInfo.Ne_Field_Name))
                            {
                                clause.Terms.Add(WhereTerm.CustomSql(string.Format("tb0.{0} = {1}.{0}",
                                    _busyInfo.Ne_Field_Name, BUSY_TABLE_NAME)));
                            }
                        }
                        else
                        {
                            clause.Terms.Add(WhereTerm.CustomSql(string.Format("tb0.{1} = {0}",
                               _busyInfo.Busy_Group_Morning_Busy_Type, BUSY_TYPE_COLNAME)));
                        }
                        break;
                    case BusyTimeType.EveningBusy:
                        if (string.IsNullOrWhiteSpace(_busyInfo.Busy_Group_Evening_Busy_Type))
                        {
                            clause.Terms.Add(WhereTerm.CustomSql(string.Format("tb0.{0} = {2}.{1}",
                              _neRelationInfo.TimeFieldName,
                              _busyInfo.Busy_Time_Evening_Field_Name, BUSY_TABLE_NAME)));
                            if (!string.IsNullOrEmpty(_busyInfo.Ne_Field_Name))
                            {
                                clause.Terms.Add(WhereTerm.CustomSql(string.Format("tb0.{0} = {1}.{0}",
                                    _busyInfo.Ne_Field_Name, BUSY_TABLE_NAME)));
                            }
                        }
                        else
                        {
                            clause.Terms.Add(WhereTerm.CustomSql(string.Format("tb0.{1} = {0}",
                               _busyInfo.Busy_Group_Evening_Busy_Type, BUSY_TYPE_COLNAME)));
                        }
                        break;
                }
            }

            //添加扩展表视图关联关系 ， 有合并显示聚合字段的已合并配置为准 
            foreach (var field in _neAggregationRelation.ExtensionRelationFields)
            {
                if (!string.IsNullOrEmpty(field))
                {
                    clause.Terms.Add(WhereTerm.CustomSql(string.Format("tb0.{0} = {1}.{0}", field, EXTENSION_TABLE_NAME)));
                }
            }


            //添加指标条件
            foreach (var group in template.FilterGroup)
            {
                //foreach (ConditionGroup group in souceGroup.Value)
                //{
                WhereClause subclause = new WhereClause(group.Logic);
                //递归解析过滤字段
                RecursionBuildFilterFormula(subclause, group, vendor);

                clause.SubClauses.Add(subclause);
                //}
            }
            return clause;
        }

        /// <summary>
        /// 递归建立过滤条件
        /// </summary>
        /// <param name="clause">查询条件</param>
        /// <param name="group">分组</param>
        /// <param name="vendor">厂家</param>
        /// <returns></returns>
        protected void RecursionBuildFilterFormula(WhereClause clause, ConditionGroup group, string vendor)
        {
            if (group.ConditionFieldList != null && group.ConditionFieldList.Count > 0)
            {
                #region
                foreach (QueryFilter filter in group.ConditionFieldList)
                {
                    var item = _baseFormulaService.GetById(filter.FieldID);
                    PhraseStorage sp = null;
                    if (_dictVendorFilterFormula.ContainsKey(string.Format(@"{0}_{1}", vendor, item.AttEnName)))
                    {
                        sp = _dictVendorFilterFormula[string.Format(@"{0}_{1}", vendor, item.AttEnName)];
                    }
                    if (sp == null)
                        continue;

                    string FormulaString = BuildForumlaByRawCounter(sp, 0, _template, item, vendor);

                    double d = 0;
                    if (double.TryParse(filter.Value.ToString(), out d))
                    {
                        clause.Terms.Add(
                            WhereTerm.CreateCompare(filter.Logic, SqlExpression.Raw(FormulaString),
                            SqlExpression.Number(d), filter.Operator));
                    }
                    else
                    {
                        clause.Terms.Add(
                            WhereTerm.CreateCompare(filter.Logic, SqlExpression.Raw(FormulaString),
                            SqlExpression.String(filter.Value.ToString()), filter.Operator));
                    }
                }
                #endregion

            }

            if (group.ConditionGroupList != null)
            {
                foreach (ConditionGroup subGroup in group.ConditionGroupList)
                    RecursionBuildFilterFormula(clause, subGroup, vendor);
            }
        }
        #endregion

        #region 7.Order by Field
        /// <summary>
        /// 排序字段
        /// </summary>
        /// <param name="template">模板</param>
        /// <returns></returns>
        protected OrderByTermCollection BuildOrderByTerm(QueryTemplateView template, bool isUseOrderby = true)
        {
            OrderByTermCollection OrderByCollection = new OrderByTermCollection();
            if (!isUseOrderby)
                return OrderByCollection;
            foreach (QueryField item in template.QueryFields)
            {

                if (item.OrderBy == OrderByDirection.Null)
                    continue;
                if (item.Alias.IsNullOrEmpty())
                {
                    var formula = _baseFormulaService.GetById(item.FieldID);
                    OrderByCollection.Add(new OrderByTerm(formula.AttEnName, item.OrderBy));
                }
                else
                {
                    OrderByCollection.Add(new OrderByTerm(item.Alias, item.OrderBy));
                }
            }
            if (OrderByCollection.Count < 1)
            {
                if (template.TimerQuery.TimeAggregation == -1)
                {

                    var item = template.QueryFields.FirstOrDefault();
                    if (item != null)
                    {
                        if (item.Alias.IsNullOrEmpty())
                        {
                            var formula = _baseFormulaService.GetById(item.FieldID);
                            OrderByCollection.Add(new OrderByTerm(formula.AttEnName, OrderByDirection.Ascending));
                        }
                        else
                        {
                            OrderByCollection.Add(new OrderByTerm(item.Alias, OrderByDirection.Ascending));
                        }
                    }
                }
                else
                {
                    //改成默认按时间排序
                    OrderByCollection.Add(new OrderByTerm("TIME_KEY_FIELD", OrderByDirection.Descending));
                }
            }

            return OrderByCollection;
        }
        #endregion

        #region  8.Group by Field
        /// <summary>
        /// 分组字段
        /// </summary>
        /// <param name="template">模板</param>
        /// <returns></returns>
        protected GroupByTermCollection BuildGroupByTerm(QueryTemplateView template)
        {
            GroupByTermCollection GroupByCollection = new GroupByTermCollection();

            //添加时间冗余字段 
            //添加时间主键字段
            if (_timeAggregationRelation != null && !string.IsNullOrEmpty(_timeAggregationRelation.KeyField))
            {
                if (_timeAggregationRelation.KeyField.IndexOf("{0}.") >= 0)
                    GroupByCollection.Add(new GroupByTerm(string.Format(_timeAggregationRelation.KeyField, "tb0")));
                else
                    GroupByCollection.Add(new GroupByTerm(string.Format("tb0.{0}", _timeAggregationRelation.KeyField)));
            }
            string fieldstring = "";
            foreach (DictItem item in _timeAggregationRelation.Redundance_Fields)
            {
                fieldstring = item.DictCode.ToUpper();
                if (fieldstring.IndexOf("{0}.") >= 0)
                    fieldstring = string.Format(fieldstring, "tb0");
                else
                    fieldstring = string.Format("tb0.{0}", fieldstring);

                if (fieldstring.IndexOf(" AS ") >= 0)
                {
                    fieldstring = fieldstring.Substring(0, fieldstring.IndexOf(" AS ")).Trim();
                    fieldstring = string.Format(fieldstring, "tb0");
                    GroupByCollection.Add(new GroupByTerm(fieldstring));
                }
                else
                    GroupByCollection.Add(new GroupByTerm(fieldstring));
            }
            //添加网元冗余字段   
            if (_neAggregationRelation != null && !string.IsNullOrEmpty(_neAggregationRelation.KeyField))
            {
                if (_neAggregationRelation.KeyField.IndexOf("{0}.") >= 0)
                    GroupByCollection.Add(new GroupByTerm(string.Format(_neAggregationRelation.KeyField, "tb0")));
                else
                {
                    if (_neAggregationRelation.ExtensionFields.Contains(_neAggregationRelation.KeyField) && !string.IsNullOrEmpty(_neAggregationRelation.ExtensionTableView))
                        GroupByCollection.Add(new GroupByTerm(string.Format("{1}.{0}", _neAggregationRelation.KeyField, EXTENSION_TABLE_NAME)));
                    else
                        GroupByCollection.Add(new GroupByTerm(string.Format("tb0.{0}", _neAggregationRelation.KeyField)));
                }
            }

            foreach (DictItem item in _neAggregationRelation.Redundance_Fields)
            {
                if (item.DictCode.ToUpper().IndexOf(_neRelationInfo.TimeFieldName.ToUpper().Trim()) >= 0)
                {
                    GroupByCollection.Add(new GroupByTerm(string.Format("tb0.{0}", _neRelationInfo.TimeFieldName.ToUpper().Trim())));
                }
                else
                {
                    if (_neAggregationRelation.ExtensionFields.Contains(item.DictCode))
                    {
                        if (!string.IsNullOrEmpty(_neAggregationRelation.ExtensionTableView))
                        {
                            GroupByCollection.Add(new GroupByTerm(string.Format("{1}.{0}", item.DictCode, EXTENSION_TABLE_NAME)));
                        }
                        else
                        {
                            //GroupByCollection.Add(new GroupByTerm(string.Format("{1}.{0}", item.DictCode, EXTENSION_TABLE_NAME)));
                        }
                    }
                    else
                    {
                        GroupByCollection.Add(new GroupByTerm(string.Format("tb0.{0}", item.DictCode)));
                    }
                }
            }

            return GroupByCollection;
        }

        /// <summary>
        /// 外部分组
        /// </summary>
        /// <param name="template">模板</param>
        /// <returns></returns>
        protected GroupByTermCollection BuildOutSiteGroupByTerm(QueryTemplateView template)
        {
            GroupByTermCollection GroupByCollection = new GroupByTermCollection();

            //添加时间冗余字段   
            foreach (DictItem item in _timeAggregationRelation.Redundance_Fields)
            {
                if (item.DictCode.ToUpper().IndexOf(" AS ") >= 0)
                {
                    GroupByCollection.Add(new GroupByTerm(item.DictCode.ToUpper().Substring(item.DictCode.ToUpper().IndexOf(" AS ") + 4)));
                }
                else
                    GroupByCollection.Add(new GroupByTerm(item.DictCode));
            }
            if ((template.TimerQuery.TimeAggregation != 0 || _vendorInfo.Count > 1) &&
                template.TimerQuery.TimeAggregation != -1)
                GroupByCollection.Add(new GroupByTerm("TIME_KEY_FIELD"));

            //添加网元冗余字段  
            foreach (DictItem item in _neAggregationRelation.Redundance_Fields)
            {
                if (item.DictCode.ToUpper().IndexOf(" AS ") >= 0)
                {
                    GroupByCollection.Add(new GroupByTerm(item.DictCode.ToUpper().Substring(item.DictCode.ToUpper().IndexOf(" AS ") + 4)));
                }
                else
                {
                    if (_neAggregationRelation.ExtensionFields.Contains(item.DictCode))
                    {
                        if (!string.IsNullOrEmpty(_neAggregationRelation.ExtensionTableView))
                        {
                            GroupByCollection.Add(new GroupByTerm(item.DictCode));
                        }
                    }
                    else
                    {
                        GroupByCollection.Add(new GroupByTerm(item.DictCode));
                    }
                }
            }
            if ((template.NeQuery.NeAggregation != 0 || _vendorInfo.Count > 1) &&
               template.NeQuery.NeAggregation != -1)
                GroupByCollection.Add(new GroupByTerm("NE_KEY_FIELD"));
            return GroupByCollection;
        }
        #endregion
        #region 9.组装单厂家公式
        /// <summary>
        /// 组装单厂家公式
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="vendor">厂家</param>
        /// <returns></returns>
        protected SelectQuery BuildSQLByVendor(QueryTemplateView template, string vendor)
        {
            SelectQuery sql = null;

            if (template.AggregationWay == (int)AggregationWay.Formula && _bNeedVendorAggregation)
            {
                sql = BuildNoAggregation(template, vendor);
            }
            else
            {

                if (!_bNeedNeAggregation && !_bNeedTimeAggregation)
                {
                    //时间个网元都不聚合
                    sql = BuildNoAggregation(template, vendor);
                }
                else if (_bNeedNeAggregation && !_bNeedTimeAggregation)
                {
                    //网元聚合，时间不聚合
                    sql = BuildNeAggregation(template, vendor);
                }
                else if (!_bNeedNeAggregation && _bNeedTimeAggregation)
                {
                    //网元不聚合，时间聚合
                    sql = BuildTimeAggregation(template, vendor);
                }
                else
                {
                    //时间个网元都聚合
                    sql = BuildNeAndTimeAggregation(template, vendor);
                }
            }

            return sql;
        }

        /// <summary>
        /// 获取扩展查询强制索引
        /// </summary>
        /// <returns></returns>
        private string GetExtensinIndexHints()
        {
            string str = string.Empty;
            for (int i = 0; i < _tableCollection.Count; i++)
            {
                str = string.Format("{0},tb{1}", str, i);
            }

            return string.Format(" /*+ no_merge(EXTENSION) leading(EXTENSION{0})*/ ", str);
        }

        /// <summary>
        /// 创建不聚合SQL
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="vendor">厂家</param>
        /// <returns></returns>
        protected SelectQuery BuildNoAggregation(QueryTemplateView template, string vendor)
        {
            SelectQuery query = new SelectQuery();
            query.FromClause.BaseTable = GetInterFormTerm(template, vendor);
            if (!string.IsNullOrWhiteSpace(_neAggregationRelation.ExtensionTableView))
            {
                query.IndexHints = GetExtensinIndexHints();
            }
            query.Columns = BuildSelectColumn_Formula_NotExistAggregation(template, vendor);

            query.WherePhrase = BuildWherePhrase(template, vendor);
            return query;
        }
        /// <summary>
        /// 创建网元聚合SQL
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="vendor">厂家</param>
        /// <returns></returns>
        protected SelectQuery BuildNeAggregation(QueryTemplateView template, string vendor)
        {

            SelectQuery query = new SelectQuery();

            query.FromClause.BaseTable = GetInterFormTerm(template, vendor);
            if (!string.IsNullOrWhiteSpace(_neAggregationRelation.ExtensionTableView))
            {
                query.IndexHints = GetExtensinIndexHints();
            }
            query.Columns = BuildSelectColumn_Formula_ExistAggregation(template, true, vendor);
            query.WherePhrase = BuildWherePhrase(template, vendor);
            //query.OrderByTerms = BuildOrderByTerm(Template);
            query.GroupByTerms = BuildGroupByTerm(template);
            return query;
        }
        /// <summary>
        /// 创建Counter网元聚合SQL
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="vendor">厂家</param>
        /// <returns></returns>
        protected SelectQuery Build_InSite_Counter_NeAggregation(QueryTemplateView template, string vendor)
        {
            SelectQuery query = new SelectQuery();

            query.FromClause.BaseTable = GetInterFormTerm(template, vendor);
            if (!string.IsNullOrWhiteSpace(_neAggregationRelation.ExtensionTableView))
            {
                query.IndexHints = GetExtensinIndexHints();
            }
            query.Columns = BuildSelectColumn_InSite_Counter_NeAggregation(template, vendor);

            query.WherePhrase = BuildWherePhrase(template, vendor);
            //query.OrderByTerms = BuildOrderByTerm(Template);
            query.GroupByTerms = BuildGroupByTerm(template);

            //添加时间主键字段
            if (_neRelationInfo != null && !string.IsNullOrEmpty(_neRelationInfo.TimeFieldName))
            {
                query.GroupByTerms.Add(new GroupByTerm(string.Format("tb0.{0}", _neRelationInfo.TimeFieldName)));
            }

            return query;
        }
        /// <summary>
        /// 创建时间聚合SQL
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="vendor">厂家</param>
        /// <returns></returns>
        protected SelectQuery BuildTimeAggregation(QueryTemplateView template, string vendor)
        {
            SelectQuery query = new SelectQuery();

            query.FromClause.BaseTable = GetInterFormTerm(template, vendor);
            if (!string.IsNullOrWhiteSpace(_neAggregationRelation.ExtensionTableView))
            {
                query.IndexHints = GetExtensinIndexHints();
            }
            query.Columns = BuildSelectColumn_Formula_ExistAggregation(template, false, vendor);

            query.WherePhrase = BuildWherePhrase(template, vendor);
            //query.OrderByTerms = BuildOrderByTerm(Template);
            query.GroupByTerms = BuildGroupByTerm(template);
            return query;
        }

        /// <summary>
        /// 创建网元和时间都聚合SQL
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="vendor">厂家</param>
        /// <returns></returns>
        protected SelectQuery BuildNeAndTimeAggregation(QueryTemplateView template, string vendor)
        {
            SelectQuery mainquery = new SelectQuery();

            SelectQuery subquery = Build_InSite_Counter_NeAggregation(template, vendor);
            FromTerm maint = FromTerm.SubQuery(subquery, "");

            mainquery.FromClause.BaseTable = maint;
            mainquery.Columns = BuildSelectColumn_MiddleSite_Counter_TimeAggregation(template, vendor);
            mainquery.GroupByTerms = BuildOutSiteGroupByTerm(template);

            return mainquery;
        }

        #endregion

        #region 10.组装多厂家公式
        /// <summary>
        /// 组装多厂家公式
        /// </summary>
        /// <param name="template">模板</param>
        /// <returns></returns>
        protected SelectQuery BuildSQLByMuliVendor(QueryTemplateView template)
        {
            SelectQuery mainquery = new SelectQuery();

            SqlUnion union = new SqlUnion();
            FromTerm maint;
            foreach (string vendor in _vendorInfo)
            {
                SelectQuery subquery = BuildSQLByVendor(template, vendor);
                if (subquery.FromClause.BaseTable.Expression != null)
                {
                    union.Add(subquery, DistinctModifier.All);
                }
            }

            //OracleRenderer _renderer = new OracleRenderer();

            //string sqlUnion = _renderer.RenderUnion(union);

            maint = FromTerm.SubQuery(union, "");
            mainquery.FromClause.BaseTable = maint;
            mainquery.Columns = BuildSelectColumn_OutSite_Att_EnName_Aggregation(template);
            mainquery.GroupByTerms = BuildOutSiteGroupByTerm(template);

            return mainquery;
        }

        #endregion
    }
}
