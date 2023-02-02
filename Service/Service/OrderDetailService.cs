using AutoMapper;
using Entity.Dtos.OrderDetail;
using Entity.Models;
using Repository.IRepository;
using Service.IService;
using Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }
        public async Task<ServiceResponse<int>> CreateNewOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                if (orderDetail.Quantity <= 0)
                {
                    return new ServiceResponse<int>
                    { 
                        Message = "Quantity must greater than 0. Please try again!!!",
                        Success = true,
                        StatusCode = 400
                    };
                }
                var checkOrderExist = await _orderRepository.GetById(orderDetail.OrderId);
                if (checkOrderExist == null)
                {
                    return new ServiceResponse<int>
                    {
                        Message = "Not found order",
                        Success = true,
                        StatusCode = 400
                    };
                }

                var checkProductExist = await _productRepository.GetById(orderDetail.ProductId);
                if (checkProductExist == null)
                {
                    return new ServiceResponse<int>
                    {
                        Message = "Not found product",
                        Success = true,
                        StatusCode = 400
                    };
                }
                await _orderDetailRepository.Insert(orderDetail);
                return new ServiceResponse<int>
                {
                    Data = orderDetail.Id,
                    Message = "Successfully",
                    StatusCode = 201,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<IEnumerable<OrderDetailDto>>> GetOrderDetailsByOrderId(int orderId)
        {
            try
            {
                List<Expression<Func<OrderDetail, object>>> includes = new List<Expression<Func<OrderDetail, object>>>
                { 
                    x => x.Order,
                    x => x.Product
                };
                var lst = await _orderDetailRepository.GetAllWithCondition(x => x.OrderId == orderId, includes, x => x.Id, true);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<OrderDetailDto>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<OrderDetailDto>>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<OrderDetailDto>>
                {
                    Data = lstDto,
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
