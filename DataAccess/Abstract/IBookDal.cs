using Core.Abstract.DataAccess;
using Core.Utilities.Datatable.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IBookDal: IEntityRepository<Book>
    {
        DatatableResult<Book> GetAllDt(DatatableParameterDto datatableParameterDto);
    }
}
