using Autofac;
using Business.Abstract;
using Business.DependencyResolvers.Autofac;

namespace GutenbergTests
{
    [TestClass]
    public class BookTests: TestBase
    {
        [TestMethod]
        public void GetAll_ShouldReturnListOfBooks()
        {
            var bookService = _container.Resolve<IBookService>();
            var data = bookService.GetAll();
            Assert.IsNotNull(data);
            Assert.AreEqual(true, data.Success);
            Assert.IsNotNull(data.Data);
            Assert.AreEqual(true, data.Data.Count > 70_000);
        }

        [TestMethod]
        public void GetBySearchKey_ShouldReturnListOfMatchingBooks()
        {
            var bookService = _container.Resolve<IBookService>();
            var data = bookService.GetBySearchKey("The");
            Assert.IsNotNull(data);
            Assert.AreEqual(true, data.Success);
            Assert.IsNotNull(data.Data);
            Assert.AreEqual(true, data.Data.Count >= 36708);
        }
    }
}