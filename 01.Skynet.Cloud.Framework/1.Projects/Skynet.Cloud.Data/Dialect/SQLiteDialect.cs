

namespace UWay.Skynet.Cloud.Data.Dialect
{
    partial class SQLiteDialect : Dialect
    {

        public override char OpenQuote
        {
            get { return '['; }
        }

        public override char CloseQuote
        {
            get { return ']'; }
        }








    }
}
