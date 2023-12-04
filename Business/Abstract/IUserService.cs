using Core.Concrete.Entities;
using Core.Utilities.Results;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        User GetByMail(string email);
        void UpdateLastLoginDate(User user);
        IResult Add(UserForRegisterDto userForRegisterDto);
    }
}
