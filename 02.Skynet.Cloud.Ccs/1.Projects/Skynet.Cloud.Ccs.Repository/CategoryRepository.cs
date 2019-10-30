using System;
using System.Collections.Generic;
using System.Linq;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Ccs.Entity;

namespace UWay.Skynet.Cloud.Ccs.Repository
{
    public class CategoryRepository : ObjectRepository
    {
        public CategoryRepository(IDbContext dbcontext) : base(dbcontext)
        {
        }

        public int Add(CategoryItem info)
        {
            return base.Add<CategoryItem>(info);
        }


        public long Update(CategoryItem info)
        {
            return base.Update<CategoryItem>(info);
        }

        public CategoryItem GetById(long categoryId)
        {
            return GetByID<CategoryItem>(categoryId);
        }

        public long Delete(long[] ids)
        {
            return base.Delete<CategoryItem>(p => ids.Contains(p.CategoryItemId));
        }

        public IQueryable<CategoryItem> Query()
        {
            return CreateQuery<CategoryItem>();
        }
    }
}
