﻿using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IOrderDetailRepository : IGenericRepository<OrderDetail>
    {
        Task<OrderDetail> GetOrderDetailByOrderId(int orderId);
    }
}
