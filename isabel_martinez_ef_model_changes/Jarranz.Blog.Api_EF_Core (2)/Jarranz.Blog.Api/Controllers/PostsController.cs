using Jarranz.Blog.Api.Contexts;
using Jarranz.Blog.Api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jarranz.Blog.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ILogger<PostsController> _logger;
        private readonly BlogContext _blogContext;


        public PostsController(ILogger<PostsController> logger, BlogContext blogContext)
        {
            _logger = logger;
            _blogContext = blogContext;
        }

        // GET: Posts
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Post>> Get()
        {
            _logger.LogInformation("GET all posts");

            return Ok(_blogContext.Posts);
        }

        // GET: Posts/5
        [HttpGet("{id}", Name = "GetPost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Post> Get(int id)
        {
            _logger.LogInformation($"GET post {id}");
            var post = _blogContext.Posts.FirstOrDefault(p => p.Id == id);
            return Ok(post);
        }

        // GET: Posts/5
        [HttpGet("Category/{idCategory}", Name = "GetPostByCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Post>> GetByCategory(int idCategory)
        {

            _logger.LogInformation($"GET post {idCategory}");

            var existCategory = _blogContext.Categories.Any(c => c.Id == idCategory);
            if (!existCategory)
                return ValidationProblem($"Category with id {idCategory} not found", "Post", 400, "Validation error");

            var posts = _blogContext.Posts.Where(p => p.CategoryId == idCategory);

            return Ok(posts);

        }

        // POST: Posts
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<int> Post([FromBody] Post value)
        {
            _logger.LogInformation($"POST post {JsonConvert.SerializeObject(value)}"); 

            if (_blogContext.Posts.Any())
                value.Id = _blogContext.Posts.Max(p => p.Id) + 1;
            else
                value.Id = 1;

            if (value.CategoryId.HasValue && !_blogContext.Categories.Any(c => c.Id == value.CategoryId))
                return ValidationProblem($"Category with id {value.CategoryId} not found");

            _blogContext.Posts.Add(value);
            _blogContext.SaveChanges();

            return Ok(value.Id);
        }

        // PUT: Posts/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Put(int id, [FromBody] Post value)
        {
            _logger.LogInformation($"PUT post {JsonConvert.SerializeObject(value)}");

            var postToUpdate = _blogContext.Posts.FirstOrDefault(p => p.Id == id);

            if (postToUpdate == null)
                return ValidationProblem($"Post with id {id} not found");

            if (value.CategoryId.HasValue && !_blogContext.Categories.Any(c => c.Id == value.Id))
                return ValidationProblem($"Category with id {value.Id} not found");

            value.Id = id;
            _blogContext.Posts.Remove(postToUpdate);
            _blogContext.Posts.Add(value);
            _blogContext.SaveChanges();
            return Ok();
        }

        // DELETE: Posts/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int id)
        {
            _logger.LogInformation($"DELETE post {id}");

            var postToDelete = _blogContext.Posts.FirstOrDefault(p => p.Id == id);
            if (postToDelete == null)
                return ValidationProblem($"Post with id {id} not found");

            _blogContext.Posts.Remove(postToDelete);
            _blogContext.SaveChanges();
            return Ok();
        }



        // PATCH: Posts/5
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Patch(int id, [FromBody] JsonPatchDocument<Post> value)
        {
            _logger.LogInformation($"PATCH post {JsonConvert.SerializeObject(value)}");

            var postToUpdate = _blogContext.Posts.FirstOrDefault(p => p.Id == id);

            if (postToUpdate == null)
                return ValidationProblem($"Post with id {id} not found");

            value.ApplyTo(postToUpdate);//result gets the values from the patch request

            _blogContext.SaveChanges();

            return Ok();
        }
    }
}
