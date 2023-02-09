using Entity.Models;
using Repository.IRepository;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ProductSecondHandImageService : IProductSecondHandImageService
    {
        private readonly IProductSecondHandImageRepository _productSecondHandImageRepository;

        public ProductSecondHandImageService(IProductSecondHandImageRepository productSecondHandImageRepository)
        {
            _productSecondHandImageRepository = productSecondHandImageRepository;
        }
        public async Task<ServiceResponse<int>> CountAll(int requestId)
        {
            try
            {
                var count = await _productSecondHandImageRepository.CountAll(x => x.RequestSellSecondHandId == requestId);
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

        public async Task<ServiceResponse<int>> CreateNewProductSecondHandImage(ProductSecondHandImage productSecondHandImage)
        {
            try
            {
                //validation in here
                // starting insert into Db
                await _productSecondHandImageRepository.Insert(productSecondHandImage);
                return new ServiceResponse<int>
                { 
                    Data = productSecondHandImage.Id,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 201
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<IEnumerable<ProductSecondHandImage>>> GetProductSecondHandImagesByRequestId(int requestId)
        {
            try
            {
                var lst = await _productSecondHandImageRepository.GetAllWithCondition(x => x.RequestSellSecondHandId == requestId, null, x => x.Id, true);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<ProductSecondHandImage>>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<ProductSecondHandImage>>
                {
                    Data = lst,
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

        public async Task<ServiceResponse<ProductSecondHandImage>> UpdateProductSecondHandImage(int productSecondHandImageId, ProductSecondHandImage productSecondHandImage)
        {
            try
            {
                var checkExist = await _productSecondHandImageRepository.GetById(productSecondHandImageId);
                if (checkExist == null)
                {
                    return new ServiceResponse<ProductSecondHandImage>
                    { 
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (!string.IsNullOrEmpty(productSecondHandImage.ImgPath))
                { 
                    checkExist.ImgPath = productSecondHandImage.ImgPath;
                }
                if (!string.IsNullOrEmpty(productSecondHandImage.RequestSellSecondHandId.ToString()))
                {
                    checkExist.RequestSellSecondHandId = productSecondHandImage.RequestSellSecondHandId;
                }
                await _productSecondHandImageRepository.Update(checkExist);
                return new ServiceResponse<ProductSecondHandImage>
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
