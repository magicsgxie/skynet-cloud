using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    internal sealed class MemberExpressionFingerprint : ExpressionFingerprint
    {
        // Methods
        public MemberExpressionFingerprint(ExpressionType nodeType, Type type, MemberInfo member) : base(nodeType, type)
        {
            this.Member = member;
        }

        internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
        {
            combiner.AddObject(this.Member);
            base.AddToHashCodeCombiner(combiner);
        }

        public override bool Equals(object obj)
        {
            MemberExpressionFingerprint fingerprint = obj as MemberExpressionFingerprint;
            return (((fingerprint != null) && object.Equals(this.Member, fingerprint.Member)) && base.Equals((ExpressionFingerprint)fingerprint));
        }

        // Properties
        public MemberInfo Member { get; private set; }
    }



}
