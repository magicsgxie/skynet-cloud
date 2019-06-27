
namespace UWay.Skynet.Cloud.Data.Dialect
{
    partial class OracleDialect : Dialect
    {

        public override bool SupportMultipleCommands
        {
            get { return false; }
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
                return '\"';
            }
        }

        public override char CloseQuote
        {
            get
            {
                return '\"';
            }
        }



        public override string Quote(string name)
        {
            return OpenQuote + name.ToUpper() + CloseQuote;
        }
    }
}
