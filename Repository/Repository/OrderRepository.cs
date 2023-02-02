using Entity.Models;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly SoleAuthenticity_DBContext _dbContext;

        public OrderRepository(SoleAuthenticity_DBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
