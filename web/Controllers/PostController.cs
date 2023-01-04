using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using web.Entities;
using web.Services;

namespace web.Controllers 
{
    [ApiController]
    [Route("posts")]
    public class PostController : ControllerBase
    {
        private IPostService postService;
        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create(Post post)
        {
            postService.CreatePost(post);
            return Ok(new { message = "Post was successfully created! "});
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get(int id)
        {
            
            return Ok(postService.GetPosts(id));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetPost(int id) 
        {
            return Ok(postService.GetPost(id));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            postService.RemovePost(id);
            return Ok(new { message = "Post was successfully deleted!" });
        }

        [Authorize]
        [HttpPut]
        public IActionResult Update(Post post) 
        {
            postService.UpdatePost(post);
            return Ok(new { message = "Post was successfully updated!" });
        }
    }
}
