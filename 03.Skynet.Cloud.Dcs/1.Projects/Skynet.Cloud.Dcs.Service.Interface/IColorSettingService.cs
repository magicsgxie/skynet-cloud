using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;

namespace UWay.Skynet.Cloud.Dcs.Services.Interface
{
    /// <summary>
    /// 色阶设置
    /// </summary>
    public interface IColorSettingService
    {
        /// <summary>
        /// 获取色阶信息
        /// </summary>
        /// <param name="moduleId"><see cref="ColorLevelSetting.ModuleId"/></param>
        /// <param name="settingName"></param>
        /// <returns></returns>
        ColorLevelSetting GetColorSettingInfo(long moduleId, string settingName);


        /// <summary>
        /// 获取对应模版色阶数据
        /// </summary>
        /// <param name="moduleId"><see cref="ColorLevelSetting.ModuleId"/></param>
        /// <returns></returns>
        List<ColorLevelSetting> GetColorSettingInfos(long moduleId);

        /// <summary>
        /// 插入色阶
        /// </summary>
        /// <param name="setting"><see cref="ColorLevelSetting"/></param>
        ColorLevelSetting AddColorLevelSetting(ColorLevelSetting setting);

        /// <summary>
        /// 插入色阶
        /// </summary>
        /// <param name="setting"></param>
        void AddBatchColorLevelSetting(List<ColorLevelSetting> setting);



        /// <summary>
        /// 更新色阶
        /// </summary>
        /// <param name="setting"></param>
        void UpdateColorLevelSetting(ColorLevelSetting setting);

        /// <summary>
        /// 删除色阶
        /// </summary>
        /// <param name="settingName">色阶模板名称</param>
        /// <param name="moduleID">模块<see cref="ColorLevelSetting.SettingName"/></param>
        /// <returns></returns>
        int DelColorLevelSetting(string settingName, long moduleID);

        

        /// <summary>
        /// 获取色阶
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="settingName"></param>
        /// <returns></returns>
        List<ColorLevelSetting> GetColorSettingInfosByCondition(long moduleId, string settingName);
    }
}
