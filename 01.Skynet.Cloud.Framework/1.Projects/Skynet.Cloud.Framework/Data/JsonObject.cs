using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Collections
{
    using System.Collections.Generic;

    /// <summary>
    /// JSON Object
    /// </summary>
    public abstract class JsonObject
    {
        public IDictionary<string, object> ToJson()
        {
            var json = new Dictionary<string, object>();

            Serialize(json);

            return json;
        }

        protected abstract void Serialize(IDictionary<string, object> json);
    }
}
