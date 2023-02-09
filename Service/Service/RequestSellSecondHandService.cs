using AutoMapper;
using Entity.Dtos.RequestSellSecondHand;
using Entity.Enum;
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
    public class RequestSellSecondHandService : IRequestSellSecondHandService
    {
        private readonly IRequestSellSecondHandRepository _requestSellSecondHandRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IProductSecondHandImageRepository _productSecondHandImageRepository;
        private readonly IProductRepository _productRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        }); 

        public RequestSellSecondHandService(IRequestSellSecondHandRepository requestSellSecondHandRepository, IProductImageRepository productImageRepository, IProductSecondHandImageRepository productSecondHandImageRepository, IProductRepository productRepository)
        {
            _requestSellSecondHandRepository = requestSellSecondHandRepository;
            _productImageRepository = productImageRepository;
            _productSecondHandImageRepository = productSecondHandImageRepository;
            _productRepository = productRepository;
        }

        public async Task<ServiceResponse<int>> ChangeStatusToAccept(int requestId)
        {
            try
            {
                RequestStatus checking = RequestStatus.Checking;
                RequestStatus accept = RequestStatus.Accept;
                var checkExist = await _requestSellSecondHandRepository.GetById(requestId);
                if (checkExist == null)
                {
                    return new ServiceResponse<int>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (!checkExist.RequestStatus.Equals(checking.ToString()))
                {
                    return new ServiceResponse<int>
                    {
                        Message = "Can not change status!!!",
                        Success = true,
                        StatusCode = 400
                    };
                }
                checkExist.RequestStatus = accept.ToString();
                checkExist.IsRejected = false;
                await _requestSellSecondHandRepository.Save();
                
                //create new product
                Product pro = new Product
                {
                    Name = checkExist.ProductName + " (Second Hand)",
                    AmountInStore = 1,
                    Price = Convert.ToInt32(checkExist.PriceSell) + ((Convert.ToInt32(checkExist.PriceSell) * 5) / 100),
                    Description = "Hàng: " + checkExist.Quality + " Thời hạn bảo hành: " + checkExist.Warranty,
                    DateCreated = DateTime.Now,
                    IsSecondHand = true,
                    IsActive = true,
                    BrandId = null,
                    CategoryId = null,
                    StoreId = null,
                    IsPreOrder = null
                };
                await _productRepository.Insert(pro);
                //process image
                var lstImagesFromRequestId = await _productSecondHandImageRepository.GetAllWithCondition(x => x.RequestSellSecondHandId == checkExist.Id, null, null, true);
                foreach (var item in lstImagesFromRequestId)
                {
                    ProductImage pic = new ProductImage();
                    pic.ProductId = pro.Id;
                    pic.ImgPath = item.ImgPath;
                    await _productImageRepository.Insert(pic);
                }
                return new ServiceResponse<int>
                { 
                    Data = pro.Id,
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

        public async Task<ServiceResponse<string>> ChangeStatusToCancel(int requestId)
        {
            try
            {
                RequestStatus checking = RequestStatus.Checking;
                RequestStatus cancel = RequestStatus.Cancle;
                var checkExist = await _requestSellSecondHandRepository.GetById(requestId);
                if (checkExist == null)
                {
                    return new ServiceResponse<string>
                    { 
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (!checkExist.RequestStatus.Equals(checking.ToString()))
                {
                    return new ServiceResponse<string>
                    { 
                        Message = "Can not change status!!!",
                        Success = true,
                        StatusCode = 400
                    };
                }
                checkExist.IsRejected = true;
                checkExist.RequestStatus = cancel.ToString();
                await _requestSellSecondHandRepository.Save();
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

        public async Task<ServiceResponse<string>> ChangeStatusToChecking(int requestId)
        {
            try
            {
                RequestStatus checking = RequestStatus.Checking;
                RequestStatus processing = RequestStatus.In_Progress;
                var checkExist = await _requestSellSecondHandRepository.GetById(requestId);
                if (checkExist == null)
                {
                    return new ServiceResponse<string>
                    { 
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (!checkExist.RequestStatus.Equals(processing.ToString()))
                {
                    return new ServiceResponse<string>
                    { 
                        Message = "Can not change status!!!",
                        Success = true,
                        StatusCode = 400
                    };
                }
                checkExist.RequestStatus = checking.ToString();
                await _requestSellSecondHandRepository.Save();
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

        public async Task<ServiceResponse<int>> CountRequestSellSecondHandsForAd()
        {
            try
            {
                var count = await _requestSellSecondHandRepository.CountAll(null);
                if (count <= 0)
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

        public async Task<ServiceResponse<int>> CountRequestSellSecondHandsForCus(int userId)
        {
            try
            {
                var count = await _requestSellSecondHandRepository.CountAll(x => x.UserId == userId);
                if (count <= 0)
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

        public async Task<ServiceResponse<int>> CreateNewRequestSellSecondHand(RequestSellSecondHand requestSellSecondHand)
        {
            try
            {
                //validation in here
                //starting insert into Db
                RequestStatus processing = RequestStatus.In_Progress;
                requestSellSecondHand.RequestStatus = processing.ToString();
                requestSellSecondHand.IsRejected = null;
                requestSellSecondHand.IsActive = true;
                await _requestSellSecondHandRepository.Insert(requestSellSecondHand);
                return new ServiceResponse<int>
                { 
                    Data = requestSellSecondHand.Id,
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

        public async Task<ServiceResponse<RequestSellSecondHandDtoForAd>> GetRequestSellSecondHandByIdForAd(int id)
        {
            try
            {
                List<Expression<Func<RequestSellSecondHand, object>>> includes = new List<Expression<Func<RequestSellSecondHand, object>>>
                {
                    x => x.User
                };
                var checkExist = await _requestSellSecondHandRepository.GetByWithCondition(x => x.Id == id, includes, true);
                var _mapper = config.CreateMapper();
                var requestDto = _mapper.Map<RequestSellSecondHandDtoForAd>(checkExist);
                if(checkExist == null)
                {
                    return new ServiceResponse<RequestSellSecondHandDtoForAd>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<RequestSellSecondHandDtoForAd>
                {
                    Data = requestDto,
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

        public async Task<ServiceResponse<RequestSellSecondHandDto>> GetRequestSellSecondHandForCus(int id)
        {
            try
            {
                var checkExist = await _requestSellSecondHandRepository.GetById(id);
                var _mapper = config.CreateMapper();
                var requestDto = _mapper.Map<RequestSellSecondHandDto>(checkExist);
                if (checkExist == null)
                {
                    return new ServiceResponse<RequestSellSecondHandDto>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<RequestSellSecondHandDto>
                {
                    Data = requestDto,
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

        public async Task<ServiceResponse<IEnumerable<RequestSellSecondHandDtoForAd>>> GetRequestSellSecondHandsForAdWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }
                List<Expression<Func<RequestSellSecondHand, object>>> includes = new List<Expression<Func<RequestSellSecondHand, object>>>
                { 
                    x => x.User
                };
                var lst = await _requestSellSecondHandRepository.GetAllWithPagination(null, includes, x => x.Id, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<RequestSellSecondHandDtoForAd>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<RequestSellSecondHandDtoForAd>>
                    { 
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<RequestSellSecondHandDtoForAd>>
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

        public async Task<ServiceResponse<IEnumerable<RequestSellSecondHandDto>>> GetRequestSellSecondHandsForCusWithPagination(int userId, int page, int pageSize)
        {
            try
            {
                if (page <= 0)
                { 
                    page = 1;
                }
                var lst = await _requestSellSecondHandRepository.GetAllWithPagination(x => x.UserId == userId, null, x => x.Id, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<RequestSellSecondHandDto>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<RequestSellSecondHandDto>>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<RequestSellSecondHandDto>>
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

        public async Task<ServiceResponse<RequestSellSecondHand>> UpdateRequestSellSecondHand(int id, RequestSellSecondHand requestSellSecondHand)
        {
            try
            {
                var checkExist = await _requestSellSecondHandRepository.GetById(id);
                if (checkExist == null)
                {
                    return new ServiceResponse<RequestSellSecondHand>
                    { 
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (!string.IsNullOrEmpty(requestSellSecondHand.ProductName))
                {
                    checkExist.ProductName = requestSellSecondHand.ProductName;
                }
                if (!string.IsNullOrEmpty(requestSellSecondHand.BrandName))
                {
                    checkExist.BrandName = requestSellSecondHand.BrandName;
                }
                if (!string.IsNullOrEmpty(requestSellSecondHand.Quality.ToString()))
                {
                    checkExist.Quality = requestSellSecondHand.Quality;
                }
                if (!string.IsNullOrEmpty(requestSellSecondHand.IsFullbox.ToString()))
                {
                    checkExist.IsFullbox = requestSellSecondHand.IsFullbox;
                }
                if (!string.IsNullOrEmpty(requestSellSecondHand.PriceBuy.ToString()))
                {
                    checkExist.PriceBuy = requestSellSecondHand.PriceBuy;
                }
                if (!string.IsNullOrEmpty(requestSellSecondHand.PriceSell.ToString()))
                {
                    checkExist.PriceSell = requestSellSecondHand.PriceSell;
                }
                if (!string.IsNullOrEmpty(requestSellSecondHand.Warranty))
                {
                    checkExist.Warranty = requestSellSecondHand.Warranty;
                }
                if (!string.IsNullOrEmpty(requestSellSecondHand.Contact))
                {
                    checkExist.Contact = requestSellSecondHand.Contact;
                }
                await _requestSellSecondHandRepository.Update(checkExist);
                return new ServiceResponse<RequestSellSecondHand>
                { 
                    Data = checkExist,
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
