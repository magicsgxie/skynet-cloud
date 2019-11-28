using System;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// Represnts a Join between two tables.
    /// </summary>
    [Serializable]
    public  class Join
    {
        FromTerm leftTable, rightTable;
        //string leftField, rightField;
        WhereClause conditions;
        JoinType type;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="leftTable"></param>
        /// <param name="rightTable"></param>
        /// <param name="conditions"></param>
        /// <param name="type"></param>
        public Join(FromTerm leftTable, FromTerm rightTable, WhereClause conditions, JoinType type)
        {
            this.leftTable = leftTable;
            this.rightTable = rightTable;
            //			this.leftField = leftField;
            //			this.rightField = rightField;
            this.conditions = conditions;
            this.type = type;
        }

        /// <summary>
        /// 
        /// </summary>
        public FromTerm LeftTable
        {
            get { return this.leftTable; }
        }

        /*		public string LeftField
                {
                    get { return this.leftField; }
                }
                public string RightField
                {
                    get { return this.rightField; }
                }
        */
        /// <summary>
        /// 
        /// </summary>
        public WhereClause Conditions
        {
            get { return conditions; }
        }

        /// <summary>
        /// 
        /// </summary>
        public FromTerm RightTable
        {
            get { return this.rightTable; }
        }

        /// <summary>
        /// 
        /// </summary>
        public JoinType Type
        {
            get { return this.type; }
        }
    }
}
