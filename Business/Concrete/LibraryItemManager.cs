using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LibraryItemManager : ILibraryItemService
    {
        private ILibraryItemDal _libraryItemDal;

        public LibraryItemManager(ILibraryItemDal libraryItemDal)
        {
            _libraryItemDal=libraryItemDal;
        }

        public IResult AddToLibrary(int bookId, int userId)
        {
            var data = _libraryItemDal.Get(l => l.BookId == bookId && l.UserId == userId);
            if (data != null)
            {
                return new ErrorResult(Messages.BookAlreadyExists);
            }
            _libraryItemDal.Add(new LibraryItem 
            { 
                BookId = bookId, 
                UserId = userId,
                AddedDate = DateTime.Now,
                Progress = 0
            });
            return new SuccessResult(Messages.BookAdded);
        }

        public IDataResult<List<LibraryDto>> GetLibraryItems(int userId)
        {
            var data = _libraryItemDal.GetLibraryItems(userId);
            return new SuccessDataResult<List<LibraryDto>>(data);
        }

        public IResult RemoveFromLibrary(int bookId, int userId)
        {
            var data = _libraryItemDal.Get(l => l.BookId == bookId && l.UserId == userId);
            if (data == null)
            {
                return new ErrorResult(Messages.BookNotFound);
            }
            _libraryItemDal.Delete(data);
            return new SuccessResult(Messages.BookRemoved);
        }
    }
}
