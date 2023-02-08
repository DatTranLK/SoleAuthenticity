using Entity.Dtos.Comment;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ICommentService
    {
        //For Cus
        Task<ServiceResponse<IEnumerable<CommentDto>>> GetCommentsByReviewIdWithPagination(int reviewId, int page, int pageSize);
        Task<ServiceResponse<int>> CountCommentByReviewId(int reviewId);
        Task<ServiceResponse<IEnumerable<CommentDto>>> GetSubCommentByParentCommentIdWithPagination(int commentParentId, int page, int pageSize);
        Task<ServiceResponse<int>> CountSubCommentByParentCommentId(int commentParentId);

        Task<ServiceResponse<int>> CreateNewComment(Comment comment);
        Task<ServiceResponse<string>> DisableOrEnableComment(int commentId);
        Task<ServiceResponse<string>> UpdateComment(int commentId, Comment comment);
        Task<ServiceResponse<string>> ReplyComment(int commentParentId, Comment comment);

    }
}
