using DotNetCoreTraining20230617.DbService.Services;
using DotNetCoreTraining20230617.Mapper;
using DotNetCoreTraining20230617.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreTraining20230617.WebApi.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : BaseController
    {
        private readonly AppDbContext _appDbContext;

        public BlogController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        // Http Method
        // get
        // post
        // put
        // patch
        // delete

        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<IActionResult> Get(int pageNo = 1, int pageSize = 10)
        {
            List<BlogViewModel> model = new();
            try
            {
                if (pageNo == 0)
                {
                    return BadRequest("Invalid Page No.");
                }
                if (pageSize == 0)
                {
                    return BadRequest("Invalid Page Size.");
                }

                var lst = await _appDbContext.Blogs.AsNoTracking().Pagination(pageNo, pageSize).ToListAsync();
                model = lst.Select(x => x.Change()).ToList();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            //return Success();
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post(BlogViewModel blogViewModel)
        {
            BlogDataModel blogDataModel = blogViewModel.Change();
            var model = new { Id = 0 };
            try
            {
                _appDbContext.Blogs.Add(blogDataModel);
                var result = _appDbContext.SaveChanges();
                model = new { Id = blogDataModel.Blog_Id };
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, BlogViewModel blogViewModel)
        {
            if (id == 0)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok();
        }
    }
}