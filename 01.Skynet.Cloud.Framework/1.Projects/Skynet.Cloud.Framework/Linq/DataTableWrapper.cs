

namespace UWay.Skynet.Cloud.Linq
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    internal class DataTableWrapper : IEnumerable<DataRowView>
    {
        internal DataTableWrapper(DataTable dataTable)
        {
            Table = dataTable;
        }

        public DataTable Table { get; private set; }

        public IEnumerator<DataRowView> GetEnumerator()
        {
            if (Table == null)
                yield break;

            foreach (DataRowView row in Table.DefaultView)
            {
                yield return row;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
