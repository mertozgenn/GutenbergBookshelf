using Core.Abstract.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Book: IEntity
    {
        public int Id { get; set; }
        public int EbookNo { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? CoverUrl { get; set; }
        public string? ContentUrl { get; set; }
        public DateTime? PublishDate { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public string? Lang { get; set; }
    }
}
