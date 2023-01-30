using AutoMapper;
using Entity.Dtos.ShoeCheck;
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
    public class ShoeCheckService : IShoeCheckService
    {
        private readonly IShoeCheckRepository _shoeCheckRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        public ShoeCheckService(IShoeCheckRepository shoeCheckRepository)
        {
            _shoeCheckRepository = shoeCheckRepository;
        }

        public async Task<ServiceResponse<string>> ChangeStatusToChecking(int id)
        {
            try
            {
                StatusChecking processing = StatusChecking.PROCESSING;
                StatusChecking checking = StatusChecking.CHECKING;

                var checkExist = await _shoeCheckRepository.GetById(id);
                if (checkExist == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (!checkExist.StatusChecking.Equals(processing.ToString()))
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Can not change status!!!",
                        Success = true,
                        StatusCode = 400
                    };
                }
                else 
                {
                    checkExist.StatusChecking = checking.ToString();
                    await _shoeCheckRepository.Save();
                }
                return new ServiceResponse<string>
                {
                    Message = "Sucessfully",
                    Success = true,
                    StatusCode = 204
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<string>> ConfirmCheckedShoe(int id, ConfirmCheckedShoe confirmCheckedShoe)
        {
            try
            {
                var checkExist = await _shoeCheckRepository.GetById(id);
                if (checkExist == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Successfully",
                        Success = true,
                        StatusCode = 200
                    };
                }
                StatusChecking shoeHasChecked = StatusChecking.CHECKED;
                StatusChecking checking = StatusChecking.CHECKING;
                
                if (!checkExist.StatusChecking.Equals(checking.ToString()))
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Can not change the status!!!",
                        Success = true,
                        StatusCode = 400
                    };
                }
                else
                {
                    checkExist.DateCompletedChecking = DateTime.Now;
                    checkExist.StatusChecking = shoeHasChecked.ToString();
                    checkExist.IsAuthentic = confirmCheckedShoe.IsAuthentic;
                    checkExist.StaffId = confirmCheckedShoe.StaffId;
                    await _shoeCheckRepository.Update(checkExist);
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

        public async Task<ServiceResponse<int>> CountForAdmin()
        {
            try
            {
                var count = await _shoeCheckRepository.CountAll(null);
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

        public async Task<ServiceResponse<int>> CountForCus(int cusId)
        {
            try
            {
                var count = await _shoeCheckRepository.CountAll(x => x.CustomerId == cusId && x.IsActive == true);
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

        public async Task<ServiceResponse<int>> CountForStaff(int staffId)
        {
            try
            {
                var count = await _shoeCheckRepository.CountAll(x => x.StaffId == staffId);
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

        public async Task<ServiceResponse<int>> CreateNewShoeCheck(ShoeCheck shoeCheck)
        {
            try
            {
                //Validation in here
                //Starting insert to Db
                shoeCheck.DateSubmitted = DateTime.Now;
                shoeCheck.DateCompletedChecking = null;
                StatusChecking statusProcessing = StatusChecking.PROCESSING;
                shoeCheck.StatusChecking = statusProcessing.ToString();
                shoeCheck.IsActive = true;
                shoeCheck.IsAuthentic = false;
                shoeCheck.StaffId = null;
                await _shoeCheckRepository.Insert(shoeCheck);
                return new ServiceResponse<int>
                {
                    Data = shoeCheck.Id,
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

        public async Task<ServiceResponse<string>> DisableOrEnableShoeCheck(int id)
        {
            try
            {
                var shoeCheck = await _shoeCheckRepository.GetById(id);
                if (shoeCheck == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                if (shoeCheck.IsActive == true)
                {
                    shoeCheck.IsActive = false;
                    await _shoeCheckRepository.Save();
                }
                else if (shoeCheck.IsActive == false)
                {
                    shoeCheck.IsActive = true;
                    await _shoeCheckRepository.Save();
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

        public async Task<ServiceResponse<ShoeCheckDtoForAdmin>> GetShoeCheckByIdForAdmin(int id)
        {
            try
            {
                var shoeCheck = await _shoeCheckRepository.GetById(id);
                var _mapper = config.CreateMapper();
                var shoeCheckDto = _mapper.Map<ShoeCheckDtoForAdmin>(shoeCheck);
                if (shoeCheck == null)
                {
                    return new ServiceResponse<ShoeCheckDtoForAdmin>
                    { 
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<ShoeCheckDtoForAdmin>
                {
                    Data = shoeCheckDto,
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

        public async Task<ServiceResponse<ShoeCheckDtoForCustomer>> GetShoeCheckByIdForCustomer(int id)
        {
            var shoeCheck = await _shoeCheckRepository.GetById(id);
            var _mapper = config.CreateMapper();
            var shoeCheckDto = _mapper.Map<ShoeCheckDtoForCustomer>(shoeCheck);
            if (shoeCheck == null)
            {
                return new ServiceResponse<ShoeCheckDtoForCustomer>
                {
                    Message = "No rows",
                    StatusCode = 200,
                    Success = true
                };
            }
            return new ServiceResponse<ShoeCheckDtoForCustomer>
            {
                Data = shoeCheckDto,
                Message = "Successfully",
                StatusCode = 200,
                Success = true
            };
        }

        public async Task<ServiceResponse<ShoeCheckDtoForStaff>> GetShoeCheckByIdForStaff(int id)
        {
            var shoeCheck = await _shoeCheckRepository.GetById(id);
            var _mapper = config.CreateMapper();
            var shoeCheckDto = _mapper.Map<ShoeCheckDtoForStaff>(shoeCheck);
            if (shoeCheck == null)
            {
                return new ServiceResponse<ShoeCheckDtoForStaff>
                {
                    Message = "No rows",
                    StatusCode = 200,
                    Success = true
                };
            }
            return new ServiceResponse<ShoeCheckDtoForStaff>
            {
                Data = shoeCheckDto,
                Message = "Successfully",
                StatusCode = 200,
                Success = true
            };
        }

        public async Task<ServiceResponse<IEnumerable<ShoeCheckDtoForAdmin>>> GetShoeChecksForAdminWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }
                List<Expression<Func<ShoeCheck, object>>> includes = new List<Expression<Func<ShoeCheck, object>>>
                { 
                    x => x.Customer,
                    x => x.Staff
                };
                var lst = await _shoeCheckRepository.GetAllWithPagination(null, includes, x => x.Id, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<ShoeCheckDtoForAdmin>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<ShoeCheckDtoForAdmin>>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<ShoeCheckDtoForAdmin>>
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

        public async Task<ServiceResponse<IEnumerable<ShoeCheckDtoForCustomer>>> GetShoeChecksForCustomerWithPagination(int cusId, int page, int pageSize)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }
                var lst = await _shoeCheckRepository.GetAllWithPagination(x => x.CustomerId == cusId && x.IsActive == true, null, x => x.Id, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<ShoeCheckDtoForCustomer>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<ShoeCheckDtoForCustomer>>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<ShoeCheckDtoForCustomer>>
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

        public async Task<ServiceResponse<IEnumerable<ShoeCheckDtoForStaff>>> GetShoeChecksForStaffWithPagination(int staffId, int page, int pageSize)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }
                var lst = await _shoeCheckRepository.GetAllWithPagination(x => x.StaffId == staffId, null, x => x.Id, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<ShoeCheckDtoForStaff>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<ShoeCheckDtoForStaff>>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<ShoeCheckDtoForStaff>>
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
    }
}
