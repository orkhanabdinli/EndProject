using EndProject.Business.DTOs.PostDTOs;
using EndProject.Business.Services.Interfaces;
using EndProject.Business.Utilities.CustomExceptions.NotFoundExceptions;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreatePost(PostCreateDTO postCreateDTO)
        {
            try
            {
                await _postService.CreatePostAsync(postCreateDTO);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> GetMyPosts(string UserId)
        {
            try
            {
                return Ok(await _postService.GetMyPostsAsync(UserId));
            }
            catch (UserNotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
