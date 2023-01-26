using AutoMapper;
using Entity.Dtos.Store;
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
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        public async Task<ServiceResponse<int>> CountStores()
        {
            try
            {
                var count = await _storeRepository.CountAll(null);
                if (count <= 0)
                { 
                    return new ServiceResponse<int> { 
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

        public async Task<ServiceResponse<int>> CreateNewStore(Store store)
        {
            try
            {
                //Validation in here
                //Starting insert to Db
                store.IsActive = true;
                await _storeRepository.Insert(store);
                return new ServiceResponse<int>
                { 
                    Data = store.Id,
                    Message = "Successfully",
                    StatusCode = 201
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<string>> DisableOrEnableStore(int id)
        {
            try
            {
                var store = await _storeRepository.GetById(id);
                if (store == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                if (store.IsActive == true)
                {
                    store.IsActive = false;
                    await _storeRepository.Save();
                }
                else if (store.IsActive == false)
                {
                    store.IsActive = true;
                    await _storeRepository.Save();
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

        public async Task<ServiceResponse<StoreDto>> GetStoreById(int id)
        {
            try
            {
                var store = await _storeRepository.GetById(id);
                var _mapper = config.CreateMapper();
                var storeDto = _mapper.Map<StoreDto>(store);
                if (store == null)
                {
                    return new ServiceResponse<StoreDto>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<StoreDto>
                {
                    Data = storeDto,
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

        public async Task<ServiceResponse<IEnumerable<StoreDto>>> GetStoresWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                var lst = await _storeRepository.GetAllWithPagination(null, null, x => x.Id, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<StoreDto>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<StoreDto>>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<StoreDto>>
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

        public async Task<ServiceResponse<Store>> UpdateStore(int id, Store store)
        {
            try
            {
                var checkExist = await _storeRepository.GetById(id);
                if (checkExist == null)
                {
                    return new ServiceResponse<Store>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (!string.IsNullOrEmpty(store.Name))
                {
                    checkExist.Name = store.Name;
                }
                if (!string.IsNullOrEmpty(store.Avatar))
                {
                    checkExist.Avatar = store.Avatar;
                }
                if (!string.IsNullOrEmpty(store.Address))
                {
                    checkExist.Address = store.Address;
                }
                if (!string.IsNullOrEmpty(store.Phone))
                {
                    checkExist.Phone = store.Phone;
                }
                if (!string.IsNullOrEmpty(store.Rate.ToString()))
                {
                    checkExist.Rate = store.Rate;
                }
                await _storeRepository.Update(checkExist);
                return new ServiceResponse<Store>
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
