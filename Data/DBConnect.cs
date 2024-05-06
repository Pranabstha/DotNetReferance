using Authentication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Data
{
    public class DBConnect : IdentityDbContext<ApplicationUser>
    {
        public DBConnect(DbContextOptions options) : base(options)
        { 
        
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlServer("Server=localhost;Database=CWDatabase;Trusted_connection=True;Encrypt=False;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<BlogReaction> BlogReactions { get; set; }

        public DbSet<CommentReaction> CommentReactions { get; set; }
    }
}
