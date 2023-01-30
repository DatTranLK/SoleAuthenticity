using Entity.Dtos.ShoeCheck;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IShoeCheckService
    {
        //GetForAdmin
        Task<ServiceResponse<IEnumerable<ShoeCheckDtoForAdmin>>> GetShoeChecksForAdminWithPagination(int page, int pageSize);
        Task<ServiceResponse<int>> CountForAdmin();
        Task<ServiceResponse<ShoeCheckDtoForAdmin>> GetShoeCheckByIdForAdmin(int id);
        //GetForCustomer
        Task<ServiceResponse<IEnumerable<ShoeCheckDtoForCustomer>>> GetShoeChecksForCustomerWithPagination(int cusId, int page, int pageSize);
        Task<ServiceResponse<int>> CountForCus(int cusId);
        Task<ServiceResponse<ShoeCheckDtoForCustomer>> GetShoeCheckByIdForCustomer(int id);
        //GetForStaff
        Task<ServiceResponse<IEnumerable<ShoeCheckDtoForStaff>>> GetShoeChecksForStaffWithPagination(int staffId, int page, int pageSize);
        Task<ServiceResponse<int>> CountForStaff(int staffId);
        Task<ServiceResponse<ShoeCheckDtoForStaff>> GetShoeCheckByIdForStaff(int id);

        Task<ServiceResponse<string>> DisableOrEnableShoeCheck(int id);
        Task<ServiceResponse<int>> CreateNewShoeCheck(ShoeCheck shoeCheck);
    }
}
