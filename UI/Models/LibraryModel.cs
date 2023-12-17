using Entities.Concrete;
using Entities.Dtos;

namespace UI.Models
{
    public class LibraryModel
    {
        public List<LibraryDto> Books { get; set; }
        public string? Message { get; set; }
    }
}
