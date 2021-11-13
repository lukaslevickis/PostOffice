using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostOfficeBackend.DAL.Entities;
using PostOfficeBackend.Dtos;
using PostOfficeBackend.Services;

namespace PostOfficeBackend.Controllers
{
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _postService.GetAllAsync());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            PostDto data = await _postService.GetByIdAsync(id);
            if (data == null)
            {
                return BadRequest();
            }

            return Ok(data);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Post post)
        {
            return Ok(await _postService.CreateAsync(post));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            return Ok(await _postService.UpdateAsync(post));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.DeleteAsync(id);

            return NoContent();
        }
    }
}
