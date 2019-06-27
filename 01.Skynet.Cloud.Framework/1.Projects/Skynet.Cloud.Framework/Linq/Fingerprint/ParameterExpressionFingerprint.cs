using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    internal sealed class ParameterExpressionFingerprint : ExpressionFingerprint
    {
        // Methods
        public ParameterExpressionFingerprint(ExpressionType nodeType, Type type, int parameterIndex) : base(nodeType, type)
        {
            this.ParameterIndex = parameterIndex;
        }

        internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
        {
            combiner.AddInt32(this.ParameterIndex);
            base.AddToHashCodeCombiner(combiner);
        }

        public override bool Equals(object obj)
        {
            ParameterExpressionFingerprint fingerprint = obj as ParameterExpressionFingerprint;
            return (((fingerprint != null) && (this.ParameterIndex == fingerprint.ParameterIndex)) && base.Equals((ExpressionFingerprint)fingerprint));
        }

        // Properties
        public int ParameterIndex { get; private set; }
    }



}
