using Entities.Concrete;

namespace UI.Models
{
    public class ReadBookModel
    {
        public Book Book { get; set; }
        public string BookContent { get; set; }
        public byte Progress { get; set; }
    }
}
