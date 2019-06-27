using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    internal sealed class ConditionalExpressionFingerprint : ExpressionFingerprint
    {
        // Methods
        public ConditionalExpressionFingerprint(ExpressionType nodeType, Type type) : base(nodeType, type)
        {
        }

        public override bool Equals(object obj)
        {
            ConditionalExpressionFingerprint fingerprint = obj as ConditionalExpressionFingerprint;
            return ((fingerprint != null) && base.Equals((ExpressionFingerprint)fingerprint));
        }
    }


}
