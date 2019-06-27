using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq
{
    internal class HashCodeCombiner
    {
        // Fields
        private long _combinedHash64 = 0x1505;

        // Methods
        public void AddEnumerable(IEnumerable e)
        {
            if (e == null)
            {
                this.AddInt32(0);
            }
            else
            {
                int i = 0;
                foreach (object obj2 in e)
                {
                    this.AddObject(obj2);
                    i++;
                }
                this.AddInt32(i);
            }
        }

        public void AddFingerprint(ExpressionFingerprint fingerprint)
        {
            if (fingerprint != null)
            {
                fingerprint.AddToHashCodeCombiner(this);
            }
            else
            {
                this.AddInt32(0);
            }
        }

        public void AddInt32(int i)
        {
            this._combinedHash64 = ((this._combinedHash64 << 5) + this._combinedHash64) ^ i;
        }

        public void AddObject(object o)
        {
            int i = (o != null) ? o.GetHashCode() : 0;
            this.AddInt32(i);
        }

        // Properties
        public int CombinedHash
        {
            get
            {
                return this._combinedHash64.GetHashCode();
            }
        }
    }



}