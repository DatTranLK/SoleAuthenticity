using AutoMapper;
using Entity.Dtos.Product;
using Entity.Models;
using Repository.IRepository;
using Service.IService;
using Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        }); 

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ServiceResponse<int>> CountProducts()
        {
            try
            {
                var count = await _productRepository.CountAll(null);
                if (count <= 0)
                {
                    return new ServiceResponse<int>
                    {
                        Data = 0,
                        Message = "Successfully",
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<int>
                {
                    Data = count,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<int>> CreateNewProduct(Product product)
        {
            try
            {
                //Validation in here
                //Starting insert to Db
                product.AmountSold = 0;
                product.DateCreated = DateTime.Now;
                product.IsActive = true;
                await _productRepository.Insert(product);
                return new ServiceResponse<int>
                { 
                    Data = product.Id,
                    Message = "Successfully",
                    StatusCode = 201
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<string>> DisableOrEnableProduct(int id)
        {
            try
            {
                var pro = await _productRepository.GetById(id);
                if (pro == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                if (pro.IsActive == true)
                {
                    pro.IsActive = false;
                    await _productRepository.Save();
                }
                else if (pro.IsActive == false)
                {
                    pro.IsActive = true;
                    await _productRepository.Save();
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

        public async Task<ServiceResponse<ProductDto>> GetProductById(int id)
        {
            try
            {
                var pro = await _productRepository.GetById(id);
                var _mapper = config.CreateMapper();
                var proDto = _mapper.Map<ProductDto>(pro);
                if (pro == null)
                {
                    return new ServiceResponse<ProductDto>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<ProductDto>
                {
                    Data = proDto,
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

        public async Task<ServiceResponse<IEnumerable<ProductDto>>> GetProductsWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                var lst = await _productRepository.GetAllWithPagination(null, null, x => x.Id, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<ProductDto>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<ProductDto>>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<ProductDto>>
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

        public async Task<ServiceResponse<Product>> UpdateProduct(int id, Product product)
        {
            try
            {
                var checkExist = await _productRepository.GetById(id);
                if (checkExist == null)
                {
                    return new ServiceResponse<Product>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if(!string.IsNullOrEmpty(product.Code))
                {
                    checkExist.Code = product.Code;
                }
                if (!string.IsNullOrEmpty(product.Name))
                {
                    checkExist.Code = product.Name;
                }
                if (!string.IsNullOrEmpty(product.AmountInStore.ToString()))
                {
                    checkExist.AmountInStore = product.AmountInStore;
                }
                if (!string.IsNullOrEmpty(product.Price.ToString()))
                {
                    checkExist.Price = product.Price;
                }
                if (!string.IsNullOrEmpty(product.Description))
                {
                    checkExist.Description = product.Description;
                }
                if (!string.IsNullOrEmpty(product.DateCreated.ToString()))
                {
                    checkExist.DateCreated = product.DateCreated;
                }
                if (!string.IsNullOrEmpty(product.BrandId.ToString()))
                {
                    checkExist.BrandId = product.BrandId;
                }
                if (!string.IsNullOrEmpty(product.CategoryId.ToString()))
                {
                    checkExist.CategoryId = product.CategoryId;
                }
                if (!string.IsNullOrEmpty(product.StoreId.ToString()))
                {
                    checkExist.StoreId = product.StoreId;
                }
                await _productRepository.Update(checkExist);
                return new ServiceResponse<Product>
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
