

using System.Collections.Generic;

namespace Jarranz.Blog.Api.Entities
{

    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Post> Posts { get; set; }

    }
}
