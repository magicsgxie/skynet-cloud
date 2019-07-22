using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Nom.Entity;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Nom.Repository
{
    public class NeGroupItemRepository : ObjectRepository
    {

        public NeGroupItemRepository(IDbContext uow) : base(uow)
        {

        }


        /// <summary>
        /// 获取网元分组最大ID
        /// </summary>
        /// <returns></returns>
        private int GetNeGroupId()
        {
            var q = CreateQuery<NeGroupItem>();
            var a = from b in q
                    select b.GroupID;
            return a.Max();
        }

        public int AddNeGroupItem(NeGroupItem item)
        {
            item.GroupID = GetNeGroupId() + 1;
            return Add<NeGroupItem>(item);
        }

        public int UpdateNeGroupItem(NeGroupItem item)
        {
            return Update<NeGroupItem>(item);
        }

        public int DelNeGroupItems(int[] idArrays)
        {
            return Delete<NeGroupItem>(p => idArrays.Contains(p.GroupID));
        }


      

        public void UpdateNeGroupItem(NeGroupItem item, string tableName)
        {
            DelNeGroupItems(new int[] { item.GroupID });
            AddNeGroupItem(item);
            //
        }

        public IQueryable<NeGroup> GetNeGroups()
        {
            return CreateQuery<NeGroup>();
        }

        public IQueryable<NeGroupItem> GetNeGroupItems()
        {
            return CreateQuery<NeGroupItem>();
        }
    }
}
