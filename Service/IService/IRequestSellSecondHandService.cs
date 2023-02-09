using Entity.Dtos.RequestSellSecondHand;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IRequestSellSecondHandService
    {
        //For Staff or Admin
        Task<ServiceResponse<IEnumerable<RequestSellSecondHandDtoForAd>>> GetRequestSellSecondHandsForAdWithPagination(int page, int pageSize);
        Task<ServiceResponse<int>> CountRequestSellSecondHandsForAd();
        Task<ServiceResponse<RequestSellSecondHandDtoForAd>> GetRequestSellSecondHandByIdForAd(int id);
        //For Cus
        Task<ServiceResponse<IEnumerable<RequestSellSecondHandDto>>> GetRequestSellSecondHandsForCusWithPagination(int userId, int page, int pageSize);
        Task<ServiceResponse<int>> CountRequestSellSecondHandsForCus(int userId);
        Task<ServiceResponse<RequestSellSecondHandDto>> GetRequestSellSecondHandForCus(int id);

        Task<ServiceResponse<int>> CreateNewRequestSellSecondHand(RequestSellSecondHand requestSellSecondHand);
        Task<ServiceResponse<RequestSellSecondHand>> UpdateRequestSellSecondHand(int id, RequestSellSecondHand requestSellSecondHand);

        //Change status and process the logic
        Task<ServiceResponse<string>> ChangeStatusToChecking(int requestId);
        Task<ServiceResponse<string>> ChangeStatusToCancel(int requestId);
        Task<ServiceResponse<int>> ChangeStatusToAccept(int requestId);

    }
}
