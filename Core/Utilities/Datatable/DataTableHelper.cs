using Core.Utilities.Datatable.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Datatable
{
    public static class DataTableHelper
    {
        public static DatatableResult<T> DataTableProcessing<T>(IQueryable<T> query, int recordsTotal, DatatableParameterDto? datatableParameter = null, 
            Func<IQueryable<T>, string, IQueryable<T>>? searchFilterFunction = null)
        {
            if (datatableParameter != null)
            {
                // Search
                if (!string.IsNullOrEmpty(datatableParameter.Search.Value) && searchFilterFunction != null)
                {
                    string key = datatableParameter.Search.Value;
                    query = searchFilterFunction(query, key);
                    recordsTotal = query.Count();
                }

                // Ordering
                query = Order(datatableParameter, query);

                DatatableResult<T> result = new DatatableResult<T>
                {
                    Draw = datatableParameter.Draw,
                    RecordsTotal = recordsTotal,
                    RecordsFiltered = query.Count(),
                    Data = query.Skip(datatableParameter.Start)
                                .Take(datatableParameter.Length == -1 ? recordsTotal : datatableParameter.Length)
                                .ToList(),
                };
                return result;
            }

            return new DatatableResult<T>
            {
                Draw = 0,
                RecordsTotal = recordsTotal,
                RecordsFiltered = query.Count(),
                Data = query.ToList(),
            };
        }

        public static IQueryable<T> Order<T>(DatatableParameterDto datatableParameterDto,
            IQueryable<T> query)
        {
            if (datatableParameterDto.Order.Any())
            {
                var index = datatableParameterDto.Order[0].Column;
                var key = datatableParameterDto.Columns[index].Name;
                var dir = datatableParameterDto.Order[0].Dir;
                var type = typeof(T);
                var propertyType = type.GetProperty(key).PropertyType;
                if (propertyType == typeof(string))
                {
                    return query.OrderBy($"({key} ?? string.Empty).Trim() {dir}");
                }
                return query.OrderBy($"{key} {dir}");
            }
            return query;
        }
    }
}
