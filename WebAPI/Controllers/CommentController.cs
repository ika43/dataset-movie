using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.Exceptions;
using Application.ICommands.ICommentCommands;
using Application.SearchObj;
using EfCommands.EfCommentCommands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICreateCommentComand _createComment;
        private readonly IGetOneCommentCommand _getOneComment;
        private readonly IGetCommentComand _getComment;
        private readonly IUpdateCommentComand _updateComment;
        private readonly IDeleteCommentComand _deleteComment;
        public CommentController(ICreateCommentComand createComment, IGetOneCommentCommand getOneComment, IGetCommentComand getComment, IUpdateCommentComand updateComment, IDeleteCommentComand deleteComment)
        {
            _createComment = createComment;
            _getOneComment = getOneComment;
            _getComment = getComment;
            _updateComment = updateComment;
            _deleteComment = deleteComment;
        }
        // GET: api/Comment
        [HttpGet]
        public IActionResult Get([FromQuery] CommentSearch obj)
        {
            try
            {
                var comments = _getComment.Execute(obj);
                return Ok(comments);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // GET: api/Comment/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                var comment = _getOneComment.Execute(id);
                return Ok(comment);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // POST: api/Comment
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateCommentDto obj)
        {
            var currentUser = HttpContext.User;
            if(currentUser.HasClaim(c=>c.Type == "UserId"))
            {
                var userId = currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
                obj.UserId = userId;
            }
            try
            {
                _createComment.Execute(obj);
                return StatusCode(201);
            }
            catch (EntityNotFoundException)
            {
                return UnprocessableEntity();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // PUT: api/Comment/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] UpdateCommentDto obj)
        {
            obj.Id = id;
            try
            {
                _updateComment.Execute(obj);
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _deleteComment.Execute(id);
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
