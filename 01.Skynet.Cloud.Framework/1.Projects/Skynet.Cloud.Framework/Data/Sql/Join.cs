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

        public Join(FromTerm leftTable, FromTerm rightTable, WhereClause conditions, JoinType type)
        {
            this.leftTable = leftTable;
            this.rightTable = rightTable;
            //			this.leftField = leftField;
            //			this.rightField = rightField;
            this.conditions = conditions;
            this.type = type;
        }

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
        public WhereClause Conditions
        {
            get { return conditions; }
        }

        public FromTerm RightTable
        {
            get { return this.rightTable; }
        }

        public JoinType Type
        {
            get { return this.type; }
        }
    }
}
