using AutoMapper;
using Entity.Dtos.Size;
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
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        public SizeService(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;
        }
        public async Task<ServiceResponse<int>> CountSizes()
        {
            try
            {
                var count = await _sizeRepository.CountAll(null);
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

        public async Task<ServiceResponse<int>> CreateNewSize(Size size)
        {
            try
            {
                //Validation in here
                //Starting insert to DB
                size.IsActive = true;
                await _sizeRepository.Insert(size);
                return new ServiceResponse<int>
                {
                    Data = size.Id,
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

        public async Task<ServiceResponse<string>> DisableOrEnableSize(int id)
        {
            try
            {
                var checkExist = await _sizeRepository.GetById(id);
                if (checkExist == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (checkExist.IsActive == true)
                {
                    checkExist.IsActive = false;
                    await _sizeRepository.Save();
                }
                else if(checkExist.IsActive == false)
                {
                    checkExist.IsActive = true;
                    await _sizeRepository.Save();
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

        public async Task<ServiceResponse<SizeDto>> GetSizeById(int id)
        {
            try
            {
                var checkExist = await _sizeRepository.GetById(id);
                var _mapper = config.CreateMapper();
                var sizeDto = _mapper.Map<SizeDto>(checkExist);
                if (checkExist == null)
                {
                    return new ServiceResponse<SizeDto>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<SizeDto>
                {
                    Data = sizeDto,
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

        public async Task<ServiceResponse<IEnumerable<SizeDto>>> GetSizesWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                var lst = await _sizeRepository.GetAllWithPagination(null, null, x => x.Id, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<SizeDto>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<SizeDto>>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<SizeDto>>
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

        public async Task<ServiceResponse<Size>> UpdateSize(int id, Size size)
        {
            try
            {
                var checkExist = await _sizeRepository.GetById(id);
                if (checkExist == null)
                {
                    return new ServiceResponse<Size>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (!string.IsNullOrEmpty(size.SizeName))
                { 
                    checkExist.SizeName = size.SizeName;
                }
                if (!string.IsNullOrEmpty(size.Price.ToString()))
                {
                    checkExist.Price = size.Price;
                }
                if(!string.IsNullOrEmpty(size.ProductId.ToString()))
                {
                    checkExist.ProductId = size.ProductId;
                }
                await _sizeRepository.Update(checkExist);
                return new ServiceResponse<Size>
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
