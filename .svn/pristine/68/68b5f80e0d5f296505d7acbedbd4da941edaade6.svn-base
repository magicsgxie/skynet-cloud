using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    internal abstract class ExpressionFingerprint
    {
        // Methods
        protected ExpressionFingerprint(ExpressionType nodeType, Type type)
        {
            this.NodeType = nodeType;
            this.Type = type;
        }

        internal virtual void AddToHashCodeCombiner(HashCodeCombiner combiner)
        {
            combiner.AddInt32((int)this.NodeType);
            combiner.AddObject(this.Type);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ExpressionFingerprint);
        }

        protected bool Equals(ExpressionFingerprint other)
        {
            return (((other != null) && (this.NodeType == other.NodeType)) && object.Equals(this.Type, other.Type));
        }

        public override int GetHashCode()
        {
            HashCodeCombiner combiner = new HashCodeCombiner();
            this.AddToHashCodeCombiner(combiner);
            return combiner.CombinedHash;
        }

        // Properties
        public ExpressionType NodeType { get; private set; }

        public Type Type { get; private set; }
    }



}
