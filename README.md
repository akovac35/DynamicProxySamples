# DynamicProxySamples
A small project to explore dynamic proxy functionality in .NET Core.

### Executing code
Navigate to ../Shared.Tests and execute ```dotnet test```.

### What is it about?
This sample project demonstrates the application of dynamic proxies applied to Adapter object oriented pattern, which converts the interface of a class into another interface clients expect. Mature projects such as Castle DynamicProxy have made it possible to generate lightweight dynamic proxies on the fly at runtime.

Scenarios commonly encountered in enterprise applications are generated clients and data transfer objects (DTO) without declared interface or virtual methods, either of which is usually required for Castle dynamic proxy functionality. While there may exist libraries without such restrictions, one should aim to build enterprise applications on mature projects with large user and contributor community. 

To highlight the described scenario, imagine a class for manipulating ```Blog``` objects in a database:
```cs
using System.Collections.Generic;
using System.Linq;

namespace Shared.Blogs
{
    public class BlogService
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

```
