using Entity.Dtos.RequestSellSecondHand;
using Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SoleAuthenticity.Controllers
{
    [Route("api/request-sell-secondhands")]
    [ApiController]
    public class RequestSellSecondHandController : ControllerBase
    {
        private readonly IRequestSellSecondHandService _requestSellSecondHandService;

        public RequestSellSecondHandController(IRequestSellSecondHandService requestSellSecondHandService)
        {
            _requestSellSecondHandService = requestSellSecondHandService;
        }
        [HttpGet("/api/admin/request-sell-secondhands", Name = "GetRequestSellSecondHandsForAdWithPagination")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<RequestSellSecondHandDtoForAd>>>> GetRequestSellSecondHandsForAdWithPagination([FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                var res = await _requestSellSecondHandService.GetRequestSellSecondHandsForAdWithPagination(page, pageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: "+ex.Message);
            }
        }
        [HttpGet("/api/admin/request-sell-secondhands/count", Name = "CountRequestSellSecondHandsForAdWithPagination")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<int>>> CountRequestSellSecondHandsForAdWithPagination()
        {
            try
            {
                var res = await _requestSellSecondHandService.CountRequestSellSecondHandsForAd();
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("/api/admin/request-sell-secondhands/{id}", Name = "GetRequestSellSecondHandByIdForAd")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<RequestSellSecondHandDtoForAd>>> GetRequestSellSecondHandByIdForAd(int id)
        {
            try
            {
                var res = await _requestSellSecondHandService.GetRequestSellSecondHandByIdForAd(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("/api/cus/{cusId}/request-sell-secondhands", Name = "GetRequestSellSecondHandsForCusWithPagination")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<RequestSellSecondHandDto>>>> GetRequestSellSecondHandsForCusWithPagination(int cusId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                var res = await _requestSellSecondHandService.GetRequestSellSecondHandsForCusWithPagination(cusId, page, pageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("/api/cus/{cusId}/request-sell-secondhands/count", Name = "CountRequestSellSecondHandsForCus")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<int>>> CountRequestSellSecondHandsForCus(int cusId)
        {
            try
            {
                var res = await _requestSellSecondHandService.CountRequestSellSecondHandsForCus(cusId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("/api/cus/request-sell-secondhands/{id}", Name = "GetRequestSellSecondHandById")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<RequestSellSecondHandDto>>> GetRequestSellSecondHandById(int id)
        {
            try
            {
                var res = await _requestSellSecondHandService.GetRequestSellSecondHandForCus(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost("/api/request-sell-secondhands/request-sell-secondhand", Name = "CreateNewRequestSellSecondHand")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<int>>> CreateNewRequestSellSecondHand([FromBody]RequestSellSecondHand requestSellSecondHand)
        {
            try
            {
                var res = await _requestSellSecondHandService.CreateNewRequestSellSecondHand(requestSellSecondHand);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("/api/request-sell-secondhands/request-sell-secondhand", Name = "UpdateRequestSellSecondHand")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<RequestSellSecondHand>>> UpdateRequestSellSecondHand(int id, [FromBody] RequestSellSecondHand requestSellSecondHand)
        {
            try
            {
                var res = await _requestSellSecondHandService.UpdateRequestSellSecondHand(id, requestSellSecondHand);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("/api/request-sell-secondhands/request-sell-secondhand/request-status/checking", Name = "ChangeStatusToCheckingForRequest")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> ChangeStatusToCheckingForRequest(int id)
        {
            try
            {
                var res = await _requestSellSecondHandService.ChangeStatusToChecking(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("/api/request-sell-secondhands/request-sell-secondhand/request-status/cancel", Name = "ChangeStatusToCancelForRequest")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> ChangeStatusToCancelForRequest(int id)
        {
            try
            {
                var res = await _requestSellSecondHandService.ChangeStatusToCancel(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("/api/request-sell-secondhands/request-sell-secondhand/request-status/accept", Name = "ChangeStatusToAcceptForRequest")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<int>>> ChangeStatusToAcceptForRequest(int id)
        {
            try
            {
                var res = await _requestSellSecondHandService.ChangeStatusToAccept(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
