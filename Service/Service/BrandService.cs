using AutoMapper;
using Entity.Dtos.Brand;
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
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.AddProfile(new MappingProfile());
        });
        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public async Task<ServiceResponse<int>> CountBrand()
        {
            try
            {
                var count = await _brandRepository.CountAll(null);
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

        public async Task<ServiceResponse<int>> CreateNewBrand(Brand brand)
        {
            try
            {
                //Validation in here
                //Starting insert to DB
                brand.IsActive = true;
                await _brandRepository.Insert(brand);
                return new ServiceResponse<int>
                { 
                    Data = brand.Id,
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

        public async Task<ServiceResponse<string>> DisableOrEnableBrand(int id)
        {
            try
            {
                var br = await _brandRepository.GetById(id);
                if (br == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                if (br.IsActive == true)
                {
                    br.IsActive = false;
                    await _brandRepository.Save();
                }
                else if (br.IsActive == false)
                {
                    br.IsActive = true;
                    await _brandRepository.Save();
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

        public async Task<ServiceResponse<IEnumerable<BrandDto>>> GetAllBrandsWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                var lst = await _brandRepository.GetAllWithPagination(null, null, x => x.Id, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<BrandDto>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<BrandDto>>
                    { 
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<BrandDto>>
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

        public async Task<ServiceResponse<BrandDto>> GetBrandById(int id)
        {
            try
            {
                var brand = await _brandRepository.GetById(id);
                var _mapper = config.CreateMapper();
                var brandDto = _mapper.Map<BrandDto>(brand);
                if (brand == null)
                {
                    return new ServiceResponse<BrandDto>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<BrandDto>
                { 
                    Data = brandDto,
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

        public async Task<ServiceResponse<Brand>> UpdateBrand(int id, Brand brand)
        {
            try
            {
                var checkBrand = await _brandRepository.GetById(id);
                if (checkBrand == null)
                {
                    return new ServiceResponse<Brand>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (!string.IsNullOrEmpty(brand.Name))
                {
                    checkBrand.Name = brand.Name;
                }
                if (!string.IsNullOrEmpty(brand.Avatar))
                {
                    checkBrand.Avatar = brand.Avatar;
                }
                await _brandRepository.Update(checkBrand);
                return new ServiceResponse<Brand>
                {
                    Data = checkBrand,
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
