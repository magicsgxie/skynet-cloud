using System;
using System.Collections.Generic;
using System.Text;

namespace Steeltoe.Discovery.Nacos.Discovery
{
    public class NacosHeartbeatOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether heartbeats are enabled, defaults true
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the time to live heartbeat time, defaults 30
        /// </summary>
        public int TtlValue { get; set; } = 30;

        /// <summary>
        /// Gets or sets the time unit of the TtlValue, defaults = "s"
        /// </summary>
        public string TtlUnit { get; set; } = "s";

        /// <summary>
        /// Gets or sets the interval ratio
        /// </summary>
        public double IntervalRatio { get; set; } = 2.0 / 3.0;

        /// <summary>
        /// Gets the time to live setting
        /// </summary>
        public string Ttl
        {
            get
            {
                return TtlValue.ToString() + TtlUnit;
            }
        }

        public TimeSpan ComputeHearbeatInterval()
        {
            // heartbeat rate at ratio * ttl, but no later than ttl -1s and, (under lesser priority),
            // no sooner than 1s from now
            double interval = TtlValue * IntervalRatio;
            double max = Math.Max(interval, 1);
            int ttlMinus1 = TtlValue - 1;
            double min = Math.Min(ttlMinus1, max);
            double heartbeatInterval = (int)Math.Round(1000 * min);
            return TimeSpan.FromMilliseconds(heartbeatInterval);
        }
    }
}
