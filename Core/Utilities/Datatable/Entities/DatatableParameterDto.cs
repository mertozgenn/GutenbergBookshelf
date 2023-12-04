using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Datatable.Entities
{
    public class DatatableParameterDto
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public DatatableSearchItem Search { get; set; }
        public List<DatatableOrderItem> Order { get; set; }
        public List<DatatableColumnItem> Columns { get; set; }
    }
}
