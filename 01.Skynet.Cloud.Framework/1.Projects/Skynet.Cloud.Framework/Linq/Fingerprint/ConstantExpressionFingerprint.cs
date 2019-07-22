using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    internal sealed class ConstantExpressionFingerprint : ExpressionFingerprint
    {
        // Methods
        public ConstantExpressionFingerprint(ExpressionType nodeType, Type type) : base(nodeType, type)
        {
        }

        public override bool Equals(object obj)
        {
            ConstantExpressionFingerprint fingerprint = obj as ConstantExpressionFingerprint;
            return ((fingerprint != null) && base.Equals((ExpressionFingerprint)fingerprint));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }



}
