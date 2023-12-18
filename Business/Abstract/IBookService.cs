using Core.Utilities.Datatable.Entities;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBookService
    {
        IDataResult<DatatableResult<Book>> GetAll(DatatableParameterDto? datatableParameterDto = null);
        IDataResult<DatatableResult<Book>> GetBySearchKey(string key, DatatableParameterDto? datatableParameterDto = null);
        IDataResult<Book> GetById(int id);
        IDataResult<string> GetBookContent(int id);
    }
}
