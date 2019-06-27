

namespace UWay.Skynet.Cloud.Data.Dialect
{
    partial class MySqlDialect : Dialect
    {
        public override char CloseQuote
        {
            get
            {
                return '`';
            }
        }

        public override char OpenQuote
        {
            get
            {
                return '`';
            }
        }

        public override bool SupportMultipleCommands
        {
            get { return false; }
        }

        public override bool SupportDistinctInAggregates
        {
            get { return true; }
        }

    }
}
