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
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;

        public ProductImageService(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }
        public async Task<ServiceResponse<int>> CountAll(int productId)
        {
            try
            {
                var count = await _productImageRepository.CountAll(x => x.ProductId == productId);
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

        public async Task<ServiceResponse<int>> CreateNewProductImage(ProductImage productImage)
        {
            try
            {
                //Validation in here
                //Starting insert to Db
                await _productImageRepository.Insert(productImage);
                return new ServiceResponse<int>
                {
                    Data = productImage.Id,
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

        public async Task<ServiceResponse<IEnumerable<ProductImage>>> GetProductImageById(int productId)
        {
            try
            {
                var lst = await _productImageRepository.GetAllWithCondition(x => x.ProductId == productId, null, null, true);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<ProductImage>>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<ProductImage>>
                {
                    Data = lst,
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

        public async Task<ServiceResponse<ProductImage>> UpdateProductImage(int id, ProductImage productImage)
        {
            try
            {
                var checkExist = await _productImageRepository.GetById(id);
                if (checkExist == null)
                {
                    return new ServiceResponse<ProductImage>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (!string.IsNullOrEmpty(productImage.ImgPath))
                {
                    checkExist.ImgPath = productImage.ImgPath;
                }
                if (!string.IsNullOrEmpty(productImage.ProductId.ToString()))
                {
                    checkExist.ProductId = productImage.ProductId;
                }
                await _productImageRepository.Update(checkExist);
                return new ServiceResponse<ProductImage>
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
