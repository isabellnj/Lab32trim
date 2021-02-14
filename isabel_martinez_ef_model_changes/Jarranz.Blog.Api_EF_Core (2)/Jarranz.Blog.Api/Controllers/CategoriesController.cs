using Jarranz.Blog.Api.Contexts;
using Jarranz.Blog.Api.Core;
using Jarranz.Blog.Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Jarranz.Blog.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly BlogContext _blogContext;
        private readonly ILogger _logger;


        public CategoriesController(ILogger<CategoriesController> logger, BlogContext blogContext)
        {
            _blogContext = blogContext;
            _logger = logger;
        }

        // GET: Categories
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Category>> Get()
        {
            _logger.LogInformation("GET all categories");
            return Ok(_blogContext.Categories);//.Include(c=>c.Posts)
        }

        // GET: Categories/5
        [HttpGet("{id}", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Category> Get(int id)
        {
            _logger.LogInformation($"GET category {id}");
            var category = _blogContext.Categories.Include(c => c.Posts).FirstOrDefault(post => post.Id == id);
            return Ok(category);

        }

        // POST: Categories
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<int> Post([FromBody] Category value)
        {
            _logger.LogInformation($"POST category {JsonConvert.SerializeObject(value)}");


            if (_blogContext.Categories.Any())
                value.Id = _blogContext.Categories.Max(p => p.Id) + 1;
            else
                value.Id = 1;

            _blogContext.Categories.Add(value);
            _blogContext.SaveChanges();

            return Ok(value.Id);

        }

        // PUT: Categories/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Put(int id, [FromBody] Category value)
        {
            var categoryToUpdate = _blogContext.Categories.FirstOrDefault(p => p.Id == id);

            if (categoryToUpdate == null)
                return ValidationProblem($"Post with id {id} not found");


            value.Id = id;
            _blogContext.Categories.Remove(categoryToUpdate);
            _blogContext.Categories.Add(value);
            _blogContext.SaveChanges();
            return Ok();


        }

        // DELETE: Categories/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int id)
        {
            _logger.LogInformation($"DELETE category {id}");

            var categoryToDelete = _blogContext.Categories.FirstOrDefault(p => p.Id == id);
            if (categoryToDelete == null)
                return ValidationProblem($"Category with id {id} not found");

            _blogContext.Categories.Remove(categoryToDelete);
            _blogContext.SaveChanges();
            return Ok();
        }

        // DELETE: Categories/5
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Patch(int id, [FromBody] JsonPatchDocument<Category> value)
        {
            _logger.LogInformation($"PATCH category {JsonConvert.SerializeObject(value)}");
                        
            var categoryToUpdate = _blogContext.Categories.FirstOrDefault(p => p.Id == id);
            if (categoryToUpdate == null)
                return ValidationProblem($"Category with id {id} not found");

            value.ApplyTo(categoryToUpdate);//result gets the values from the patch request
            _blogContext.SaveChanges();
            return Ok();

        }
    }
}
