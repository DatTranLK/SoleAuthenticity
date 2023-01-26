using Entity.Dtos.Brand;
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
    [Route("api/brands")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandservice;

        public BrandController(IBrandService brandservice)
        {
            _brandservice = brandservice;
        }
        [HttpGet(Name = "GetBrands")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<BrandDto>>>> GetBrands([FromQuery]int page, [FromQuery]int pageSize)
        {
            try
            {
                var res = await _brandservice.GetAllBrandsWithPagination(page, pageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("brand/{id}", Name = "GetBrandById")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<BrandDto>>> GetBrandById(int id)
        {
            try
            {
                var res = await _brandservice.GetBrandById(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("count", Name = "GetCount")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<int>>> GetCount()
        {
            try
            {
                var res = await _brandservice.CountBrand();
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpDelete("brand", Name = "DisableOrEnableBrand")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> DisableOrEnableBrand(int id)
        {
            try
            {
                var res = await _brandservice.DisableOrEnableBrand(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPost("brand", Name = "CreateNewBrand")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<BrandDto>>> CreateNewBrand([FromBody]Brand brand)
        {
            try
            {
                var res = await _brandservice.CreateNewBrand(brand);
                return CreatedAtRoute("GetBrandById", new { id = brand.Id}, brand);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("brand", Name = "UpdateBrand")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<Brand>>> UpdateBrand(int id, [FromBody] Brand brand)
        {
            try
            {
                var res = await _brandservice.UpdateBrand(id, brand);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
