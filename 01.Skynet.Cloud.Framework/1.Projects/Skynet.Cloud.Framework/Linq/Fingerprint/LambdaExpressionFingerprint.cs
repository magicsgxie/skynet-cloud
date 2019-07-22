using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    internal sealed class LambdaExpressionFingerprint : ExpressionFingerprint
    {
        // Methods
        public LambdaExpressionFingerprint(ExpressionType nodeType, Type type) : base(nodeType, type)
        {
        }

        public override bool Equals(object obj)
        {
            LambdaExpressionFingerprint fingerprint = obj as LambdaExpressionFingerprint;
            return ((fingerprint != null) && base.Equals((ExpressionFingerprint)fingerprint));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }


}
