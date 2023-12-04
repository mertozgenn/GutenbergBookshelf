using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GutenbergTests
{
    [TestClass]
    public class UserTests: TestBase
    {
        UserForRegisterDto userForRegisterDto = new()
        {
            Email = "test@test.com",
            Name = "Test",
            Password = "password",
            Surname = "Test"
        };

        [TestMethod]
        public void Add_ShouldAddUser()
        {
            var userService = _container.Resolve<IUserService>();
            var result = userService.Add(userForRegisterDto);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void GetByMail_ShouldGetUser()
        {
            var userService = _container.Resolve<IUserService>();
            userService.Add(userForRegisterDto);
            var result = userService.GetByMail(userForRegisterDto.Email);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Email, userForRegisterDto.Email);
        }

        [TestMethod]
        public void UpdateLastLoginDate_ShouldUpdateLastLoginDate()
        {
            var userService = _container.Resolve<IUserService>();
            userService.Add(userForRegisterDto);
            var user = userService.GetByMail(userForRegisterDto.Email);
            var oldDate = user.LastLoginDate;
            userService.UpdateLastLoginDate(user);
            var result = userService.GetByMail(userForRegisterDto.Email);
            var newDate = result.LastLoginDate;
            Assert.IsNotNull(result);
            Assert.AreNotEqual(oldDate, newDate);
        }

        [TestCleanup]
        public void CleanUp()
        {
            var userDal = _container.Resolve<IUserDal>();
            var data = userDal.GetAll(x => x.Email == userForRegisterDto.Email);
            userDal.DeleteAll(data);
        }
    }
}
