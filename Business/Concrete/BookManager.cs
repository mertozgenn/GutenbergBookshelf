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

        public IDataResult<DatatableResult<Book>> GetAll(DatatableParameterDto? datatableParameterDto = null)
        {
            var data = _bookDal.GetAllDt(datatableParameterDto);
            return new SuccessDataResult<DatatableResult<Book>>(data);
        }

        public IDataResult<DatatableResult<Book>> GetBySearchKey(string searchKey, DatatableParameterDto datatableParameterDto)
        {
            searchKey = searchKey.ToLower();
            var data = _bookDal.GetAll(b => b.Title.ToLower().Contains(searchKey) || 
                                            b.Author.ToLower().Contains(searchKey) || 
                                            b.Subject.ToLower().Contains(searchKey));

            if (datatableParameterDto == null)
            {
                return new SuccessDataResult<DatatableResult<Book>>(new DatatableResult<Book>
                {
                    Data = data,
                    Draw = 0,
                    Error = null,
                    RecordsFiltered = data.Count(),
                    RecordsTotal = data.Count()
                });
            }

            int recordsTotal = data.Count();
            // Search
            if (!string.IsNullOrEmpty(datatableParameterDto.Search.Value))
            {
                string key = datatableParameterDto.Search.Value;
                data = data.Where(x => (x.Title ?? "").ToLower().Contains(key.ToLower()) ||
                                    (x.Author ?? "").ToLower().Contains(key.ToLower()) ||
                                    (x.Lang ?? "").ToLower().Contains(key.ToLower()) ||
                                    x.EbookNo.ToString().ToLower().Contains(key.ToLower())).ToList();
            }
            // Ordering
            data = DataTableHelper.Order(datatableParameterDto, data.AsQueryable()).ToList();

            DatatableResult<Book> result = new DatatableResult<Book>
            {
                Draw = datatableParameterDto.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = data.Count(),
                Data = data.Skip(datatableParameterDto.Start).Take(datatableParameterDto.Length).ToList(),
            };
            return new SuccessDataResult<DatatableResult<Book>>(result);
        }
    }
}
