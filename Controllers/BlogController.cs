using Authentication.Models;
using Authentication.Data;
using Authentication.Interfaces;
using Authentication.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Data;

namespace BIsleriumCW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly DBConnect _dbContext;
        private readonly IUserAuthenticationRepository _userAuthenticationRepository;
        //private IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public BlogController(DBConnect dbContext, UserManager<ApplicationUser> userManager, IUserAuthenticationRepository userAuthenticationRepository)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _userAuthenticationRepository = userAuthenticationRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(Blog request)
        {
            //var user = await _userManager.GetUserAsync(User);

            //if (user == null)
            //{
            //    return Unauthorized();
            //}

            var AddBlog = new Blog()
            {
                BlogTitle = request.BlogTitle,
                BlogDescription = request.BlogDescription,
                BlogImageUrl = request.BlogImageUrl,
                UserId = _userAuthenticationRepository.GetUserId(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
            Console.WriteLine(_userAuthenticationRepository.GetUserId());
            await _dbContext.Blogs.AddAsync(AddBlog);
            await _dbContext.SaveChangesAsync();

            return Ok(AddBlog);
        }

        //This Get method is to list all the available Blog in the SYSTEM.
        [HttpGet]
        //[Authorize]
        [Route("ListBlogs")]
        public async Task<IActionResult> GetBlogs()
        {
            var blogs = await _dbContext.Blogs.ToListAsync();

            return Ok(blogs);
        }


        //Switch IsDeleted into true / into false
        [HttpPut]
        [Route("ToggleDelete/{id}")]
        public async Task<IActionResult> ToggleDelete(int id)
        {
            var blog = await _dbContext.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound(); // Blog not found
            }

            // Toggle the IsDeleted property
            blog.IsDeleted = !blog.IsDeleted;
            await _dbContext.SaveChangesAsync();

            return Ok(blog); // Return the updated blog
        }

        //Update Blog
        [HttpPut]
        [Route("UpdateBlog/{id}")]
        public async Task<IActionResult> UpdateBlog(int id, Blog updatedBlog)
        {
            var blog = await _dbContext.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound(); // Blog not found
            }

            // Update the properties of the existing blog with the values from the updatedBlog
            blog.BlogTitle = updatedBlog.BlogTitle ?? blog.BlogTitle;
            blog.BlogDescription = updatedBlog.BlogDescription ?? blog.BlogDescription;
            blog.BlogImageUrl = updatedBlog.BlogImageUrl ?? blog.BlogImageUrl;

            // Set the UpdatedAt timestamp to the current date and time
            blog.UpdatedAt = DateTime.Now;

            // Save the changes to the database
            await _dbContext.SaveChangesAsync();

            return Ok(blog); // Return the updated blog
        }

        //Only Fetch Blog with IsDeleted False
        [HttpGet]
        [Route("ListActiveBlogs")]
        public async Task<IActionResult> GetActiveBlogs()
        {
            var activeBlogs = await _dbContext.Blogs
                                          .Where(blog => !blog.IsDeleted) // Filter where IsDeleted is false
                                          .ToListAsync();

            return Ok(activeBlogs);
        }


        [HttpGet]
        [Route("ListRandomActiveBlogs")]
        public async Task<IActionResult> GetRandomActiveBlogs()
        {
            var activeBlogs = await _dbContext.Blogs
                                          .Where(blog => !blog.IsDeleted) // Filter where IsDeleted is false
                                          .OrderBy(o => Guid.NewGuid()) // Randomize the order
                                          .ToListAsync();

            return Ok(activeBlogs);
        }

        //[HttpGet]
        //[Route("ListActiveBlogsByPopularity")]
        //public async Task<IActionResult> GetActiveBlogsByPopularity()
        //{
        //    var activeBlogs = await _dbContext.Blogs
        //                                  .Where(blog => !blog.IsDeleted) // Filter where IsDeleted is false
        //                                  .OrderByDescending(blog => blog.Popularity) // Order by Popularity (highest to lowest)
        //                                  .ToListAsync();

        //    return Ok(activeBlogs);
        //}
    }
}
