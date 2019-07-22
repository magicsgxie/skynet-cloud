/************************************************************************************
 * Copyright (c) 2016 优网科技 All Rights Reserved.
 * CLR版本： 4.0.30319.42000
 * 机器名称：UWAY15003
 * 公司名称：优网科技
 * 命名空间：UWay.Skynet.Cloud.Dcs.Services
 * 文件名：  QueryTemplateService
 * 版本号：  V1.0.0.0
 * 唯一标识：cec9edc6-3323-4847-9f69-e380a7654c8a
 * 当前的用户域：UWAY15003
 * 创建人：  谢韶光
 * 电子邮箱：xiesg@uway.cn
 * 创建时间：2016/6/21 15:13:02
 * 描述：
 * 
 * 
 * =====================================================================
 * 修改标记 
 * 修改时间：2016/6/21 15:13:02
 * 修改人： 谢韶光
 * 版本号： V1.0.0.0
 * 描述：模板管理
 * 
 * 
 * 
 * 
 ************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UWay.Skynet.Cloud.Linq;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Dcs.Repository;
using UWay.Skynet.Cloud.Dcs.Entity;
using UWay.Skynet.Cloud;
using UWay.Skynet.Cloud.Request;
using Skynet.Cloud.Dcs.Repository;
using UWay.Skynet.Cloud.Dcs.Service.Interface;

namespace UWay.Skynet.Cloud.Dcs.Service
{
    /// <summary>
    /// 模板管理
    /// </summary>
    public class QueryTemplateService : IQueryTemplateService
    {
        IBaseFormulaService _baseFormulaService;
        public QueryTemplateService(IBaseFormulaService baseFormulaService)
        {
            _baseFormulaService = baseFormulaService;
        }

        /// <summary>
        /// <see cref="IOpenQueryTemplateService.AddQueryTemplate(QueryTemplateView)"/>
        /// </summary>
        /// <param name="item"><see cref="QueryTemplateView"/></param>
        /// <returns></returns>
        public long AddQueryTemplate(QueryTemplateView item)
        {
            
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                
               
                long templateID = 0;
                context.UsingTransaction(() =>
                {
                    templateID = new QueryTemplateRepository(context).AddQueryTemplate(item);
                    List<QueryGroup> groups = new List<QueryGroup>();
                    List<QueryFilter> filters = new List<QueryFilter>();
                    Recursive(item.FilterGroup, string.Empty, string.Empty, 0, templateID, groups, filters);

                    new QueryGroupRepository(context).BacthAddQueryGroup(groups);
                    new QueryFilterRepository(context).AddBatchQueryFilter(filters); 
                    if(item.QueryFields != null && item.QueryFields.Count > 0)
                    {
                        item.QueryFields.ForEach(p => {
                            p.TemplateID = templateID;
                        });

                        if (item.BaseQueryFields != null && item.BaseQueryFields.Count > 0)
                        {
                            item.BaseQueryFields.ForEach(p =>
                            {
                                p.TemplateID = templateID;
                            });
                            item.QueryFields.AddRange(item.BaseQueryFields);
                        }

                        new QueryFieldRepository(context).AddBatchQueryField(item.QueryFields);
                    }

                    if (item.ColorLevelCfgInfos != null && item.ColorLevelCfgInfos.Count > 0)
                    {
                        var csr = new ColorSettingRepository(context);
                        item.ColorLevelCfgInfos.ForEach(p =>
                        {
                            p.ModuleId = 400000000 + item.MenuID * 1000;
                            p.SettingName = string.Format("{0}|{1}|{2}",  p.SettingName, templateID, item.TemplateName);
                        });
                        csr.Add(item.ColorLevelCfgInfos);
                        var list = new List<ColorLevelDynamicInfo>();
                        item.ColorLevelCfgInfos.ForEach(o =>
                        {
                            list.AddRange(o.SectionList);
                        });
                        csr.Add(list);
                    }

                    if(item.IsHasNe && !item.NeParameter.IsNullOrEmpty())
                    {
                        new QueryNeRepository(context).AddQueryNe(new QueryNe { TemplateID = templateID, NeValue = item.NeParameter, NeName = item.NeNames });
                    }
                    //var strWhere = item.NeParameter.ToString();
                    
                });
                return templateID;
            }
        }

        /// <summary>
        /// 递归生成分组信息
        /// </summary>
        /// <param name="source">分组源</param>
        /// <param name="groupID">分组ID</param>
        /// <param name="funcKind">父分组ID列表</param>
        /// <param name="level">节点级别</param>
        /// <param name="templateID">模板ID</param>
        /// <param name="result">分组结果列表</param>
        /// <param name="filters">分组下对应的过滤字段</param>
        private void Recursive(List<ConditionGroup> source, string groupID, string funcKind, int level, long templateID, List<QueryGroup> result, List<QueryFilter> filters)
        {

            if (result == null)
            {
                result = new List<QueryGroup>();
            }
            if (filters == null)
            {
                filters = new List<QueryFilter>();
            }
            if (source != null && source.Count > 0)
            {
                var i = 0;
                source.ForEach(p =>
                {
                    result.Add(new QueryGroup()
                    {
                        ID = string.Format("{0}{1}{2}", templateID, level, i),
                        ParentID = groupID,
                        FuncKind = funcKind,
                        LogicalOperator = p.Logic,
                        TemplateID = templateID
                    });

                    if(p.ConditionFieldList != null && p.ConditionFieldList.Count > 0)
                    {
                        p.ConditionFieldList.ForEach(q =>
                        {
                            q.GroupID = string.Format("{0}{1}{2}", templateID,  level, i);
                        });
                        filters.AddRange(p.ConditionFieldList);
                    }

                    Recursive(p.ConditionGroupList, string.Format("{0}{1}{2}", templateID,  level, i),
                        string.Format("{0}{1}{2}{3}", templateID,  level, i, groupID), level + 1, templateID, result, filters);
                    i++;
                });
            }
        }

      

        /// <summary>
        /// 批量删除模板
        /// </summary>
        /// <param name="menuID">功能ID</param>
        /// <param name="idArray">模板ID列表</param>
        /// <returns></returns>
        public int DeleteQueryTemplates(int menuID,long[] idArray)
        {
            //var ids = new List<QueryParameter>();
            //idArray.ForEach(id =>
            //{
            //    ids.Add(new QueryParameter()
            //    {
            //        Name = "SettingName",
            //        Operator = FilterOperator.Contains,
            //        Value = "|" + id
            //    });
            //});

            //var paramters = new List<QueryParameter>();
            //paramters.Add(new QueryParameter()
            //{
            //    Name = "ModuleId",
            //    Operator = FilterOperator.IsEqualTo,
            //    Value = 400000000 + menuID * 1000
            //});
            //if (ids.Count == 1)
            //{

            //    paramters.AddRange(ids);
            //}
            //else
            //{
            //    paramters.Add(new QueryParameter()
            //    {
            //        Comperation = FilterCompositionLogicalOperator.Or,
            //        Composite = ids
            //    });
            //}
            //using (var context = UnitOfWork.Get(Unity.ContainerName))
            //{
            //    var result = 0;
            //    context.UsingTransaction(() =>
            //    {
            //        new QueryTemplateRepository(context).DeleteQueryTemplate(idArray);
            //        var r = new QueryGroupRepository(context);
            //        var queryGroups = r.GetQueryGroups().Where(p => idArray.Contains(p.TemplateID)).Select(p => p.ID).ToList();
            //        if (queryGroups != null && queryGroups.Count > 0)
            //        {
            //            r.DeleteQueryGroupByTemplateID(idArray);
            //            new QueryFilterRepository(context).DeleteQueryFilterByGroupID(queryGroups);
            //        }
            //        new QueryFieldRepository(context).DeleteQueryFieldByTemplateID(idArray);
            //        var cs = new ColorSettingRepository(context);
            //        var csr = new ColorSettingRepository(context);
            //        var settings = csr.All().Where(paramters.ToLambda<ColorLevelSetting>()).ToList();
            //        if (settings.FirstOrDefault() != null)
            //        {
            //            csr.Delete(settings.Select(p => p.Id));
            //            csr.DeleteDynamic(settings.Select(p => p.Id));
            //        }
            //        new QueryNeRepository(context).DeleteQueryNe(idArray);
            //        result = 1;
            //    });
            //    return result;
            //}
            return 0;
        }

        /// <summary>
        /// 判断模板名称是否存在
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        public bool IsExistsTemplateName(string templateName, int menuID)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                using (var r = new QueryTemplateRepository(context))
                {
                    return r.GetQueryTemplates().Where(p => p.TemplateName == templateName && p.MenuID == menuID).Any();
                }
            }
        }

        /// <summary>
        /// 分页获取模板信息
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public DataSourceResult GetQueryTemplatePaginations(DataSourceRequest dataSourceRequest)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                return new QueryTemplateRepository(context).GetQueryTemplates().ToDataSourceResult(dataSourceRequest);
            }
        }


        /// <summary>
        /// 更新模板信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int UpdateQueryTemplates(QueryTemplateView item)
        {
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                var result = 0;
                context.UsingTransaction(() =>
                {
                    new QueryTemplateRepository(context).UpdateQueryTemplate(item);
                    var r = new QueryGroupRepository(context);
                    var queryGroups = r.GetQueryGroups().Where(p => p.TemplateID == item.TemplateID).Select(p => p.ID).ToList();
                    if (queryGroups != null && queryGroups.Count > 0)
                    {
                        r.DeleteQueryGroupByTemplateID(item.TemplateID);
                        new QueryFilterRepository(context).DeleteQueryFilterByGroupID(queryGroups);
                    }
                    List<QueryGroup> groups = new List<QueryGroup>();
                    List<QueryFilter> filters = new List<QueryFilter>();
                    Recursive(item.FilterGroup, null, null, 0, item.TemplateID, groups, filters);

                    new QueryGroupRepository(context).BacthAddQueryGroup(groups);
                    new QueryFilterRepository(context).AddBatchQueryFilter(filters);
                    if (item.QueryFields != null && item.QueryFields.Count > 0)
                    {
                        var qf = new QueryFieldRepository(context);
                        qf.DeleteQueryFieldByTemplateID(new long[] { item.TemplateID });
                        item.QueryFields.ForEach(p =>
                        {
                            p.TemplateID = item.TemplateID;
                        });

                        if (item.BaseQueryFields != null && item.BaseQueryFields.Count > 0)
                        {
                            item.BaseQueryFields.ForEach(p =>
                            {
                                p.TemplateID = item.TemplateID;
                            });
                            item.QueryFields.AddRange(item.BaseQueryFields);
                        }

                        qf.AddBatchQueryField(item.QueryFields);
                    }

                    if(item.ColorLevelCfgInfos != null && item.ColorLevelCfgInfos.Count > 0)
                    {
                        //var service = ServiceFactory.GetService<IColorSettingService>();
                        var csr = new ColorSettingRepository(context);
                        var settings = csr.All().Where(t => t.ModuleId.Equals(400000000 + item.MenuID * 1000) && t.SettingName.Contains(item.TemplateID.ToString() + "|")).ToList();
                        if(settings.FirstOrDefault() != null)
                        {
                            csr.Delete(settings.Select(p => p.Id));
                            csr.DeleteDynamic(settings.Select(p => p.Id));
                        }
                            
                        item.ColorLevelCfgInfos.ForEach(p =>
                        {
                            p.ModuleId = 400000000 + item.MenuID * 1000;
                            p.SettingName = string.Format("{0}|{1}|{2}", p.SettingName, item.TemplateID,item.TemplateName);
                        });
                        csr.Add(item.ColorLevelCfgInfos);
                        var list = new List<ColorLevelDynamicInfo>();
                        item.ColorLevelCfgInfos.ForEach(o =>
                        {
                            list.AddRange(o.SectionList);
                        });
                        csr.Add(list);
                    }

                    if(item.IsHasNe)
                    {
                        var qne = new QueryNeRepository(context).GetQueryNe(item.TemplateID);
                        if (qne != null)
                        {
                            new QueryNeRepository(context).UpdateQueryNe(new QueryNe { TemplateID = item.TemplateID, NeValue = item.NeParameter, NeName = item.NeNames });
                        }
                        else
                        {
                            new QueryNeRepository(context).AddQueryNe(new QueryNe { TemplateID = item.TemplateID, NeValue = item.NeParameter, NeName = item.NeNames });
                        }
                    }
                    result = 1;
                });
                return result;
            }
        }

        /// <summary>
        /// 获取模板信息
        /// </summary>
        /// <param name="templateID"></param>
        /// <returns></returns>
        public QueryTemplateView GetQueryTemplateByID(long templateID)
        {
            QueryTemplateView template = null;
            using (var context = UnitOfWork.Get(Unity.ContainerName))
            {
                using (var qt = new QueryTemplateRepository(context))
                {
                    var result = qt.GetQueryTemplateByID(templateID);
                    if(result != null)
                    {
                        template = new QueryTemplateView() {
                            TemplateID = result.TemplateID,
                            BusinessType = result.BusinessType,
                            IsHasNe = result.IsHasNe,
                            MenuID = result.MenuID,
                            NeLevel = result.NeLevel,
                            NeType = result.NeType,
                            ShareType = result.ShareType,
                            TemplateName = result.TemplateName,
                            VendorVersion = result.VendorVersion,
                            CreatedBy = result.CreatedBy,
                            CreatedByName = result.CreatedByName,
                            CreatedTime = result.CreatedTime,
                            UpdatedBy = result.UpdatedBy,
                            UpdatedByName = result.UpdatedByName,
                            UpdatedTime = result.UpdatedTime,

                        };
                    }
                }
                if(template != null)
                {
                    var counters = _baseFormulaService.GetPerfFormulaDatas();
                    List<QueryGroup> groups = new List<QueryGroup>();
                    using (var qg = new QueryGroupRepository(context))
                    {
                        groups = qg.GetQueryGroups().Where(p => p.TemplateID == templateID).ToList();
                    }
                    List<QueryFilter> queryFilters = new List<QueryFilter>();
                    var condtionGroups = new List<ConditionGroup>();
                    if (groups != null && groups.Count > 0)
                    {
                        var ids = groups.Select(g => g.ID).ToArray();
                        using (var qg = new QueryFilterRepository(context))
                        {
                            queryFilters = qg.GetQueryFilters().Where(p => ids.Contains(p.GroupID)).ToList();
                            queryFilters.ForEach(p =>
                            {
                                p.FieldName = _baseFormulaService.GetById(p.FieldID).AttCnName;
                                p.Alias = string.Format("{0}{1}", "P", p.FieldID);
                                if (counters.ContainsKey(p.FieldID))
                                {
                                    p.UserInBSC = 1;//counters[p.FieldID].NeCombination;
                                    p.UserInBTS = 1;
                                    p.UserInCARR = 1;
                                    p.UserInCELL = 1;
                                }
                            });
                        }

                        BuildCondition(condtionGroups, groups, queryFilters, string.Empty);
                    }
                    template.FilterGroup.AddRange(condtionGroups);
                    
                    List<QueryField> queryFields = new List<QueryField>();
                    List<QueryField> baseDataFields = new List<QueryField>();
                    using (var qf = new QueryFieldRepository(context))
                    {
                        var queryFieldsList = qf.GetQueryFields().Where(p => p.TemplateID == templateID).ToList();
                        //queryFieldsList.ForEach(p =>
                        //{
                        //    p.FieldName = BaseFormulaManager.GetFormula(p.FieldID).AttCnName;
                        //    p.Alias = string.Format("{0}{1}", "P", p.FieldID);
                        //});

                        foreach (var field in queryFieldsList)
                        {
                            var formula = _baseFormulaService.GetById(field.FieldID);
                            if (formula.DataSource == (int)UqlType.IndicatorData)
                            {
                                field.FieldName = formula.AttCnName;
                                field.Alias = string.Format("{0}{1}", "P", field.FieldID);
                                if (counters.ContainsKey(field.FieldID))
                                {
                                    field.UserInBSC = 1;// counters[field.FieldID].NeCombination;
                                    field.UserInBTS = 1;
                                    field.UserInCARR = 1;
                                    field.UserInCELL = 1;
                                }
                                queryFields.Add(field);
                            }
                            else if (formula.DataSource == (int)UqlType.BaseData)
                            {
                                field.FieldName = formula.Formula;
                                field.Alias = formula.AttCnName;
                                if (counters.ContainsKey(field.FieldID))
                                {
                                    field.UserInBSC = 1;// counters[field.FieldID].NeCombination;
                                    field.UserInBTS = 1;
                                    field.UserInCARR = 1;
                                    field.UserInCELL = 1;
                                }
                                baseDataFields.Add(field);
                            }
                        }

                    }
                    template.QueryFields.AddRange(queryFields);
                    template.BaseQueryFields.AddRange(baseDataFields);
                    if (template.IsHasNe)
                    {
                        var qne = new QueryNeRepository(context).GetQueryNe(templateID);
                        if (qne != null)
                        {
                            template.NeParameter = qne.NeValue;
                            template.NeNames = qne.NeName;
                        }
    
                    }
                }
                if (template != null)
                {
                    var csr = new ColorSettingRepository(context);
                    long i = 400000000 + template.MenuID * 1000;
                    var colorsettings = csr.All().Where(t => t.ModuleId.Equals(i) && t.SettingName.Contains(template.TemplateID + "|")).ToList();
                    if(colorsettings != null)
                        template.ColorLevelCfgInfos = colorsettings;
                }
            }


          
            return template;
        }

        private void BuildCondition(List<ConditionGroup> cgs, List<QueryGroup> groups, List<QueryFilter> queryFilters, string groupID)
        {

            var parents = groups.AsEnumerable();
            if(!groupID.IsNullOrEmpty())
                parents = groups.Where(p => p.ParentID== groupID);
            foreach(var item in parents)
            {
                var cg = new ConditionGroup();
                var childs = queryFilters.Where(p => p.GroupID == item.ID);
                if (childs.FirstOrDefault() != null)
                {
                    cg.ConditionFieldList.AddRange(childs);
                    cg.GroupName = BuildGroupName(childs);
                    cg.Logic = item.LogicalOperator;
                }
                else
                {
                    
                    BuildCondition(cg.ConditionGroupList, groups, queryFilters, item.ID);
                    var sb = new StringBuilder();
                    var i = 0;
                    foreach(var cgItem in cg.ConditionGroupList)
                    {
                        sb.AppendFormat("(0)", cgItem.GroupName);
                        if (i < cg.ConditionGroupList.Count())
                            sb.Append(cgItem.Logic.GetDescriptionByName());
                        i++;
                    }

                    cg.GroupName = sb.ToString();
                }
                cgs.Add(cg);
            }
        }

        private string BuildGroupName(IEnumerable<QueryFilter> filters)
        {
            var sb = new StringBuilder();
            var i = 0;
            foreach(var item in filters)
            {
                var filterName = _baseFormulaService.GetById(item.FieldID);
                if(filterName != null)
                {
                    sb.AppendFormat("({0}{1}{2})", filterName.AttCnName, item.Operator.SqlOperator(), item.Value);
                }
                
                if (i < filters.Count())
                    sb.Append(item.Logic.GetDescriptionByName());
                i++;
            }
            return sb.ToString();
        }
    }
}
