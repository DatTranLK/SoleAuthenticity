using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IShoeCheckImageService
    {
        Task<ServiceResponse<IEnumerable<ShoeCheckImage>>> GetShoeCheckImages(int shoeCheckId);
        Task<ServiceResponse<int>> CountAll(int shoeCheckId);
        Task<ServiceResponse<int>> CreateNewShoeCheckImage(ShoeCheckImage shoeCheckImage);
        Task<ServiceResponse<ShoeCheckImage>> UpdateShoeCheckImage(int shoeCheckImageId, ShoeCheckImage shoeCheckImage);
    }
}
