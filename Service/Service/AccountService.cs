using AutoMapper;
using Entity.Dtos.Account;
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
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.AddProfile(new MappingProfile());
        });
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<ServiceResponse<int>> CountAccount()
        {
            try
            {
                var count = await _accountRepository.CountAll(null);
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

        public async Task<ServiceResponse<string>> DisableOrEnableAccount(int id)
        {
            try
            {
                var acc = await _accountRepository.GetById(id);
                if (acc == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                if (acc.IsActive == true)
                {
                    acc.IsActive = false;
                    await _accountRepository.Save();
                }
                else if (acc.IsActive == false)
                {
                    acc.IsActive = true;
                    await _accountRepository.Save();
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

        public async Task<ServiceResponse<AccountDto>> GetAccountById(int id)
        {
            try
            {
                var acc = await _accountRepository.GetById(id);
                var _mapper = config.CreateMapper();
                var accDto = _mapper.Map<AccountDto>(acc);
                if (acc == null)
                {
                    return new ServiceResponse<AccountDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<AccountDto>
                {
                    Data = accDto,
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

        public async Task<ServiceResponse<IEnumerable<AccountDto>>> GetAllAccountWithPagination(int page, int pageSize)
        {
            try
            {
                var lst = await _accountRepository.GetAllWithPagination(null, null, x => x.Id, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<AccountDto>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<AccountDto>>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<AccountDto>>
                {
                    Data = lstDto,
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
    }
}
