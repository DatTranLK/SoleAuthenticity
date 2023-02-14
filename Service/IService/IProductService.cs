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

        //second hand product
        Task<ServiceResponse<IEnumerable<ProductDto>>> GetSecondHandProductsWithPagination(int page, int pageSize);
        Task<ServiceResponse<int>> CountSecondHandProductsWithPagination();
        Task<ServiceResponse<IEnumerable<ProductDto>>> GetSecondHandProductsWithMobile();

        //preorder product
        Task<ServiceResponse<IEnumerable<ProductDto>>> GetPreOrderProductsWithPagination(int page, int pageSize);
        Task<ServiceResponse<int>> CountPreOrderProductsWithPagination();
        Task<ServiceResponse<IEnumerable<ProductDto>>> GetPreOrderProductsWithMobile();
        Task<ServiceResponse<int>> CreateNewPreOrderProduct(Product product);

        //For Customer UI
        Task<ServiceResponse<IEnumerable<ProductShowDto>>> GetProductsInCus();
    }
}
