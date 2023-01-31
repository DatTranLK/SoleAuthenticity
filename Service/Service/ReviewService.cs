using AutoMapper;
using Entity.Dtos.Review;
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
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IProductRepository _productRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        public ReviewService(IReviewRepository reviewRepository, IProductRepository productRepository)
        {
            _reviewRepository = reviewRepository;
            _productRepository = productRepository;
        }
        public async Task<ServiceResponse<int>> CountAll()
        {
            try
            {
                var count = await _reviewRepository.CountAll(null);
                if (count <= 0)
                {
                    return new ServiceResponse<int>
                    {
                        Data = 0,
                        Message = "Sucessfully",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<int>
                {
                    Data = count,
                    Message = "Sucessfully",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<int>> CreateNewReview(Review review)
        {
            try
            {
                //Validation in here
                //Starting insert to Db
                var checkProductExist = await _productRepository.GetById(review.ProductId);
                if (checkProductExist == null)
                {
                    return new ServiceResponse<int>
                    {
                        Message = "The Product Id is not exist!!!",
                        Success = true,
                        StatusCode = 200
                    };
                }
                review.IsActive = true;
                await _reviewRepository.Insert(review);
                return new ServiceResponse<int>
                {
                    Data = review.ProductId,
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

        public async Task<ServiceResponse<string>> DisableOrEnableReview(int productId)
        {
            try
            {
                var review = await _reviewRepository.GetById(productId);
                if (review == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                if (review.IsActive == true)
                {
                    review.IsActive = false;
                    await _reviewRepository.Save();
                }
                else if (review.IsActive == false)
                {
                    review.IsActive = true;
                    await _reviewRepository.Save();
                }
                return new ServiceResponse<string>
                {
                    Message = "Successfully",
                    StatusCode = 204,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<ReviewDto>> GetReviewById(int productId)
        {
            try
            {
                var review = await _reviewRepository.GetById(productId);
                var _mapper = config.CreateMapper();
                var reviewDto = _mapper.Map<ReviewDto>(review);
                if (review == null)
                {
                    return new ServiceResponse<ReviewDto>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<ReviewDto>
                {
                    Data = reviewDto,
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

        public async Task<ServiceResponse<IEnumerable<ReviewDto>>> GetReviewsWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }
                List<Expression<Func<Review, object>>> includes = new List<Expression<Func<Review, object>>> 
                {
                    x => x.Staff
                };
                var lst = await _reviewRepository.GetAllWithPagination(null, includes, x => x.ProductId, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<ReviewDto>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<ReviewDto>>
                    { 
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<ReviewDto>>
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

        public async Task<ServiceResponse<Review>> UpdateReview(int productId, Review review)
        {
            try
            {
                var checkExist = await _reviewRepository.GetById(productId);
                if (checkExist == null)
                {
                    return new ServiceResponse<Review>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                /*if (!string.IsNullOrEmpty(review.ProductId.ToString()))
                {
                    var checkProductExist = await _productRepository.GetById(productId);
                    if (checkProductExist == null)
                    {
                        return new ServiceResponse<Review>
                        {
                            Message = "The Product Id is not exist!!!",
                            Success = true,
                            StatusCode = 200
                        };
                    }
                    checkExist.ProductId = review.ProductId;
                }*/
                if (!string.IsNullOrEmpty(review.Title))
                {
                    checkExist.Title = review.Title;
                }
                if (!string.IsNullOrEmpty(review.Avatar))
                {
                    checkExist.Avatar = review.Avatar;
                }
                if (!string.IsNullOrEmpty(review.StaffId.ToString()))
                {
                    checkExist.StaffId = review.StaffId;
                }
                if (!string.IsNullOrEmpty(review.Description))
                {
                    checkExist.Description = review.Description;
                }
                if (!string.IsNullOrEmpty(review.Elements))
                {
                    checkExist.Elements = review.Elements;
                }
                await _reviewRepository.Update(checkExist);
                return new ServiceResponse<Review>
                { 
                    Data = checkExist,
                    Message = "Successfully",
                    StatusCode = 204,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
