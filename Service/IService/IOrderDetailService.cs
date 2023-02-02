using Entity.Dtos.OrderDetail;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IOrderDetailService
    {
        Task<ServiceResponse<IEnumerable<OrderDetailDto>>> GetOrderDetailsByOrderId(int orderId);
        Task<ServiceResponse<int>> CreateNewOrderDetail(OrderDetail orderDetail);
    }
}
