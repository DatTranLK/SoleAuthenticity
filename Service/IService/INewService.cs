using Entity.Dtos.New;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface INewService
    {
        Task<ServiceResponse<IEnumerable<NewDto>>> GetNewsWithPagination(int page, int pageSize);
        Task<ServiceResponse<NewDto>> GetNewById(int id);
        Task<ServiceResponse<int>> CountNews();
        Task<ServiceResponse<string>> DisableOrEnableNew(int id);
        Task<ServiceResponse<int>> CreateNewNew(New newAdd);
        Task<ServiceResponse<New>> UpdateNew(int id, New newUpdate);
    }
}
