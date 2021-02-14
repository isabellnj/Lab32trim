using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jarranz.Blog.Api.Core
{
    public static class ScopePolicy
    {
        public static List<string> ApiUse => new List<string> { "Api.Use" };
        public static List<string> ApiPosts => new List<string> { "Api.Use", "Api.Posts" };
        public static List<string> ApiCategories => new List<string> { "Api.Use", "Api.Categories" };

    }
}
