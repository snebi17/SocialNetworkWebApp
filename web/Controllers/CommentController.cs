using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using web.Services;
using web.Entities;

namespace web.Controllers 
{
    [ApiController]
    [Route("comments")]
    public class CommentController : ControllerBase 
    {
        private ICommentService commentService;
        CommentController(ICommentService commentService) 
        {
            this.commentService = commentService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create(Comment comment) 
        {
            commentService.CreateComment(comment);
            return Ok(new { message = "Comment has been successfully created!" });
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            commentService.DeleteComment(id);
            return Ok(new { message = "Comment has been successfully deleted!" });
        }

        [Authorize]
        [HttpPut]
        public IActionResult Update(Comment comment) 
        {
            commentService.UpdateComment(comment);
            return Ok(new { message = "Comment has been successfully updated!" });
        }
        
    }
}