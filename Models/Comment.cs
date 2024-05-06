using System.ComponentModel.DataAnnotations;

namespace Authentication.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public string Comments { get; set; }

        //FK is user and blog

        public int BlogId { get; set; } //FK

        public Blog Blog { get; set; } //referencing to Blog
    }
}
