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
    [Route("api/shoe-check-images")]
    [ApiController]
    public class ShoeCheckImageController : ControllerBase
    {
        private readonly IShoeCheckImageService _shoeCheckImageService;

        public ShoeCheckImageController(IShoeCheckImageService shoeCheckImageService)
        {
            _shoeCheckImageService = shoeCheckImageService;
        }
        [HttpGet("{shoeCheckId}", Name = "GetShoeCheckImages")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ShoeCheckImage>>>> GetShoeCheckImages(int shoeCheckId)
        {
            try
            {
                var res = await _shoeCheckImageService.GetShoeCheckImages(shoeCheckId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: "+ex.Message);
            }
        }
        [HttpGet("count/{shoeCheckId}", Name = "CountShoeCheckImages")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<int>>> CountShoeCheckImages(int shoeCheckId)
        {
            try
            {
                var res = await _shoeCheckImageService.CountAll(shoeCheckId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPost("shoe-check-image", Name = "CreateNewShoeCheckImage")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<int>>> CreateNewShoeCheckImage([FromBody]ShoeCheckImage shoeCheckImage)
        {
            try
            {
                var res = await _shoeCheckImageService.CreateNewShoeCheckImage(shoeCheckImage);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("shoe-check-image/{id}", Name = "UpdateShoeCheckImage")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<ShoeCheckImage>>> UpdateShoeCheckImage(int id, [FromBody] ShoeCheckImage shoeCheckImage)
        {
            try
            {
                var res = await _shoeCheckImageService.UpdateShoeCheckImage(id, shoeCheckImage);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
