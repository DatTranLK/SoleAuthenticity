using Entity.Dtos.Store;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IStoreService
    {
        Task<ServiceResponse<IEnumerable<StoreDto>>> GetStoresWithPagination(int page, int pageSize);
        Task<ServiceResponse<StoreDto>> GetStoreById(int id);
        Task<ServiceResponse<int>> CountStores();
        Task<ServiceResponse<string>> DisableOrEnableStore(int id);
        Task<ServiceResponse<int>> CreateNewStore(Store store);
        Task<ServiceResponse<Store>> UpdateStore(int id, Store store);
    }
}
