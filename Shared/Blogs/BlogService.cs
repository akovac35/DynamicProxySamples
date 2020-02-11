using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Blogs
{
    public class BlogService: IDisposable
    {
        public BlogContext Context { get; set; }

        public BlogService(BlogContext context)
        {
            Context = context;
        }

        public void Add(string url)
        {
            var blog = new Blog { Url = url };
            Context.Blogs.Add(blog);
            Context.SaveChanges();
        }

        public IEnumerable<Blog> Find(string term)
        {
            return Context.Blogs
                .Where(b => b.Url.Contains(term))
                .OrderBy(b => b.Url)
                .ToList();
        }

        public string MethodWithoutParams()
        {
            return "Just a test";
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Context?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
