using Core.Utilities.Results;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<List<Claim>> Register(UserForRegisterDto userForRegisterDto);
        IDataResult<List<Claim>> Login(UserForLoginDto userForLoginDto);
    }
}
