using AutoMapper;
using Entity.Dtos.New;
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
    public class NewService : INewService
    {
        private readonly INewRepository _newRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        public NewService(INewRepository newRepository)
        {
            _newRepository = newRepository;
        }
        public async Task<ServiceResponse<int>> CountNews()
        {
            try
            {
                var count = await _newRepository.CountAll(null);
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

        public async Task<ServiceResponse<int>> CreateNewNew(New newAdd)
        {
            try
            {
                //Validation in here
                //Starting insert to Db
                newAdd.DateCreated = DateTime.Now;
                newAdd.IsActive = true;
                await _newRepository.Insert(newAdd);
                return new ServiceResponse<int>
                {
                    Data = newAdd.Id,
                    Message = "Successfully",
                    StatusCode = 201,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<string>> DisableOrEnableNew(int id)
        {
            try
            {
                var newExist = await _newRepository.GetById(id);
                if (newExist == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                if (newExist.IsActive == true)
                {
                    newExist.IsActive = false;
                    await _newRepository.Save();
                }
                else if (newExist.IsActive == false)
                {
                    newExist.IsActive = true;
                    await _newRepository.Save();
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

        public async Task<ServiceResponse<NewDto>> GetNewById(int id)
        {
            try
            {
                var newGet = await _newRepository.GetById(id);
                var _mapper = config.CreateMapper();
                var newDto = _mapper.Map<NewDto>(newGet);
                if (newGet == null)
                {
                    return new ServiceResponse<NewDto>
                    { 
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<NewDto>
                {
                    Data = newDto,
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

        public async Task<ServiceResponse<IEnumerable<NewDto>>> GetNewsWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                var lst = await _newRepository.GetAllWithPagination(null, null, x => x.Id, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<NewDto>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<NewDto>>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<NewDto>>
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

        public async Task<ServiceResponse<New>> UpdateNew(int id, New newUpdate)
        {
            try
            {
                var checkExist = await _newRepository.GetById(id);
                if (checkExist == null)
                {
                    return new ServiceResponse<New>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (!string.IsNullOrEmpty(newUpdate.Title))
                { 
                    checkExist.Title = newUpdate.Title;
                }
                if (!string.IsNullOrEmpty(newUpdate.Avatar))
                {
                    checkExist.Avatar = newUpdate.Avatar;
                }
                if (!string.IsNullOrEmpty(newUpdate.Context))
                {
                    checkExist.Context = newUpdate.Context;
                }
                if (!string.IsNullOrEmpty(newUpdate.DateCreated.ToString()))
                {
                    checkExist.DateCreated = newUpdate.DateCreated;
                }
                await _newRepository.Update(checkExist);
                return new ServiceResponse<New>
                { 
                    Data = checkExist,
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
    }
}
