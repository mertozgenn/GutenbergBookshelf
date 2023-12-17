using Core.Concrete.DataAccess.EntityFramework.Repositories;
using Core.Utilities.Datatable;
using Core.Utilities.Datatable.Entities;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfBookDal : EfEntityRepositoryBase<Book, Context>, IBookDal
    {
        public DatatableResult<Book> GetAllDt(DatatableParameterDto? datatableParameterDto = null)
        {
            using (Context context = new Context())
            {
                if (datatableParameterDto == null)
                {
                    var list = context.Books.ToList();
                    return new DatatableResult<Book>
                    {
                        Draw = 0,
                        RecordsTotal = list.Count,
                        RecordsFiltered = list.Count,
                        Data = list,
                    };
                }
                var data = context.Books.AsQueryable();
                int recordsTotal = data.Count();
                // Search
                if (!string.IsNullOrEmpty(datatableParameterDto.Search.Value))
                {
                    string key = datatableParameterDto.Search.Value;
                    data = data.Where(x => x.Title.ToLower().Contains(key.ToLower()) ||
                                        x.Author.ToLower().Contains(key.ToLower()) ||
                                        x.Lang.ToLower().Contains(key.ToLower()) ||
                                        x.EbookNo.ToString().ToLower().Contains(key.ToLower()));
                }
                // Ordering
                data = DataTableHelper.Order(datatableParameterDto, data.AsQueryable());

                DatatableResult<Book> result = new DatatableResult<Book>
                {
                    Draw = datatableParameterDto.Draw,
                    RecordsTotal = recordsTotal,
                    RecordsFiltered = data.Count(),
                    Data = data.Skip(datatableParameterDto.Start).Take(datatableParameterDto.Length).ToList(),
                };
                return result;
            }
        }
    }
}
