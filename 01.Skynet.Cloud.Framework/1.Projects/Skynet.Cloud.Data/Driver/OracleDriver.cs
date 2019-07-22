
namespace UWay.Skynet.Cloud.Data.Driver
{
    /// <summary>
    /// Oracle驱动基类
    /// </summary>
    class OracleDriver : AbstractDriver
    {

        public override char NamedPrefix
        {
            get { return ':'; }
        }

    }
}
