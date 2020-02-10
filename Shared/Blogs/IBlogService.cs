using System;
using System.Collections.Generic;

namespace Shared.Blogs
{
    public interface IBlogService: IDisposable
    {
        BlogContext Context { get; set; }

        void Add(string url);
        IEnumerable<Blog> Find(string term);
        string MethodWithoutParams();
    }
}