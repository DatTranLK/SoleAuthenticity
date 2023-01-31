using Entity.Dtos.Review;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IReviewService
    {
        Task<ServiceResponse<IEnumerable<ReviewDto>>> GetReviewsWithPagination(int page, int pageSize);
        Task<ServiceResponse<ReviewDto>> GetReviewById(int productId);
        Task<ServiceResponse<int>> CountAll();
        Task<ServiceResponse<string>> DisableOrEnableReview(int productId);
        Task<ServiceResponse<int>> CreateNewReview(Review review);
        Task<ServiceResponse<Review>> UpdateReview(int productId, Review review);
    }
}
