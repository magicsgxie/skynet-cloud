using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UWay.Skynet.Cloud.Request;
using UWay.Skynet.Cloud.Uflow.Entity;
using UWay.Skynet.Cloud.Uflow.Service.Interface;
using Microsoft.Extensions.Logging;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Uflow.Service
{
    public class DispatchFlowService : IDispatchFlowService
    {
        private readonly IInstanceStepService _instanceStepService;
        private readonly ITemplateStepService _templateStepService;
        private readonly ITemplateFlowService _templateFlowService;
        private readonly IInstanceFlowService _instanceFlowService;
        private readonly IInstanceStepUserService _instanceStepUserService;
        private readonly IInstanceStepFixstepService _instanceStepFixstepService;
        private readonly IInstanceStepReturnstepService _instanceStepReturnstepService;
        private readonly IInstanceStepNextstepUserService _instanceStepNextstepUserService;

        private ILoggerFactory _loggerFactory;

        private ILogger _logger;

        public DispatchFlowService(
            ITemplateFlowService templateFlowService,
            ITemplateStepService templateStepService,
            IInstanceFlowService instanceFlowService,
            IInstanceStepService instanceStepService,
            IInstanceStepUserService instanceStepUserService,
            IInstanceStepFixstepService instanceStepFixstepService,
            IInstanceStepReturnstepService instanceStepReturnstepService,
            IInstanceStepNextstepUserService instanceStepNextstepUserService,
            ILoggerFactory loggerFactory
            )
        {
            _templateFlowService = templateFlowService;
            _templateStepService = templateStepService;
            _instanceFlowService = instanceFlowService;
            _instanceStepService = instanceStepService;
            _instanceStepReturnstepService = instanceStepReturnstepService;
            _loggerFactory = loggerFactory;
            _instanceStepUserService = instanceStepUserService;
            _instanceStepFixstepService = instanceStepFixstepService;
            _instanceStepNextstepUserService = instanceStepNextstepUserService;
            _logger = _loggerFactory.CreateLogger<IDispatchFlowService>();
        }

        public R<string> DealFlowStep(DealStepDTO dto)
        {
            InstanceStepDTO instanceStepDTO = new InstanceStepDTO();
            instanceStepDTO.InstanceFlow = _instanceFlowService.GetById(dto.InstanceID);
            instanceStepDTO.TemplateFlow = _templateFlowService.GetById(instanceStepDTO.InstanceFlow.FlowId);
            instanceStepDTO.InstanceStep = _instanceStepService.GetById(dto.InstanceStepID);
            instanceStepDTO.TemplateStep = _templateStepService.GetById(instanceStepDTO.InstanceStep.Fid);
            var result = ValidateDealStep(dto, instanceStepDTO);
            if(result.Code == 0)
            {
                return result;
            }
            throw new NotImplementedException();
        }

        public R<string> StartFlow(StartFlowDTO startFlowInfo)
        {
            throw new NotImplementedException();
        }

        public R<string> TerminateFlow(Guid guid)
        {
            throw new NotImplementedException();
        }

        private R<string> ValidateDealStep(DealStepDTO dto, InstanceStepDTO AInstanceStep)
        {
            R<string> vResMsg = new R<string>();
            vResMsg.Code = 1;
            InstanceFlow vFlowEntity = AInstanceStep.InstanceFlow;
            InstanceStep vStepEntity = AInstanceStep.InstanceStep;
            TemplateFlow vTempFlow = AInstanceStep.TemplateFlow;
            TemplateStep vTempStep = AInstanceStep.TemplateStep;

            //检查工作组是否正确
            if (!vFlowEntity.GroupId.Equals(dto.GroupID))
            {
                vResMsg.Msg = "流程实例与用户的工作组不匹配!";
                return vResMsg;
            }

            //检查流程实例状态是否合适
            if (new int[] { 4, 5, 6 }.Contains(vFlowEntity.Status)
                || new int[] { 4, 5, 6, 7 }.Contains(vStepEntity.Status)
                || !vStepEntity.Progress.Equals(7))
            {
                vResMsg.Msg = "流程实例的当前状态不适合完成此操作!";
                return vResMsg;
            }

            //检查用户是否合法
            InstanceStepUser vStepUser = (from InstanceStepUser vItem in vStepEntity.InstanceStepUsers
                                               where vItem.UserCode.Equals(dto.UserCode)
                                               select vItem).FirstOrDefault();
            if (vStepUser == null)
            {
                vResMsg.Msg = "当前用户没有操作权限!";
                return vResMsg;
            }

            //处理方式与身份是否匹配
            if (vStepUser.UserType.Equals(3) && !dto.Status.Equals(5))
            {
                vResMsg.Msg = "当前操作超过了用户的权限!";
                return vResMsg;
            }

            //用户是否已提交
            if (new int[] { 1, 2, 3, 4 }.Contains(vStepUser.Status))
            {
                vResMsg.Msg = "当前步骤的操作已完成!";
                return vResMsg;
            }

            //检查处理意见
            if (new int[] { 1, 2, 4 }.Contains(dto.Status) && vTempStep.IsFormSuggestion && string.IsNullOrWhiteSpace(dto.Content))
            {
                vResMsg.Msg = "未接收到流程要求填写的主办或汇签意见!";
                return vResMsg;
            }

            //检查退回步骤设置
            if (dto.Status.Equals(2))
            {
                vResMsg = CheckReturnStepItem(dto,AInstanceStep);
                if (vResMsg.Code == 1)
                    return vResMsg;
            }
            else if (dto.Status.Equals(1))
            {
                //检查跳转步骤设置
                vResMsg = CheckTransferStepItem(dto,vStepEntity, vTempStep);
                if (vResMsg.Code == 1)
                    return vResMsg;

                //检查下一步处理人是否填写正确
                if (!vResMsg.Data.Equals("HasCheckUser"))
                {
                    vResMsg = CheckFixNextUser(dto,vStepEntity, vTempStep);
                    if (vResMsg.Code == 1)
                        return vResMsg;
                }
            }
            vResMsg.Code = 0;
            return vResMsg;
        }

        protected R<string> SaveUserTask(DealStepDTO dto, InstanceFlow AInstanceFlow, InstanceStepDTO AInstanceStep)
        {
            R<string> vResMsg = new R<string>();
            vResMsg.Code = 1;
            InstanceStepUser vStepUser = (from InstanceStepUser vItem in AInstanceStep.InstanceStep.InstanceStepUsers
                                               where vItem.UserCode.Equals(dto.UserCode)
                                               select vItem).FirstOrDefault();

            if (vStepUser == null)
            {
                vResMsg.Msg = "没有找到下一步骤人";
                return vResMsg;
            }
                

            InstanceStepUser vTempStepUser = vStepUser.Clone() as InstanceStepUser;
            vTempStepUser.Status = dto.Status;
            vTempStepUser.Content = dto.Content;
            vTempStepUser.FormName = dto.Form_Name;
            vTempStepUser.Data = dto.Data;
            vTempStepUser.Editor = dto.UserCode;
            vTempStepUser.EditDate = DateTime.Now;
            vResMsg.Code = _instanceStepUserService.Update(vTempStepUser)> 0?0:1;
            vResMsg.Msg = _instanceStepUserService.Update(vTempStepUser) > 0 ? "跟新步骤用户成功" : "跟新步骤用户出错";

            if (vResMsg.Code == 0)
            {
                //vTempStepUser.SetMultiple(vStepUser.GetMultiple());
                AInstanceStep.InstanceStep.InstanceStepUsers.Remove(vStepUser);
                AInstanceStep.InstanceStep.InstanceStepUsers.Add(vTempStepUser);
            }
            vResMsg.Code = 0;
            return vResMsg;
        }

        protected R<string> SaveNextStep(DealStepDTO dto, InstanceFlow AInstanceFlow, InstanceStepDTO AInstanceStep)
        {
            R<string> vResMsg = new R<string>();

            vResMsg.Code = 1;
            //如果是空,则以正确方式结束
            if (string.IsNullOrWhiteSpace(dto.NextStepSeq))
                return vResMsg;

            //如果不是(1通过,2退回),则以正确方式结束
            if (!new int[] { 1, 2 }.Contains(dto.Status))
                return vResMsg;

            //根据步骤序号取得步骤ID
            var vList = from TemplateStep vItem in AInstanceFlow.TemplateSteps
                        where vItem.Seq.ToString().Equals(dto.NextStepSeq)
                        select vItem.Fid;

            if (!vList.Any())
                return vResMsg;

            string vID = vList.FirstOrDefault();

            //通过
            if (dto.Status == 1)
            {
                //检查是否已加入到列表
                var vObj = (from InstanceStepFixstep vItem in AInstanceStep.InstanceStep.InstanceStepFixsteps
                            where vItem.SelectStepId.Equals(vID)
                            select vItem).FirstOrDefault();

                if (vObj == null)
                {
                    //删除步骤下已有的指定下一步骤信息
                    vResMsg = RemoveNextStep(AInstanceStep);
                    if (vResMsg.Code == 1)
                        return vResMsg;

                   InstanceStepFixstep   vEntity = new InstanceStepFixstep();
                    vEntity.Fid = Guid.NewGuid().ToString();
                    vEntity.InstanceStepId = AInstanceStep.InstanceStep.Fid;
                    vEntity.SelectStepId = vID;
                    vEntity.Creator = dto.UserCode;
                    vEntity.CreateDate = DateTime.Now;
                    vEntity.Editor = vEntity.Creator;
                    vEntity.EditDate = vEntity.CreateDate;

                    // vEntity.SetMultiple(false);
                    //vResMsg = vEntity.Insert();
                    var result = _instanceStepFixstepService.Add(vEntity);
                    if (result > 0)
                    {
                        vResMsg.Code = 0;
                        AInstanceStep.InstanceStep.InstanceStepFixsteps.Add(vEntity);
                    }
                        
                    else
                    {
                        vResMsg.Msg = string.Format("流程实例步骤:{0}增加出错", vEntity.InstanceStepId);
                    } 
                        
                }
            }
            else//退回
            {
                //检查是否已加入到列表
                var vObj = (from InstanceStepReturnstep vItem in AInstanceStep.InstanceStep.InstanceStepReturnsteps
                            where vItem.SelectStepId.Equals(vID)
                            select vItem).FirstOrDefault();

                if (vObj == null)
                {
                    //删除步骤下已有的退回步骤信息
                    vResMsg = RemoveReturnStep(AInstanceStep);
                    if (vResMsg.Code == 1)
                        return vResMsg;

                    InstanceStepReturnstep vEntity = new InstanceStepReturnstep();
                    vEntity.Fid = Guid.NewGuid().ToString();
                    vEntity.InstanceStepId = AInstanceStep.InstanceStep.Fid;
                    vEntity.SelectStepId = vID;
                    vEntity.Creator = dto.UserCode;
                    vEntity.CreateDate = DateTime.Now;
                    vEntity.Editor = vEntity.Creator;
                    vEntity.EditDate = vEntity.CreateDate;

                    //vEntity.SetMultiple(false);
                    //vResMsg = vEntity.Insert();
                    var result = _instanceStepReturnstepService.Add(vEntity);
                    if (result > 0)
                    {
                        vResMsg.Code = 0;
                        AInstanceStep.InstanceStep.InstanceStepReturnsteps.Add(vEntity);
                        
                    } else
                    {
                        vResMsg.Msg = string.Format("流程实例步骤:{0}退回出错", vEntity.InstanceStepId);
                    }
                        
                }
            }

            return vResMsg;
        }

        protected R<string> RemoveNextStep(InstanceStepDTO AInstanceStep)
        {
            //建立条件
            //ModelCondition vCdt = new ModelCondition("INSTANCE_STEP_ID", CompareTypeOptions.Equal, AInstanceStep.InstanceStepEntity.FID.ToString());
            //删除指定的下一个步骤
            //InstanceStepFixstep vFixStep = new InstanceStepFixstep();
            //R<string> vResMsg = vFixStep.Remove(vCdt);
            var result =_instanceStepFixstepService.DeleteByInstanceStepId(AInstanceStep.InstanceStep.Fid);
            R<string> vResMsg = new R<string>();
            vResMsg.Code = 1;
            if (result >0)
            {
                vResMsg.Msg = "删除指定的下一个步骤失败";
                vResMsg.Code = 0;
            }
                

            return vResMsg;
        }

        protected R<string> RemoveReturnStep(InstanceStepDTO AInstanceStep)
        {
            //建立条件
            //ModelCondition vCdt = new ModelCondition("INSTANCE_STEP_ID", CompareTypeOptions.Equal, AInstanceStep.InstanceStepEntity.FID.ToString());
            //删除退回步骤
            //InstanceStepReturnstep  vReturnStep = new InstanceStepReturnstep();
            //R<string> vResMsg = vReturnStep.Remove(vCdt);
            //if (!vResMsg.Success)
            //    vResMsg.Msg = "删除退回步骤失败";

            //return vResMsg;
            var result = _instanceStepReturnstepService.DeleteByInstanceStepId(AInstanceStep.InstanceStep.Fid);
            R<string> vResMsg = new R<string>();
            vResMsg.Code = 1;
            if (result > 0)
            {
                vResMsg.Msg = "删除指定的下一个步骤失败";
                vResMsg.Code = 0;
            }
            return vResMsg;
        }

        protected R<string> SaveNextUsers(DealStepDTO dto, InstanceFlow AInstanceFlow, InstanceStepDTO AInstanceStep)
        {
            R<string> vResMsg = new R<string>();
            vResMsg.Code = 1;
            //如果是空,则以正确方式结束
            if (dto.NextUserList == null || dto.NextUserList.Count <= 0)
                return vResMsg;

            //删除步骤下已存在的下一步处理人信息
            vResMsg = RemoveNextUsers(AInstanceStep);
            if (vResMsg.Code != 0)
                return vResMsg;

            //加入下一步处理人信息
            foreach (USERINFO vUser in dto.NextUserList)
            {
                if (AInstanceStep.InstanceStep.InstanceStepNextstepUsers.Exists(AItem => AItem.UserCode.Equals(vUser.UserCode)))
                    continue;

                InstanceStepNextstepUser vNextStepUser = new InstanceStepNextstepUser();
                vNextStepUser.Fid = Guid.NewGuid().ToString();
                vNextStepUser.InstanceStepId = AInstanceStep.InstanceStep.Fid;
                vNextStepUser.UserCode = vUser.UserCode;
                vNextStepUser.UserType = vUser.UserType;
                vNextStepUser.Creator = dto.UserCode;
                vNextStepUser.CreateDate = DateTime.Now;
                vNextStepUser.Editor = vNextStepUser.Creator;
                vNextStepUser.EditDate = vNextStepUser.CreateDate;

                //vNextStepUser.SetMultiple(false);
                var result = _instanceStepNextstepUserService.Add(vNextStepUser);
                //vResMsg = vNextStepUser.Insert();
                if (result > 1)
                {
                    AInstanceStep.InstanceStep.InstanceStepNextstepUsers.Add(vNextStepUser);
                    vResMsg.Code = 0;
                }
                else
                    break;
            }

            return vResMsg;
        }

        protected R<string> RemoveNextUsers(InstanceStepDTO AInstanceStep)
        {
            return null;
            //UF_INSTANCE_STEP_NEXTSTEP_USER vNextUser = new UF_INSTANCE_STEP_NEXTSTEP_USER();
            //return vNextUser.Remove(new ModelCondition("INSTANCE_STEP_ID", CompareTypeOptions.Equal, AInstanceStep.InstanceStepEntity.FID.ToString()));
        }

        protected R<string> SaveTaskInFirst()
        {
            return null;
        }

        private R<string> CheckReturnStepItem(DealStepDTO dto, InstanceStepDTO AInstanceStep)
        {
            R<string> vResMsg = new R<string>();
            vResMsg.Code = 1;
            if (string.IsNullOrWhiteSpace(dto.NextStepSeq))
            {
                vResMsg.Msg = "退回时必须指定要退回的步骤";
                return vResMsg;
            }

            List<TemplateStepFixstepList> vList = LoadNextStepOptions(AInstanceStep);

            var vItems = from TemplateStepFixstepList vItem in vList
                         where vItem.IsReturn && vItem.SelectStepSeq.ToString().Equals(dto.NextStepSeq)
                         select vItem;

            if (!vItems.Any())
            {
                vResMsg.Msg = "指定要退回的步骤与流程要求的步骤不符";
                return vResMsg;
            }

            vResMsg.Code = 0;
            return vResMsg;
        }

        private List<TemplateStepFixstepList> LoadNextStepOptions(InstanceStepDTO AInstanceStep)
        {
            List<TemplateStepFixstepList> vList = AInstanceStep.TemplateStep.TemplateStepFixstepLists.ConvertAll<TemplateStepFixstepList>(AItem => AItem.Clone() as TemplateStepFixstepList);
            //根据退回设置，加入发起步骤，作为退回的可选项
            if (AInstanceStep.TemplateStep.IsFormReturnFirst)
            {
                TemplateStep vStepModel = AInstanceStep.GerFirstTemplateStep();
                if (vStepModel != null)
                    AddReturnOptions(vList, vStepModel, vStepModel.StepName + "(退回)");
            }
            //根据退回设置，加入上一个有人员处理的完成步骤，作为退回的可选项
            if (AInstanceStep.TemplateStep.IsFormReturn)
            {
                TemplateStep vStepModel = AInstanceStep.GetPreviousHasUserDealTemplateStep();
                if (vStepModel != null)
                    AddReturnOptions(vList, vStepModel, vStepModel.StepName + "(退回)");
            }
            //根据退回设置，加入有人员处理的所有完成步骤，作为退回的可选项
            if (AInstanceStep.TemplateStep.IsFormReturnFinish)
            {
                //取得所有通过的步骤实例，且要有人处理
                List<InstanceStep> vEntityList = (from InstanceStep vEntity in AInstanceStep.InstanceFlow.InstanceSteps
                                                      where vEntity.Status.Equals(6) && vEntity.InstanceStepUsers.Exists(AItem => (AItem as InstanceStepUser).Status.Equals(1))
                                                      select vEntity).ToList();

                if (vEntityList.Any())
                {
                    //取得步骤实例对应的步骤模板
                    var vItems = from TemplateStep vModel in AInstanceStep.TemplateFlow.TemplateSteps
                                 where vEntityList.Exists(AItem => AItem.StepId.Equals(vModel.Fid))
                                 select vModel;

                    foreach (TemplateStep vStepModel in vItems)
                    {
                        AddReturnOptions(vList, vStepModel, vStepModel.StepName + "(退回)");
                    }
                }
            }
            return vList;
        }

        private void AddReturnOptions(List<TemplateStepFixstepList> AList, TemplateStep AStepModel, string AStepName = "")
        {
            if (AStepModel == null || AList == null)
                return;

            if (!CheckHasExistReturnOptions(AList, AStepModel))
            {
                TemplateStepFixstepList vModel = new TemplateStepFixstepList();
                vModel.Fid = Guid.NewGuid().ToString();
                vModel.StepId = AStepModel.Fid;
                vModel.SelectStepSeq = AStepModel.Seq;
                vModel.SelectStepName = string.IsNullOrWhiteSpace(AStepName) ? AStepModel.StepName : AStepName;
                vModel.IsReturn = true;
                vModel.Remark = AStepModel.Seq.ToString() + "." + AStepModel.StepName;
                AList.Add(vModel);
            }
        }

        /// <summary>
        /// 检测是否存在退回选项
        /// </summary>
        /// <param name="AList">选项列表</param>
        /// <param name="AStepModel">要检测的步骤对象</param>
        /// <returns></returns>
        private bool CheckHasExistReturnOptions(List<TemplateStepFixstepList> AList, TemplateStep AStepModel)
        {
            bool vRes = false;

            if (AList == null || AList.Count <= 0)
                return vRes;

            if (AStepModel == null)
                return vRes;

            return AList.Exists(AItem => (AItem as TemplateStepFixstepList).IsReturn && (AItem as TemplateStepFixstepList).SelectStepSeq.Equals(AStepModel.Seq));
        }


        /// <summary>
        /// 检查通过情况下跳转步骤设置
        /// </summary>
        /// <param name="dto">步骤信息</param>
        /// <param name="AStepEntity">步骤实例</param>
        /// <param name="ATempStep">模板步骤实例</param>
        /// <returns></returns>
        private R<string> CheckTransferStepItem(DealStepDTO dto, InstanceStep AStepEntity, TemplateStep ATempStep)
        {
            R<string> vResMsg = new R<string>();

            //不可填写的，但却填写了的
            if ((!ATempStep.IsFormNextstep || ATempStep.IsFormNextstep && ATempStep.TemplateStepFixstepLists.Count <= 0)
                && !string.IsNullOrWhiteSpace(dto.NextStepSeq))
            {
                vResMsg.Msg = "流程没有要求选择的下一个处理步骤，但此处却填写了!";
                return vResMsg;
            }

            //要求必须填写的，但却没有填写的
            if (ATempStep.IsFormNextstep && ATempStep.IsFormNextstepMust && ATempStep.TemplateStepFixstepLists.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(dto.NextStepSeq))
                {
                    vResMsg.Msg = "未接收到流程要求选择的下一个处理步骤!";
                    return vResMsg;
                }
            }

            //填写过的
            if (!string.IsNullOrWhiteSpace(dto.NextStepSeq))
            {
                TemplateStepFixstepList vFindTempFixStep = (from TemplateStepFixstepList vItem in ATempStep.TemplateStepFixstepLists
                                                            where !vItem.IsReturn && vItem.SelectStepSeq.ToString().Equals(dto.NextStepSeq)
                                                                  select vItem).FirstOrDefault();
                if (vFindTempFixStep == null)
                {
                    vResMsg.Msg = "接收到的下一个处理步骤与流程要求的跳转步骤不符!";
                    return vResMsg;
                }

                //检查跳转下的处理人
                var vList = from InstanceStepFixSeluser  vFixUser in AStepEntity.InstanceStepFixSelusers
                            where vFixUser.FixstepId.Equals(vFindTempFixStep.Fid)
                            select vFixUser;

                //必须填写处理人但却没有填写的
                if (vFindTempFixStep.IsFormNextman && vFindTempFixStep.IsFormNextmanMust && vList.Any())
                {
                    if (dto.NextUserList == null || dto.NextUserList.Count <= 0)
                    {
                        vResMsg.Msg = "未接收到流程要求选择的下一步处理人!";
                        return vResMsg;
                    }
                }
                //可以填写处理人，且已填写了处理人的
                if (vFindTempFixStep.IsFormNextman && vList.Any() && dto.NextUserList != null && dto.NextUserList.Count > 0)
                {
                    List<InstanceStepFixSeluser> vNewLsit = vList.ToList();

                    var vItems = from USERINFO vSupplyUser in dto.NextUserList
                                 where !vNewLsit.Exists(AItem => AItem.UserCode.Equals(vSupplyUser.UserCode))
                                 select vSupplyUser.UserCode;

                    if (vItems.Any())
                    {
                        vResMsg.Msg = string.Format("接收到下一步处理人与流程要求选择的不符(如:{0})!", vItems.FirstOrDefault());
                        return vResMsg;
                    }

                    vResMsg.Data = "HasCheckUser";
                }
            }

            vResMsg.Code = 0;
            return vResMsg;
        }



        /// <summary>
        /// 检查通过情况下一步处理人是否填写
        /// </summary>
        /// <param name="AStepEntity">步骤实例</param>
        /// <param name="ATempStep">模板步骤实例</param>
        /// <returns></returns>
        private R<string> CheckFixNextUser(DealStepDTO dto, InstanceStep AStepEntity, TemplateStep ATempStep)
        {
            R<string> vResMsg = new R<string>();
            vResMsg.Code = 1;

            
            //要求必须填写，但却未填写
            if (ATempStep.IsFormNextman && ATempStep.IsFormNextmanMust && AStepEntity.InstanceStepNextSelusers.Count > 0)
            {
                if (dto.NextUserList == null || dto.NextUserList.Count <= 0)
                {
                    vResMsg.Msg = "未接收到流程要求选择的下一步处理人!";
                    return vResMsg;
                }
            }

            //要求非必填，但却填写了的
            if (ATempStep.IsFormNextman
                && !ATempStep.IsFormNextmanMust
                && AStepEntity.InstanceStepNextSelusers.Count > 0
                && dto.NextUserList != null
                && dto.NextUserList.Count > 0)
            {
                var vItems = from USERINFO vSupplyUser in dto.NextUserList
                             where !AStepEntity.InstanceStepNextSelusers.Exists(AItem => AItem.UserCode.Equals(vSupplyUser.UserCode))
                             select vSupplyUser.UserCode;

                if (vItems.Any())
                {
                    vResMsg.Msg = string.Format("接收到下一步处理人与流程要求选择的不符(如:{0})!", vItems.FirstOrDefault());
                    return vResMsg;
                }
            }

            vResMsg.Code = 0;
            return vResMsg;
        }
    }
}
