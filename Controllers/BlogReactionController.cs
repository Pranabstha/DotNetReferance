using Authentication.Data;
using Authentication.Models;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogReactionController : ControllerBase
    {
        private readonly DBConnect dbContext;
        public BlogReactionController(DBConnect dbContext)
        {
            this.dbContext = dbContext;
        }

    }
}
