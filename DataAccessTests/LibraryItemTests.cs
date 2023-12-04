using DataAccess.Concrete;
using Entities.Concrete;

namespace DataAccessTests
{
    [TestClass]
    public class LibraryItemTests
    {
        private EfLibraryItemDal _libraryItemDal = new EfLibraryItemDal();
        private LibraryItem libraryItem = new LibraryItem
        {
            AddedDate = DateTime.Now,
            BookId = 1,
            UserId = 0,
            Progress = 0,
        };

        [TestMethod]
        public void GetAll_ShouldReturnListOfLibraryItems()
        {
            var result = _libraryItemDal.GetAll();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Add_ShouldAddLibraryItem()
        {
            _libraryItemDal.Add(libraryItem);
        }

        [TestMethod]
        public void Update_ShouldUpdateLibraryItem()
        {
            _libraryItemDal.Add(libraryItem);
            var data = _libraryItemDal.GetAll(x => x.UserId == 0).FirstOrDefault();
            data.Progress = 1;
            _libraryItemDal.Update(libraryItem);
        }

        [TestMethod]
        public void Delete_ShouldDeleteLibraryItem()
        {
            _libraryItemDal.Add(libraryItem);
            var data = _libraryItemDal.GetAll(x => x.UserId == 0).FirstOrDefault();
            _libraryItemDal.Delete(data);
        }

        [TestCleanup]
        public void Clean()
        {
            var data = _libraryItemDal.GetAll(x => x.UserId == 0);
            _libraryItemDal.DeleteAll(data);
        }
    }
}
