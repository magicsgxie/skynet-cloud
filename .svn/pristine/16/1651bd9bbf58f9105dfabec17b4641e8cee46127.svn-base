using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    internal sealed class DefaultExpressionFingerprint : ExpressionFingerprint
    {
        // Methods
        public DefaultExpressionFingerprint(ExpressionType nodeType, Type type) : base(nodeType, type)
        {
        }

        public override bool Equals(object obj)
        {
            DefaultExpressionFingerprint fingerprint = obj as DefaultExpressionFingerprint;
            return ((fingerprint != null) && base.Equals((ExpressionFingerprint)fingerprint));
        }
    }


}
