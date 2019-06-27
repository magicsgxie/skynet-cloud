
namespace UWay.Skynet.Cloud.Data.Dialect
{
    partial class FirebirdDialect : Dialect
    {
        public override bool SupportMultipleCommands
        {
            get { return true; }
        }

        public override bool SupportSubqueryInSelectWithoutFrom
        {
            get { return true; }
        }

        public override bool SupportDistinctInAggregates
        {
            get { return true; }
        }



        public override char OpenQuote
        {
            get
            {
                return '"';
            }
        }

        public override char CloseQuote
        {
            get
            {
                return '"';
            }
        }


    }
}
