using Authentication.Data;
using Authentication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly DBConnect dbContext;

        public CommentController(DBConnect dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("CreateComment")]
        public async Task<IActionResult> CreateComment(Comment request)
        {
            try
            {
                var newComment = new Comment()
                {
                    Comments = request.Comments,
                    BlogId = request.BlogId, // Assuming you want to associate the comment with a specific blog
                    //UserId = request.UserId
                };

                dbContext.Comments.Add(newComment);
                await dbContext.SaveChangesAsync();

                return Ok(newComment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the comment."); // Return an error response
            }
        }


        [HttpPut]
        [Route("UpdateComment/{id}")]
        public async Task<IActionResult> UpdateComment(int id, Comment updatedComment)
        {
            try
            {
                var existingComment = await dbContext.Comments.FindAsync(id);

                if (existingComment == null)
                {
                    return NotFound(); // Return 404 Not Found if the comment doesn't exist
                }

                // Update the properties of the existing comment with the values from the updatedComment
                existingComment.Comments = updatedComment.Comments ?? existingComment.Comments;

                // Save the changes to the database
                await dbContext.SaveChangesAsync();

                return Ok(existingComment); // Return the updated comment
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the comment."); // Return an error response
            }
        }

        [HttpDelete]
        [Route("DeleteComment/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                var comment = await dbContext.Comments.FindAsync(id);

                if (comment == null)
                {
                    return NotFound(); // Return 404 Not Found if the comment doesn't exist
                }

                dbContext.Comments.Remove(comment);
                await dbContext.SaveChangesAsync();

                return NoContent(); // Return 204 No Content indicating successful deletion
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the comment."); // Return an error response
            }
        }

        [HttpGet]
        [Route("GetCommentsByBlogId/{blogId}")]
        public async Task<IActionResult> GetCommentsByBlogId(int blogId)
        {
            try
            {
                var comments = await dbContext.Comments
                                              .Where(comment => comment.BlogId == blogId)
                                              .ToListAsync();

                return Ok(comments); // Return comments associated with the specified blog
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching comments."); // Return an error response
            }
        }

        [HttpGet]
        [Route("GetAllComments")]
        public async Task<IActionResult> GetAllComments()
        {
            try
            {
                var comments = await dbContext.Comments.ToListAsync();

                return Ok(comments); // Return all comments
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching comments."); // Return an error response
            }
        }

    }
}
