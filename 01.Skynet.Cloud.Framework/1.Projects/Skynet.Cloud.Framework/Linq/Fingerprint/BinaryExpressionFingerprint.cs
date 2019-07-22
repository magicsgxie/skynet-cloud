using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    internal class BinaryExpressionFingerprint : ExpressionFingerprint
    {
        public BinaryExpressionFingerprint(ExpressionType nodeType, Type type, MethodInfo method) : base(nodeType, type)
        {
            this.Method = method;
        }

        internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
        {
            combiner.AddObject(this.Method);
            base.AddToHashCodeCombiner(combiner);
        }

        public override bool Equals(object obj)
        {
            BinaryExpressionFingerprint fingerprint = obj as BinaryExpressionFingerprint;
            return (((fingerprint != null) && object.Equals(this.Method, fingerprint.Method)) && base.Equals((ExpressionFingerprint)fingerprint));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        // Properties
        public MethodInfo Method { get; private set; }

    }
}
