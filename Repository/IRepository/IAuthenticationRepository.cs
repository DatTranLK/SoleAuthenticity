using Entity;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IAuthenticationRepository : IGenericRepository<Account>
    {
        Task<Account> Authentication(IdToken idToken);
    }
}
