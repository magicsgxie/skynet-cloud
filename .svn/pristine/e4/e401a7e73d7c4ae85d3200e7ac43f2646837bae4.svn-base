using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    internal sealed class IndexExpressionFingerprint : ExpressionFingerprint
    {
        // Methods
        public IndexExpressionFingerprint(ExpressionType nodeType, Type type, PropertyInfo indexer) : base(nodeType, type)
        {
            this.Indexer = indexer;
        }

        internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
        {
            combiner.AddObject(this.Indexer);
            base.AddToHashCodeCombiner(combiner);
        }

        public override bool Equals(object obj)
        {
            IndexExpressionFingerprint fingerprint = obj as IndexExpressionFingerprint;
            return (((fingerprint != null) && object.Equals(this.Indexer, fingerprint.Indexer)) && base.Equals((ExpressionFingerprint)fingerprint));
        }

        // Properties
        public PropertyInfo Indexer { get; private set; }
    }



}
