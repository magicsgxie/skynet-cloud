namespace UWay.Skynet.Cloud.Nacos
{
    using System.Threading;

    /// <summary>
    /// 
    /// </summary>
    public class Listener
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="timer"></param>
        public Listener(string name, Timer timer)
        {
            this.Name = name;
            this.Timer = timer;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Timer Timer { get; set; }
    }
}
