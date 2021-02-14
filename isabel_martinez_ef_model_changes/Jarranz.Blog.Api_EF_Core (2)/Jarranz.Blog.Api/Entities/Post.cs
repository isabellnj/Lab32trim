using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Jarranz.Blog.Api.Entities
{

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? CategoryId { get; set; }
       
        public Category Category { get; set; }

        public DateTime DateTime { get; set; }
        public ICollection<Tag> Tags { get; set; }

    }
}
