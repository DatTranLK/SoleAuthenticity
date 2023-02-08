using Entity.Dtos.Comment;
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
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet("review/{reviewId}", Name = "GetCommentsByReviewId")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<CommentDto>>>> GetCommentsByReviewId(int reviewId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                var res = await _commentService.GetCommentsByReviewIdWithPagination(reviewId, page, pageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: "+ex.Message);
            }
        }
        [HttpGet("review/{reviewId}/count", Name = "CountCommentsByReviewId")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<int>>> CountCommentsByReviewId(int reviewId)
        {
            try
            {
                var res = await _commentService.CountCommentByReviewId(reviewId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("comment/{commentParentId}", Name = "GetCommentsBycommentParentId")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<CommentDto>>>> GetCommentsBycommentParentId(int commentParentId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                var res = await _commentService.GetSubCommentByParentCommentIdWithPagination(commentParentId, page, pageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("comment/{commentParentId}/count", Name = "CountCommentsBycommentParentId")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ServiceResponse<int>>> CountCommentsBycommentParentId(int commentParentId)
        {
            try
            {
                var res = await _commentService.CountSubCommentByParentCommentId(commentParentId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPost("comment", Name = "CreateNewComment")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<int>>> CreateNewComment([FromBody]Comment comment)
        {
            try
            {
                var res = await _commentService.CreateNewComment(comment);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPost("comment/reply/{commentParentId}", Name = "ReplyComment")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> ReplyComment(int commentParentId, [FromBody] Comment comment)
        {
            try
            {
                var res = await _commentService.ReplyComment(commentParentId, comment);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpDelete("comment", Name = "DisableOrEnableComment")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> DisableOrEnableComment(int commentId)
        {
            try
            {
                var res = await _commentService.DisableOrEnableComment(commentId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("comment", Name = "UpdateComment")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> UpdateComment(int commentId, [FromBody]Comment comment)
        {
            try
            {
                var res = await _commentService.UpdateComment(commentId, comment);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
