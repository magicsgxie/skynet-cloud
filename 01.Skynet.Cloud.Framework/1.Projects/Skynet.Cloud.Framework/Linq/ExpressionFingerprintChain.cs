using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    internal sealed class ExpressionFingerprintChain : IEquatable<ExpressionFingerprintChain>
    {
        // Fields
        public readonly List<ExpressionFingerprint> Elements = new List<ExpressionFingerprint>();

        // Methods
        public override bool Equals(object obj)
        {
            return this.Equals(obj as ExpressionFingerprintChain);
        }

        public bool Equals(ExpressionFingerprintChain other)
        {
            if (other == null)
            {
                return false;
            }
            if (this.Elements.Count != other.Elements.Count)
            {
                return false;
            }
            for (int i = 0; i < this.Elements.Count; i++)
            {
                if (!object.Equals(this.Elements[i], other.Elements[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            HashCodeCombiner combiner = new HashCodeCombiner();
            this.Elements.ForEach(new Action<ExpressionFingerprint>(combiner.AddFingerprint));
            return combiner.CombinedHash;
        }
    }



}
