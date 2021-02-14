using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jarranz.Blog.Api.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Posts {get; set;}
    }
}
