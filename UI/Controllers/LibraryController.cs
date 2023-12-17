using Business.Abstract;
using Core.Utilities.Datatable.Entities;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UI.Models;

namespace UI.Controllers
{
    public class LibraryController : Controller
    {
        private ILibraryItemService _libraryItemService;
        private IBookService _bookService;

        public LibraryController(ILibraryItemService libraryItemService, IBookService bookService)
        {
            _libraryItemService=libraryItemService;
            _bookService=bookService;
        }

        [Route("")]
        public IActionResult Library()
        {
            return GetLibraryView();
        }

        [Route("search")]
        public IActionResult Search(string key)
        {
            var result = _bookService.GetBySearchKey(key);
            SearchResultModel searchResultModel = new SearchResultModel()
            {
                Books = result.Data != null ? result.Data : new List<Book>(),
                Message = result.Message,
                SearchKey = key
            };
            return View(searchResultModel);
        }

        [Route("browse")]
        public IActionResult Browse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetAll(DatatableParameterDto datatableParameter)
        {
            var result = _bookService.GetAll(datatableParameter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        public IActionResult AddToLibrary(int bookId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);
            var result = _libraryItemService.AddToLibrary(bookId, userId);
            return GetLibraryView(result.Message);
        }

        public IActionResult RemoveFromLibrary(int bookId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);
            var result = _libraryItemService.RemoveFromLibrary(bookId, userId);
            return GetLibraryView(result.Message);
        }

        private ViewResult GetLibraryView(string? message = null)
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);
            var result = _libraryItemService.GetLibraryItems(userId);
            LibraryModel libraryModel = new LibraryModel()
            {
                Books = result.Data != null ? result.Data : new List<LibraryDto>(),
                Message = $"{result.Message} {message}"
            };
            return View(libraryModel);
        }
    }
}
