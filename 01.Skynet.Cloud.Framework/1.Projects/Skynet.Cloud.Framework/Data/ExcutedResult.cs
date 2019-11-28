namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class ExcutedResult
    {
        /// <summary>
        /// 
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object rows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="msg"></param>
        /// <param name="rows"></param>
        public ExcutedResult(bool success, string msg, object rows)
        {
            this.success = success;
            this.msg = msg;
            this.rows = rows;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ExcutedResult SuccessResult(string msg = null)
        {
            return new ExcutedResult(true, msg, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static ExcutedResult SuccessResult(object rows)
        {
            return new ExcutedResult(true, null, rows);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ExcutedResult FailedResult(string msg)
        {
            return new ExcutedResult(false, msg, null);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PaginationResult : ExcutedResult
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageIndex { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int pageCount => total % pageSize == 0 ? total / pageSize : total / pageSize + 1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="msg"></param>
        /// <param name="rows"></param>
        public PaginationResult(bool success, string msg, object rows) : base(success, msg, rows)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <param name="size"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static PaginationResult PagedResult(object rows, int total, int size, int index)
        {
            return new PaginationResult(true, null, rows)
            {
                total = total,
                pageSize = size,
                pageIndex = index
            };
        }
    }
}
