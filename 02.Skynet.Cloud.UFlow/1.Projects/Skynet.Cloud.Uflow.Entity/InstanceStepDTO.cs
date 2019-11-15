using System;
using System.Collections.Generic;
using System.Linq;

namespace UWay.Skynet.Cloud.Uflow.Entity
{
    public class InstanceStepDTO
    {
        /// <summary>
        /// 流程模板对象
        /// </summary>
        public TemplateFlow TemplateFlow { set; get; }

        /// <summary>
        /// 流程实例对象
        /// </summary>
        public InstanceFlow InstanceFlow { set; get; }

        /// <summary>
        /// 步骤模板对象
        /// </summary>
        public TemplateStep TemplateStep { set; get; }

        /// <summary>
        /// 步骤实例对象
        /// </summary>
        public InstanceStep InstanceStep { set; get; }

        /// <summary>
        /// 取得发起步骤的步骤模板的实体
        /// </summary>
        /// <returns></returns>
        public TemplateStep GerFirstTemplateStep(InstanceStep AInstanceStepEntity = null)
        {
            InstanceStep vNewInstanceStepEntity = AInstanceStepEntity;
            if (vNewInstanceStepEntity == null)
            {
                vNewInstanceStepEntity = GerFirstInstanceStep();
            }

            if (vNewInstanceStepEntity == null)
                return null;

            return GetTemplateStep(vNewInstanceStepEntity.StepId);
        }


        /// <summary>
        /// 取得发起步骤的步骤实例的实体
        /// </summary>
        /// <returns></returns>
        public InstanceStep GerFirstInstanceStep()
        {
            var vList = from InstanceStep vStep in InstanceFlow.InstanceSteps
                        orderby vStep.CreateDate ascending
                        select vStep;

            if (vList == null || !vList.Any())
                return null;
            else
                return vList.First() as InstanceStep;
        }

        /// <summary>
        /// 取得上一个有人处理的步骤模板实体，按已走步骤的顺序来确定
        /// </summary>
        /// <returns></returns>
        public TemplateStep GetPreviousHasUserDealTemplateStep()
        {
            TemplateStep vRes = null;
            List<TemplateStep> vList = GetTemplateStepOrder();
            if (!vList.Any())
                return vRes;

            vList.Reverse();

            TemplateStep vStep = vList.FirstOrDefault(AItem => AItem.Fid.Equals(InstanceStep.StepId));
            if (vStep == null)
            {
                vRes = vList.LastOrDefault();
            }
            else
            {
                int vIndex = vList.IndexOf(vStep);
                vIndex--;
                if (vIndex >= 0)
                    vRes = vList.ElementAt(vIndex);
            }
            return vRes;
        }

        /// <summary>
        /// 取得由已走步骤生成的步骤模板顺序(由前到后)
        /// </summary>
        /// <returns></returns>
        private List<TemplateStep> GetTemplateStepOrder()
        {
            List<TemplateStep> vList = new List<TemplateStep>(10);
            InstanceStep vStepEntity = InstanceStep;
            TemplateStep vStepModel = null;
            do
            {
                vStepEntity = GetInstanceStep(vStepEntity.PreviousInstanceStepId);
                if (vStepEntity == null)
                    break;
                if (vStepEntity.Status.Equals(6))
                {
                    vStepModel = GetTemplateStep(vStepEntity.StepId);
                    if (vStepModel != null && !vList.Exists(AItem => AItem.Fid.Equals(vStepModel.Fid)))
                        vList.Add(vStepModel);
                }
            } while (vStepEntity != null && (vStepModel == null || vStepModel != null && !vStepModel.StepType.Equals(0)));
            return vList;
        }

        /// <summary>
        /// 取得实例步骤的实体
        /// </summary>
        /// <param name="AInstanceStepID"></param>
        /// <returns></returns>
        public InstanceStep GetInstanceStep(string AInstanceStepID)
        {
            var vList = from InstanceStep vStep in InstanceFlow.InstanceSteps
                        where vStep.Fid.Equals(AInstanceStepID)
                        select vStep;

            if (vList == null || !vList.Any())
                return null;
            else
                return vList.First() as InstanceStep;
        }


        /// <summary>
        /// 取得模板步骤的实体
        /// </summary>
        /// <param name="ATemplateStepID"></param>
        /// <returns></returns>
        public TemplateStep GetTemplateStep(string ATemplateStepID)
        {
            var vList = from TemplateStep vStep in TemplateFlow.TemplateSteps
                        where vStep.Fid.Equals(ATemplateStepID)
                        select vStep;

            if (vList == null || !vList.Any())
                return null;
            else
                return vList.First() as TemplateStep;
        }

    }
}
