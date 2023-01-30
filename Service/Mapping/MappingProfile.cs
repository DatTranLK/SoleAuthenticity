using AutoMapper;
using Entity.Dtos.Account;
using Entity.Dtos.Brand;
using Entity.Dtos.Category;
using Entity.Dtos.New;
using Entity.Dtos.Product;
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
        }
    }
}
