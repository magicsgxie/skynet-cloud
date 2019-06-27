using System;
using UWay.Skynet.Cloud.Data.Dialect;
using UWay.Skynet.Cloud.Data.Driver;
using UWay.Skynet.Cloud.Data.Schema;
using UWay.Skynet.Cloud.Data.Schema.Script;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 数据库配置信息类
    /// </summary>
    public class DbConfigurationInfo
    {
        /// <summary>
        /// 得到或设置数据库驱动
        /// </summary>
        public IDriver Driver { get; set; }

        /// <summary>
        /// 得到或设置方言
        /// </summary>
        public IDialect Dialect { get; set; }

        /// <summary>
        /// 得到或设置函数注册表
        /// </summary>
        public IFunctionRegistry FuncRegistry { get; set; }
        /// <summary>
        /// 得到或设置DbExpressionBuilder
        /// </summary>
        public IDbExpressionBuilder DbExpressionBuilder { get; set; }

        /// <summary>
        /// 得到或设置SqlBuilder工厂
        /// </summary>
        public Func<IDialect, IFunctionRegistry, ISqlBuilder> SqlBuilder { get; set; }
        /// <summary>
        /// 得到或设置ScriptGenerator工厂
        /// </summary>
        public Func<IDatabaseScriptGenerator> ScriptGenerator { get; set; }
        /// <summary>
        /// 得到或设置ScriptExecutor工厂
        /// </summary>
        public Func<IDatabaseScriptExecutor> ScriptExecutor { get; set; }
        /// <summary>
        /// 得到或设置SchemaLoader工厂
        /// </summary>
        public Func<ISchemaLoader> SchemaLoader { get; set; }

    }
}
