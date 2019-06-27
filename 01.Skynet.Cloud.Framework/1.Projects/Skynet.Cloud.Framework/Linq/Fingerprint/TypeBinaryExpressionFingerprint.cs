using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    internal sealed class TypeBinaryExpressionFingerprint : ExpressionFingerprint
    {
        // Methods
        public TypeBinaryExpressionFingerprint(ExpressionType nodeType, Type type, Type typeOperand) : base(nodeType, type)
        {
            this.TypeOperand = typeOperand;
        }

        internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
        {
            combiner.AddObject(this.TypeOperand);
            base.AddToHashCodeCombiner(combiner);
        }

        public override bool Equals(object obj)
        {
            TypeBinaryExpressionFingerprint fingerprint = obj as TypeBinaryExpressionFingerprint;
            return (((fingerprint != null) && object.Equals(this.TypeOperand, fingerprint.TypeOperand)) && base.Equals((ExpressionFingerprint)fingerprint));
        }

        // Properties
        public Type TypeOperand { get; private set; }
    }



}
