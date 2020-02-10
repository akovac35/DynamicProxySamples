using Shared.Blogs;
using Shared.CustomBlogService.Dto;
using System;
using System.Collections.Generic;

namespace Shared.CustomBlogService
{
    public interface ICustomBlogService: IDisposable
    {
        BlogContext Context { get; set; }

        void Add(string url);
        IEnumerable<CustomBlogServiceDto> Find(string term);
        string MethodWithoutParams();
    }
}