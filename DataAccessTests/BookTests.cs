using DataAccess.Concrete;

namespace DataAccessTests
{
    [TestClass]
    public class BookTests
    {
        private EfBookDal _bookDal = new EfBookDal();
        [TestMethod]
        public void GetAll_ShouldReturnListOfBooks()
        {
            var result = _bookDal.GetAll();
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Count);
        }
    }
}