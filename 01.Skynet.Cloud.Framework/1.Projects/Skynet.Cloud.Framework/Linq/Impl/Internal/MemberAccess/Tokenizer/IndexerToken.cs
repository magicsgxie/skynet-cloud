namespace UWay.Skynet.Cloud.Linq.Impl.Internal
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    

    internal class IndexerToken : IMemberAccessToken
    {
        private readonly ReadOnlyCollection<object> arguments;

        public IndexerToken(IEnumerable<object> arguments)
        {
            this.arguments = arguments.ToReadOnly();
        }

        public IndexerToken(params object[] arguments) : this((IEnumerable<object>) arguments)
        {
        }

        public ReadOnlyCollection<object> Arguments
        {
            get
            {
                return this.arguments;
            }
        }
    }
}