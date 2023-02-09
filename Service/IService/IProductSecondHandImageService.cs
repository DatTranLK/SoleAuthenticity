using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IProductSecondHandImageService
    {
        Task<ServiceResponse<IEnumerable<ProductSecondHandImage>>> GetProductSecondHandImagesByRequestId(int requestId);
        Task<ServiceResponse<int>> CountAll(int requestId);
        Task<ServiceResponse<int>> CreateNewProductSecondHandImage(ProductSecondHandImage productSecondHandImage);
        Task<ServiceResponse<ProductSecondHandImage>> UpdateProductSecondHandImage(int productSecondHandImageId, ProductSecondHandImage productSecondHandImage);
    }
}
