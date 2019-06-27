using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Linq.Impl.Internal
{
    internal class ExpressionBuilderOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether member access expression used
        /// by this builder should be lifted to null. The default value is true;
        /// </summary>
        /// <value>
        /// 	<c>true</c> if member access should be lifted to null; otherwise, <c>false</c>.
        /// </value>
        public bool LiftMemberAccessToNull { get; set; }

        public ExpressionBuilderOptions()
        {
            this.LiftMemberAccessToNull = true;
        }

        public void CopyFrom(ExpressionBuilderOptions other)
        {
            this.LiftMemberAccessToNull = other.LiftMemberAccessToNull;
        }
    }
}
