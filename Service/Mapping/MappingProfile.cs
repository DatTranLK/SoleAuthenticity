using AutoMapper;
using Entity.Dtos.Account;
using Entity.Dtos.Brand;
using Entity.Dtos.Category;
using Entity.Dtos.Comment;
using Entity.Dtos.New;
using Entity.Dtos.Order;
using Entity.Dtos.OrderDetail;
using Entity.Dtos.Product;
using Entity.Dtos.RequestSellSecondHand;
using Entity.Dtos.Review;
using Entity.Dtos.ShoeCheck;
using Entity.Dtos.Size;
using Entity.Dtos.Store;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Store, StoreDto>().ReverseMap();
            CreateMap<New, NewDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Size, SizeDto>().ReverseMap();
            CreateMap<ShoeCheck, ShoeCheckDtoForAdmin>().ReverseMap();
            CreateMap<ShoeCheck, ShoeCheckDtoForCustomer>().ReverseMap();
            CreateMap<ShoeCheck, ShoeCheckDtoForStaff>().ReverseMap();
            CreateMap<Review, ReviewDto>().ForMember(dto => dto.AuthorName, act => act.MapFrom(obj => obj.Staff.Name)).ReverseMap();
            CreateMap<Order, OrderDtoForCus>().ReverseMap();
            CreateMap<Order, OrderDtoForStaff>()
                .ForMember(dto => dto.CustomerName, act => act.MapFrom(obj => obj.Customer.Name))
                .ReverseMap();
            CreateMap<Order, OrderDtoForAdmin>()
                .ForMember(dto => dto.CustomerName, act => act.MapFrom(obj => obj.Customer.Name))
                .ForMember(dto => dto.StaffName, act => act.MapFrom(obj => obj.Staff.Name))
                .ReverseMap();
            CreateMap<OrderDetail, OrderDetailDto>()
                .ForMember(dto => dto.Code, act => act.MapFrom(obj => obj.Product.Code))
                .ForMember(dto => dto.ProductName, act => act.MapFrom(obj => obj.Product.Name))
                .ForMember(dto => dto.Price, act => act.MapFrom(obj => obj.Product.Price))
                .ForMember(dto => dto.TotalPrice, act => act.MapFrom(obj => obj.Order.TotalPrice))
                .ReverseMap();
            CreateMap<Comment, CommentDto>().ForMember(dto => dto.UserName, act => act.MapFrom(obj => obj.User.Name)).ReverseMap();
            CreateMap<RequestSellSecondHand, RequestSellSecondHandDto>().ReverseMap();
            CreateMap<RequestSellSecondHand, RequestSellSecondHandDtoForAd>()
                .ForMember(dto => dto.UserName, act => act.MapFrom(obj => obj.User.Name))
                .ReverseMap();
            CreateMap<Product, ProductShowDto>().ForMember(dto => dto.ImgPath, act => act.MapFrom(obj => obj.ProductImages.FirstOrDefault().ImgPath));
        }
    }
}
