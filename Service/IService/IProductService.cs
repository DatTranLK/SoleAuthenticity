using Entity.Dtos.Product;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IProductService
    {
        Task<ServiceResponse<IEnumerable<ProductDto>>> GetProductsWithPagination(int page, int pageSize);
        Task<ServiceResponse<ProductDto>> GetProductById(int id);
        Task<ServiceResponse<int>> CountProducts();
        Task<ServiceResponse<string>> DisableOrEnableProduct(int id);
        Task<ServiceResponse<int>> CreateNewProduct(Product product);
        Task<ServiceResponse<Product>> UpdateProduct(int id, Product product);
    }
}
