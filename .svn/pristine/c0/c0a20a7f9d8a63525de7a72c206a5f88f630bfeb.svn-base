using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.ComponentModel
{
    /// <summary>
    /// 
    /// </summary>
    [DebuggerDisplay("Name={Name},NewValue={NewValue},OldValue={OldValue}")]
    public class PropertyChangedEventArgs : System.ComponentModel.PropertyChangedEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public object NewValue { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public object OldValue { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public PropertyChangedEventArgs(string propertyName, object oldValue, object newValue) : base(propertyName)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }
    }
}
