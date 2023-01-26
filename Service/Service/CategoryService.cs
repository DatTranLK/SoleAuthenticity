using AutoMapper;
using Entity.Dtos.Category;
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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<ServiceResponse<int>> CountCategories()
        {
            try
            {
                var count = await _categoryRepository.CountAll(null);
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

        public async Task<ServiceResponse<int>> CreateNewCategory(Category category)
        {
            try
            {
                //Validation in here
                //Starting insert to DB
                category.IsActive = true;
                await _categoryRepository.Insert(category);
                return new ServiceResponse<int>
                {
                    Data = category.Id,
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

        public async Task<ServiceResponse<string>> DisableOrEnableCategory(int id)
        {
            try
            {
                var cate = await _categoryRepository.GetById(id);
                if (cate == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                if (cate.IsActive == true)
                {
                    cate.IsActive = false;
                    await _categoryRepository.Save();
                }
                else if (cate.IsActive == false)
                {
                    cate.IsActive = true;
                    await _categoryRepository.Save();
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

        public async Task<ServiceResponse<IEnumerable<CategoryDto>>> GetCategoriesWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                var lst = await _categoryRepository.GetAllWithPagination(null, null, x => x.Id, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<CategoryDto>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<CategoryDto>>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<CategoryDto>>
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

        public async Task<ServiceResponse<CategoryDto>> GetCategoryById(int id)
        {
            try
            {
                var cate = await _categoryRepository.GetById(id);
                var _mapper = config.CreateMapper();
                var cateDto = _mapper.Map<CategoryDto>(cate);
                if (cate == null)
                {
                    return new ServiceResponse<CategoryDto>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<CategoryDto>
                {
                    Data = cateDto,
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

        public async Task<ServiceResponse<Category>> UpdateCategory(int id, Category category)
        {
            try
            {
                var checkExist = await _categoryRepository.GetById(id);
                if (checkExist == null)
                {
                    return new ServiceResponse<Category>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (!string.IsNullOrEmpty(checkExist.Name))
                { 
                    checkExist.Name = category.Name;
                }
                await _categoryRepository.Update(checkExist);
                return new ServiceResponse<Category>
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
