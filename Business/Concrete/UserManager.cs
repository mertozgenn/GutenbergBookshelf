using Business.Abstract;
using Core.Concrete.Entities;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal=userDal;
        }

        public IResult Add(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordSalt, out passwordHash);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                Name = userForRegisterDto.Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                LastLoginDate = DateTime.Now,
                Surname = userForRegisterDto.Surname,
            };
            _userDal.Add(user);
            return new SuccessResult();
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        public void UpdateLastLoginDate(User user)
        {
            user.LastLoginDate = DateTime.Now;
            _userDal.Update(user);
        }
    }
}
