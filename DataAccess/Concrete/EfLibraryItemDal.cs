using Core.Concrete.DataAccess.EntityFramework.Repositories;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfLibraryItemDal : EfEntityRepositoryBase<LibraryItem, Context>, ILibraryItemDal
    {
        public List<Book> GetLibraryItems(int userId)
        {
            using (var context = new Context())
            {
                var result = from li in context.LibraryItems
                             join b in context.Books
                             on li.BookId equals b.Id
                             where li.UserId == userId
                             select b;
                return result.ToList();
            }
        }
    }
}
