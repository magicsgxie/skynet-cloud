
namespace UWay.Skynet.Cloud.Data.Dialect
{
    class ExcelDialect : AccessDialect
    {
        public override bool SupportDelete
        {
            get
            {
                return false;
            }
        }
        public override string Quote(string name)
        {
            return "[" + name + "$]";
        }


    }
}
