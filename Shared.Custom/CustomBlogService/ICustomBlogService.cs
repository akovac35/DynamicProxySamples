using Shared.Blogs;
using System;
using System.Collections.Generic;

namespace Shared.Custom.CustomBlogService
{
    public interface ICustomBlogService: IDisposable
    {
        BlogContext Context { get; set; }

        void Add(string url);
        IEnumerable<CustomBlogServiceDto> Find(string term);
        string MethodWithoutParams();
    }
}