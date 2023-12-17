using Entities.Concrete;

namespace UI.Models
{
    public class SearchResultModel
    {
        public List<Book> Books { get; set; }
        public string? Message { get; set; }
        public string? SearchKey { get; set; }
    }
}
