﻿using Core.Abstract.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ILibraryItemDal: IEntityRepository<LibraryItem>
    {
        List<Book> GetLibraryItems(int userId);
    }
}