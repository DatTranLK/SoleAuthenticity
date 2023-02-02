using Entity.Dtos.Order;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IOrderService
    {
        //For Cus
        Task<ServiceResponse<OrderDtoForCus>> GetOrderByIdForCus(int id);
        Task<ServiceResponse<IEnumerable<OrderDtoForCus>>> GetOrdersForCusWithPagination(int cusId, int page, int pageSize);
        Task<ServiceResponse<int>> CountOrdersForCus(int cusId);
        //For Staff
        Task<ServiceResponse<OrderDtoForStaff>> GetOrderByIdForStaff(int id);
        Task<ServiceResponse<IEnumerable<OrderDtoForStaff>>> GetOrdersForStaffWithPagination(int page, int pageSize);
        Task<ServiceResponse<int>> CountOrdersForStaff();

        //For Admin
        Task<ServiceResponse<OrderDtoForAdmin>> GetOrderByIdForAdmin(int id);
        Task<ServiceResponse<IEnumerable<OrderDtoForAdmin>>> GetOrdersForAdminWithPagination(int page, int pageSize);
        Task<ServiceResponse<int>> CountOrdersForAdmin();

        Task<ServiceResponse<int>> CreateNewOrder(Order order);

        //Change Status
        Task<ServiceResponse<string>> ChangeStatusOfTheOrderToAccepted(int orderId, int staffId);
        Task<ServiceResponse<string>> ChangeStatusOfTheOrderToDone(int orderId);
        Task<ServiceResponse<string>> ChangeStatusOfTheOrderToCancle(int orderId, int staffId);
    }
}
