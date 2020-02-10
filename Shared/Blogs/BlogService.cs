using System.Collections.Generic;
using System.Linq;

namespace Shared.Blogs
{
    public class BlogService : IBlogService
    {
        public virtual BlogContext Context { get; set; }

        public BlogService(BlogContext context)
        {
            Context = context;
        }

        public virtual void Add(string url)
        {
            var blog = new Blog { Url = url };
            Context.Blogs.Add(blog);
            Context.SaveChanges();
        }

        public virtual IEnumerable<Blog> Find(string term)
        {
            return Context.Blogs
                .Where(b => b.Url.Contains(term))
                .OrderBy(b => b.Url)
                .ToList();
        }

        public virtual string MethodWithoutParams()
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

        public virtual void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
