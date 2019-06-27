

namespace UWay.Skynet.Cloud.Data.Dialect
{
    /// <summary>
    /// 数据库方言
    /// </summary>
    abstract partial class Dialect : UWay.Skynet.Cloud.Data.Dialect.IDialect
    {
        public virtual bool SupportInsert { get { return true; } }
        public virtual bool SupportDelete { get { return true; } }
        public virtual bool SupportUpdate { get { return true; } }
        public virtual bool SupportSelect { get { return true; } }

        public virtual bool SupportSchema { get { return false; } }

        public virtual bool SupportMultipleCommands { get { return false; } }
        public virtual bool SupportSubqueryInSelectWithoutFrom { get { return false; } }
        public virtual bool SupportDistinctInAggregates { get { return false; } }

        public virtual char CloseQuote { get { return ']'; } }
        public virtual char OpenQuote { get { return '['; } }

        public virtual string Quote(string name)
        {
            return OpenQuote + name + CloseQuote;
        }

    }
}
