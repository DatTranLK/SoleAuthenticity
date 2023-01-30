using Entity.Dtos.ShoeCheck;
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
    [Route("api/shoe-checks")]
    [ApiController]
    public class ShoeCheckController : ControllerBase
    {
        private readonly IShoeCheckService _shoeCheckService;

        public ShoeCheckController(IShoeCheckService shoeCheckService)
        {
            _shoeCheckService = shoeCheckService;
        }
        [HttpGet("admin", Name = "GetShoeChecksForAdmin")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ShoeCheckDtoForAdmin>>>> GetShoeChecksForAdmin([FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                var res = await _shoeCheckService.GetShoeChecksForAdminWithPagination(page, pageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: "+ex.Message);
            }
        }
        [HttpGet("admin/count", Name = "CountShoeChecksForAdmin")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<int>>> CountShoeChecksForAdmin()
        {
            try
            {
                var res = await _shoeCheckService.CountForAdmin();
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("admin/shoe-check/{id}", Name = "GetShoeCheckByIdForAdmin")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<ShoeCheckDtoForAdmin>>> GetShoeCheckByIdForAdmin(int id)
        {
            try
            {
                var res = await _shoeCheckService.GetShoeCheckByIdForAdmin(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("customer/{cusId}", Name = "GetShoeChecksForCustomer")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ShoeCheckDtoForCustomer>>>> GetShoeChecksForCustomer(int cusId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                var res = await _shoeCheckService.GetShoeChecksForCustomerWithPagination(cusId, page, pageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("customer/count/{cusId}", Name = "CountShoeChecksForCustomer")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<int>>> CountShoeChecksForCustomer(int cusId)
        {
            try
            {
                var res = await _shoeCheckService.CountForCus(cusId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("customer/shoe-check/{id}", Name = "GetShoeCheckByIdForCustomer")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<ShoeCheckDtoForCustomer>>> GetShoeCheckByIdForCustomer(int id)
        {
            try
            {
                var res = await _shoeCheckService.GetShoeCheckByIdForCustomer(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("staff/{staffId}", Name = "GetShoeChecksForStaff")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ShoeCheckDtoForStaff>>>> GetShoeChecksForStaff(int staffId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                var res = await _shoeCheckService.GetShoeChecksForStaffWithPagination(staffId, page, pageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("staff/count/{staffId}", Name = "CountShoeChecksForStaff")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<int>>> CountShoeChecksForStaff(int staffId)
        {
            try
            {
                var res = await _shoeCheckService.CountForStaff(staffId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("staff/shoe-check/{id}", Name = "GetShoeCheckByIdForStaff")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<ShoeCheckDtoForStaff>>> GetShoeCheckByIdForStaff(int id)
        {
            try
            {
                var res = await _shoeCheckService.GetShoeCheckByIdForStaff(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpDelete("shoe-check", Name = "DisableOrEnableShoeCheck")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> DisableOrEnableShoeCheck(int id)
        {
            try
            {
                var res = await _shoeCheckService.DisableOrEnableShoeCheck(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPost("shoe-check", Name = "CreateNewShoeCheck")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<int>>> CreateNewShoeCheck([FromBody]ShoeCheck shoeCheck)
        {
            try
            {
                var res = await _shoeCheckService.CreateNewShoeCheck(shoeCheck);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("shoe-check/{id}", Name = "ChangeStatusToChecking")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> ChangeStatusToChecking(int id)
        {
            try
            {
                var res = await _shoeCheckService.ChangeStatusToChecking(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("shoe-check/confirm/{id}", Name = "ConfirmCheckedShoe")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> ConfirmCheckedShoe(int id, [FromBody]ConfirmCheckedShoe confirmCheckedShoe)
        {
            try
            {
                var res = await _shoeCheckService.ConfirmCheckedShoe(id, confirmCheckedShoe);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
