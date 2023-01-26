using AutoMapper;
using Entity.Dtos.Account;
using Entity.Dtos.Brand;
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
        }
    }
}
