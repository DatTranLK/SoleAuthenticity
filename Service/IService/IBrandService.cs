using Entity.Dtos.Brand;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IBrandService
    {
        Task<ServiceResponse<IEnumerable<BrandDto>>> GetAllBrandsWithPagination(int page, int pageSize);
        Task<ServiceResponse<int>> CountBrand();
        Task<ServiceResponse<BrandDto>> GetBrandById(int id);
        Task<ServiceResponse<string>> DisableOrEnableBrand(int id);
        Task<ServiceResponse<int>> CreateNewBrand(Brand brand);
        Task<ServiceResponse<Brand>> UpdateBrand(int id, Brand brand);
    }
}
