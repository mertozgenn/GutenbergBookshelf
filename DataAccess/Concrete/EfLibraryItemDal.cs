using Core.Concrete.DataAccess.EntityFramework.Repositories;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfLibraryItemDal : EfEntityRepositoryBase<LibraryItem, Context>, ILibraryItemDal
    {
        public List<LibraryDto> GetLibraryItems(int userId)
        {
            using (var context = new Context())
            {
                var result = from li in context.LibraryItems
                             join b in context.Books
                             on li.BookId equals b.Id
                             where li.UserId == userId
                             select new LibraryDto
                             {
                                 Author = b.Author,
                                 ContentUrl = b.ContentUrl,
                                 CoverUrl = b.CoverUrl,
                                 Description = b.Description,
                                 EbookNo = b.EbookNo,
                                 Id = b.Id,
                                 Lang = b.Lang,
                                 PublishDate = b.PublishDate,
                                 Subject = b.Subject,
                                 Title = b.Title,
                                 Progress = li.Progress
                             };
                return result.ToList();
            }
        }
    }
}
