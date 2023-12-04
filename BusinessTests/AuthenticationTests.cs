using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GutenbergTests
{
    [TestClass]
    public class AuthenticationTests: TestBase
    {
        UserForRegisterDto userForRegister = new()
        {
            Email = "test@test.com",
            Name = "Test",
            Password = "123456",
            Surname = "Test"
        };

        [TestMethod]
        public void Register_WithValidCredentials_PerformsRegistration()
        {
            var authService = _container.Resolve<IAuthService>();
            var result = authService.Register(userForRegister);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Data.Count == 3);
        }

        [TestMethod]
        public void Register_WithExistingEmail_ShouldReturnUserAlreadyRegisteredMessage()
        {
            var authService = _container.Resolve<IAuthService>();
            authService.Register(userForRegister);
            var result = authService.Register(userForRegister);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.UserAlreadyExists, result.Message);
        }

        [TestMethod]
        public void Login_WithValidCredentials_PerformsLogin()
        {
            var authService = _container.Resolve<IAuthService>();
            authService.Register(userForRegister);

            var result = authService.Login(new UserForLoginDto
            {
                Email = userForRegister.Email,
                Password = userForRegister.Password
            });
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Data.Count == 3);
        }

        [TestMethod]
        public void Login_WithWrongPassword_ShouldReturnWrongPasswordMessage()
        {
            var authService = _container.Resolve<IAuthService>();
            authService.Register(userForRegister);

            var result = authService.Login(new UserForLoginDto
            {
                Email = userForRegister.Email,
                Password = "1234567"
            });
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.PasswordError, result.Message);
        }

        [TestMethod]
        public void Login_WithInvalidEmail_ShouldReturnUserNotFoundMessage()
        {
            var authService = _container.Resolve<IAuthService>();
            var result = authService.Login(new UserForLoginDto
            {
                Email = "",
                Password = userForRegister.Password
            });
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.UserNotFound, result.Message);
        }

        [TestCleanup]
        public void CleanUp()
        {
            var userDal = _container.Resolve<IUserDal>();
            var data = userDal.GetAll(u => u.Email == userForRegister.Email);
            userDal.DeleteAll(data);
        }
    }
}
