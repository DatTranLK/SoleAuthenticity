using Entity.Dtos.Account;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IAccountService
    {
        Task<ServiceResponse<IEnumerable<AccountDto>>> GetAllAccountWithPagination(int page, int pageSize);
        Task<ServiceResponse<int>> CountAccount();
        Task<ServiceResponse<AccountDto>> GetAccountById(int id);
        Task<ServiceResponse<string>> DisableOrEnableAccount(int id);
/*        Task<ServiceResponse<string>> CreateNewAccount(Account account);
        Task<ServiceResponse<string>> UpdateAccount(Account account);*/
    }
}
