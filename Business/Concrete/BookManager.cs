using Business.Abstract;
using Core.Utilities.Datatable;
using Core.Utilities.Datatable.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BookManager : IBookService
    {
        private IBookDal _bookDal;

        public BookManager(IBookDal bookDal)
        {
            _bookDal=bookDal;
        }

        public IDataResult<DatatableResult<Book>> GetAll(DatatableParameterDto datatableParameterDto)
        {
            var data = _bookDal.GetAllDt(datatableParameterDto);
            return new SuccessDataResult<DatatableResult<Book>>(data);
        }

        public IDataResult<List<Book>> GetBySearchKey(string key)
        {
            key = key.ToLower();
            var data = _bookDal.GetAll(b => b.Title.ToLower().Contains(key) || 
                                            b.Author.ToLower().Contains(key) || 
                                            b.Subject.ToLower().Contains(key));
            return new SuccessDataResult<List<Book>>(data);
        }
    }
}
