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
    [Route("api/product-secondhand-images")]
    [ApiController]
    public class ProductSecondHandImageController : ControllerBase
    {
        private readonly IProductSecondHandImageService _productSecondHandImageService;

        public ProductSecondHandImageController(IProductSecondHandImageService productSecondHandImageService)
        {
            _productSecondHandImageService = productSecondHandImageService;
        }
        [HttpGet("request/{requestId}", Name = "GetProductSecondHandImagesByRequestId")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ProductSecondHandImage>>>> GetProductSecondHandImagesByRequestId(int requestId)
        {
            try
            {
                var res = await _productSecondHandImageService.GetProductSecondHandImagesByRequestId(requestId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: "+ex.Message);
            }
        }
        [HttpGet("request/{requestId}/Count", Name = "CountProductSecondHandImagesByRequestId")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<int>>> CountProductSecondHandImagesByRequestId(int requestId)
        {
            try
            {
                var res = await _productSecondHandImageService.CountAll(requestId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPost("product-secondhand-image", Name = "CreateNewProductSecondHandImage")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<int>>> CreateNewProductSecondHandImage([FromBody]ProductSecondHandImage productSecondHandImage)
        {
            try
            {
                var res = await _productSecondHandImageService.CreateNewProductSecondHandImage(productSecondHandImage);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("product-secondhand-image", Name = "UpdateProductSecondHandImage")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<ProductSecondHandImage>>> UpdateProductSecondHandImage(int id, [FromBody] ProductSecondHandImage productSecondHandImage)
        {
            try
            {
                var res = await _productSecondHandImageService.UpdateProductSecondHandImage(id, productSecondHandImage);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
