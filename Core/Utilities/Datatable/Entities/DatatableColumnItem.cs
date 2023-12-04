using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Datatable.Entities
{
    public class DatatableColumnItem
    {
        public string? Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public DatatableSearchItem Search { get; set; }
    }
}
