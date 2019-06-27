using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    internal sealed class UnaryExpressionFingerprint : ExpressionFingerprint
    {
        // Methods
        public UnaryExpressionFingerprint(ExpressionType nodeType, Type type, MethodInfo method) : base(nodeType, type)
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
            UnaryExpressionFingerprint fingerprint = obj as UnaryExpressionFingerprint;
            return (((fingerprint != null) && object.Equals(this.Method, fingerprint.Method)) && base.Equals((ExpressionFingerprint)fingerprint));
        }

        // Properties
        public MethodInfo Method { get; private set; }
    }



}
