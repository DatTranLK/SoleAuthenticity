using AutoMapper;
using Entity.Dtos.Comment;
using Entity.Models;
using Repository.IRepository;
using Service.IService;
using Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task<ServiceResponse<int>> CountCommentByReviewId(int reviewId)
        {
            try
            {
                var count = await _commentRepository.CountAll(x => x.ReviewId == reviewId);
                if(count <= 0)
                {
                    return new ServiceResponse<int>
                    {
                        Data = 0,
                        Message = "Successfully",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<int>
                { 
                    Data = count,
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<int>> CountSubCommentByParentCommentId(int commentParentId)
        {
            try
            {
                var count = await _commentRepository.CountAll(x => x.ParentId == commentParentId);
                if (count <= 0)
                {
                    return new ServiceResponse<int>
                    { 
                        Data = 0,
                        Message = "Successfully",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<int>
                {
                    Data = count,
                    Message = "Successfully",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<int>> CreateNewComment(Comment comment)
        {
            try
            {
                //Validation in here
                //Starting insert into Db
                comment.ParentId = null;
                comment.CreatedAt = DateTime.Now;
                comment.UpdatedAt = null;
                comment.IsActive = true;
                await _commentRepository.Insert(comment);
                return new ServiceResponse<int>
                {
                    Data = comment.Id,
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 201
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<string>> DisableOrEnableComment(int commentId)
        {
            try
            {
                var checkExist = await _commentRepository.GetById(commentId);
                if(checkExist == null)
                {
                    return new ServiceResponse<string>
                    { 
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                if (checkExist.IsActive == true)
                {
                    var lstSubComment = await _commentRepository.GetByCondition(x => x.ParentId == commentId);
                    if (lstSubComment == null)
                    {
                        checkExist.IsActive = false;
                        await _commentRepository.Save();
                    }
                    /*if (lstSubComment.Count() <= 0)
                    {
                        return new ServiceResponse<string>
                        {
                            Message = "Not found sub comment",
                            Success = true,
                            StatusCode = 200
                        };
                    }*/
                    else {
                        foreach (var item in lstSubComment)
                        {
                            var checkExistComment = await _commentRepository.GetById(item.Id);
                            checkExistComment.IsActive = false;
                            await _commentRepository.Save();
                        }
                        checkExist.IsActive = false;
                        await _commentRepository.Save();
                    }
                    
                }
                else if (checkExist.IsActive == false)
                {
                    var lstSubComment = await _commentRepository.GetByCondition(x => x.ParentId == commentId);
                    if (lstSubComment == null)
                    {
                        checkExist.IsActive = true;
                        await _commentRepository.Save();
                    }
                    /*if (lstSubComment.Count() <= 0)
                    {
                        return new ServiceResponse<string>
                        {
                            Message = "Not found sub comment",
                            Success = true,
                            StatusCode = 200
                        };
                    }*/
                    else
                    {
                        foreach (var item in lstSubComment)
                        {
                            var checkExistComment = await _commentRepository.GetById(item.Id);
                            checkExistComment.IsActive = true;
                            await _commentRepository.Save();
                        }
                        checkExist.IsActive = true;
                        await _commentRepository.Save();
                    }
                    
                }
                return new ServiceResponse<string>
                {
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 204
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<IEnumerable<CommentDto>>> GetCommentsByReviewIdWithPagination(int reviewId, int page, int pageSize)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }
                List<Expression<Func<Comment, object>>> includes = new List<Expression<Func<Comment, object>>>
                {
                    x => x.User,
                    x => x.Review
                };
                var lst = await _commentRepository.GetAllWithPagination(x => x.ReviewId == reviewId, includes, x => x.Id, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<CommentDto>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<CommentDto>>
                    { 
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<CommentDto>>
                { 
                    Data = lstDto,
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<IEnumerable<CommentDto>>> GetSubCommentByParentCommentIdWithPagination(int commentParentId, int page, int pageSize)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }
                List<Expression<Func<Comment, object>>> includes = new List<Expression<Func<Comment, object>>>
                {
                    x => x.User,
                    x => x.Review
                };
                var lst = await _commentRepository.GetAllWithPagination(x => x.ParentId == commentParentId, includes, x => x.Id, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<CommentDto>>(lst);
                if(lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<CommentDto>>
                    { 
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<CommentDto>>
                {
                    Data = lstDto,
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<string>> ReplyComment(int commentParentId, Comment comment)
        {
            try
            {
                var checkCommentParentExist = await _commentRepository.GetById(commentParentId);
                if (checkCommentParentExist == null)
                {
                    return new ServiceResponse<string>
                    { 
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                comment.ParentId = commentParentId;
                comment.CreatedAt = DateTime.Now;
                comment.UpdatedAt = null;
                comment.IsActive = true;
                await _commentRepository.Insert(comment);
                return new ServiceResponse<string>
                { 
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 201
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<string>> UpdateComment(int commentId, Comment comment)
        {
            try
            {
                var checkCommentExist = await _commentRepository.GetById(commentId);
                if(checkCommentExist == null)
                {
                    return new ServiceResponse<string>
                    { 
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (!string.IsNullOrEmpty(comment.Body))
                { 
                    checkCommentExist.Body = comment.Body;
                }
                checkCommentExist.UpdatedAt = DateTime.Now;
                await _commentRepository.Update(checkCommentExist);
                return new ServiceResponse<string>
                {
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 204
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
