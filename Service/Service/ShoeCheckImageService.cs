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
    public class ShoeCheckImageService : IShoeCheckImageService
    {
        private readonly IShoeCheckImageRepository _shoeCheckImageRepository;

        public ShoeCheckImageService(IShoeCheckImageRepository shoeCheckImageRepository)
        {
            _shoeCheckImageRepository = shoeCheckImageRepository;
        }
        public async Task<ServiceResponse<int>> CountAll(int shoeCheckId)
        {
            try
            {
                var count = await _shoeCheckImageRepository.CountAll(x => x.ShoeCheckId == shoeCheckId);
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

        public async Task<ServiceResponse<int>> CreateNewShoeCheckImage(ShoeCheckImage shoeCheckImage)
        {
            try
            {
                //Validation in here
                //Starting insert to Db
                await _shoeCheckImageRepository.Insert(shoeCheckImage);
                return new ServiceResponse<int>
                { 
                    Data = shoeCheckImage.Id,
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

        public async Task<ServiceResponse<IEnumerable<ShoeCheckImage>>> GetShoeCheckImages(int shoeCheckId)
        {
            try
            {
                var lst = await _shoeCheckImageRepository.GetAllWithCondition(x => x.ShoeCheckId == shoeCheckId, null, x => x.Id, true);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<ShoeCheckImage>>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<ShoeCheckImage>>
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

        public async Task<ServiceResponse<ShoeCheckImage>> UpdateShoeCheckImage(int shoeCheckImageId, ShoeCheckImage shoeCheckImage)
        {
            try
            {
                var checkExist = await _shoeCheckImageRepository.GetById(shoeCheckImageId);
                if (checkExist == null)
                {
                    return new ServiceResponse<ShoeCheckImage>
                    { 
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (!string.IsNullOrEmpty(shoeCheckImage.ImgPath))
                {
                    checkExist.ImgPath = shoeCheckImage.ImgPath;
                }
                if (!string.IsNullOrEmpty(shoeCheckImage.ShoeCheckId.ToString()))
                {
                    checkExist.ShoeCheckId = shoeCheckImage.ShoeCheckId;
                }
                await _shoeCheckImageRepository.Update(checkExist);
                return new ServiceResponse<ShoeCheckImage>
                { 
                    Data = checkExist,
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
    }
}
