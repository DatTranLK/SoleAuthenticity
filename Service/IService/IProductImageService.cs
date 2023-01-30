using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IProductImageService
    {
        Task<ServiceResponse<IEnumerable<ProductImage>>> GetProductImageById(int productId);
        Task<ServiceResponse<int>> CountAll(int productId);
        Task<ServiceResponse<int>> CreateNewProductImage(ProductImage productImage);
        Task<ServiceResponse<ProductImage>> UpdateProductImage(int id, ProductImage productImage);
    }
}
