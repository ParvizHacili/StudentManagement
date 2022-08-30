using Business.Abstract;
using Business.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentServices _commentService;

        public CommentController(ICommentServices commentService)
        {
            _commentService = commentService;
        }

        [Authorize]
        [HttpPost("addcomment")]
        public IActionResult AddComment(CommentDTO comment)
        {
            var usertoken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityToken(usertoken);
            var email = handler.Claims.FirstOrDefault(x => x.Type == "email").Value;

            _commentService.AddComment(comment.Message, email, comment.Id);

            return Ok(new { status = 200,message="Comment Elave olundu"});
        }
    }
}
