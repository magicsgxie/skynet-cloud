﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Request;
using UWay.Skynet.Cloud.Cfgs.Entity;

namespace UWay.Skynet.Cloud.Cfgs.Service.Interface
{
    public interface ICategoryService
    {
        IList<CategoryItem> GetByCategoryId(int categoryId);


        CategoryItem GetById(long id);


        long Add(CategoryItem user);


        int Update(CategoryItem user);

        int DeleteByIds(long[] arrayIds);
    }
}
