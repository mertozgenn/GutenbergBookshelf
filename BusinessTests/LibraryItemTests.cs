using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GutenbergTests
{
    [TestClass]
    public class LibraryItemTests: TestBase
    {
        [TestMethod]
        public void AddToLibrary_ShouldAddBookToLibrary()
        {
            var libraryItemService = _container.Resolve<ILibraryItemService>();
            var data = libraryItemService.AddToLibrary(0, 0);
            Assert.IsNotNull(data);
            Assert.AreEqual(true, data.Success);
            Assert.AreEqual(Messages.BookAdded, data.Message);
            libraryItemService.RemoveFromLibrary(0, 0);
        }

        [TestMethod]
        public void AddToLibrary_ShouldReturnErrorIfBookAlreadyExists()
        {
            var libraryItemService = _container.Resolve<ILibraryItemService>();
            libraryItemService.AddToLibrary(0, 0);
            var data = libraryItemService.AddToLibrary(0, 0);
            Assert.IsNotNull(data);
            Assert.AreEqual(false, data.Success);
            Assert.AreEqual(Messages.BookAlreadyExists, data.Message);
        }

        [TestMethod]
        public void GetLibraryItems_ShouldReturnListOfLibraryItems()
        {
            var libraryItemService = _container.Resolve<ILibraryItemService>();
            libraryItemService.AddToLibrary(1, 0);
            var data = libraryItemService.GetLibraryItems(0);
            Assert.IsNotNull(data);
            Assert.AreEqual(true, data.Success);
            Assert.IsNotNull(data.Data);
            Assert.AreEqual(true, data.Data.Count == 1);
            libraryItemService.RemoveFromLibrary(1, 0);
        }

        [TestMethod]
        public void RemoveFromLibrary_ShouldRemoveBookFromLibrary()
        {
            var libraryItemService = _container.Resolve<ILibraryItemService>();
            libraryItemService.AddToLibrary(0, 0);
            var data = libraryItemService.RemoveFromLibrary(0, 0);
            Assert.IsNotNull(data);
            Assert.AreEqual(true, data.Success);
            Assert.AreEqual(Messages.BookRemoved, data.Message);
        }

        [TestMethod]
        public void RemoveFromLibrary_ShouldReturnErrorIfBookNotFound()
        {
            var libraryItemService = _container.Resolve<ILibraryItemService>();
            var data = libraryItemService.RemoveFromLibrary(0, 0);
            Assert.IsNotNull(data);
            Assert.AreEqual(false, data.Success);
            Assert.AreEqual(Messages.BookNotFound, data.Message);
        }

        [TestCleanup]
        public void CleanUp()
        {
            var libraryItemDal = _container.Resolve<ILibraryItemDal>();
            var data = libraryItemDal.GetAll(l => l.UserId == 0);
            libraryItemDal.DeleteAll(data);
        }
    }
}
