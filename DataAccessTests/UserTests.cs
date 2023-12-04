using Core.Concrete.Entities;
using DataAccess.Concrete;

namespace DataAccessTests
{
    [TestClass]
    public class UserTests
    {
        private EfUserDal _userDal = new EfUserDal();
        private User user = new User
        {
            Email = "test@₺est.com",
            LastLoginDate = DateTime.Now,
            Name = "Test",
            PasswordHash = new byte[0],
            PasswordSalt = new byte[0],
            Surname = "Test",
        };

        [TestMethod]
        public void GetAll_ShouldReturnListOfUsers()
        {
            var result = _userDal.GetAll();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Add_ShouldAddUser()
        {
            _userDal.Add(user);
        }

        [TestMethod]
        public void Update_ShouldUpdateUser()
        {
            _userDal.Add(user);
            var data = _userDal.GetAll(x => x.Email == "test@₺est.com").FirstOrDefault();
            data.Name = "Test2";
            _userDal.Update(user);
        }

        [TestMethod]
        public void Delete_ShouldDeleteUser()
        {
            _userDal.Add(user);
            var data = _userDal.GetAll(x => x.Email == "test@₺est.com").FirstOrDefault();
            _userDal.Delete(data);
        }

        [TestCleanup]
        public void Clean()
        {
            var data = _userDal.GetAll(x => x.Email == "test@₺est.com");
            _userDal.DeleteAll(data);
        }
    }
}
