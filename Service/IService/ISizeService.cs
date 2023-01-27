using Entity.Dtos.Size;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ISizeService
    {
        Task<ServiceResponse<IEnumerable<SizeDto>>> GetSizesWithPagination(int page, int pageSize);
        Task<ServiceResponse<SizeDto>> GetSizeById(int id);
        Task<ServiceResponse<int>> CountSizes();
        Task<ServiceResponse<string>> DisableOrEnableSize(int id);
        Task<ServiceResponse<int>> CreateNewSize(Size size);
        Task<ServiceResponse<Size>> UpdateSize(int id, Size size);
    }
}
