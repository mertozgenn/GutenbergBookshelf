using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ILibraryItemService
    {
        IResult AddToLibrary(int bookId, int userId);
        IResult RemoveFromLibrary(int bookId, int userId);
        IDataResult<List<LibraryDto>> GetLibraryItems(int userId);
        IResult UpdateProgress(int bookId, int userId, int progress);
        IDataResult<byte> GetProgress(int bookId, int userId);
    }
}
