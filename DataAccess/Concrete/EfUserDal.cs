using Core.Concrete.DataAccess.EntityFramework.Repositories;
using Core.Concrete.Entities;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfUserDal: EfEntityRepositoryBase<User, Context>, IUserDal
    {
    }
}
