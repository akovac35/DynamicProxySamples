# DynamicProxySamples
A small project to explore dynamic proxy functionality in .NET Core.

### Executing code
Navigate to ../Shared.Tests and execute ```dotnet test```.

### What is it about?
This sample project demonstrates the application of dynamic proxies applied to Adapter object oriented pattern which converts the interface of a class into another interface clients expect. Mature projects such as Castle DynamicProxy have made it possible to generate lightweight dynamic proxies on the fly at runtime.

Scenarios commonly encountered in enterprise applications are generated clients and data transfer objects (DTO) without declared interface or virtual methods, either of which is usually required for Castle dynamic proxy functionality. While there may exist libraries without such restrictions, one should aim to build enterprise applications on mature projects with large user and contributor community. 

To highlight the described scenario, imagine a class for manipulating ```Blog``` table in a database:

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

The ```Blog``` class was autogenerated by the entity framework based on database schema, any changes to it will be lost once the generated code is refreshed to account for database schema changes.

Suppose now that a business scenario requires that the ```Blog``` table contents be exposed by a web service. It would be very convenient to be able to directly annotate the properties of the class directly in the source code as required by the web service code generators, but this is not possible because any changes to the ```Blog``` class will be lost, as explained. Additionally, one might have considerations about such a tight coupling of database and web service code. To overcome the limitations, a ```CustomBlogServiceDto``` class is created based on the original ```Blog``` class but with changes relevant for web service functionality; for this sample, we'll just keep the original class design:

```cs
namespace Shared.Blogs
{
    public class Blog
    {
        public long Id { get; set; }
        public string Url { get; set; }
    }
}
```

```cs
namespace Shared.Custom.CustomBlogService
{
    public class CustomBlogServiceDto
    {
        public long Id { get; set; }
        public string Url { get; set; }
    }
}

```

It would be really convenient for a developer to be able to use the ```CustomBlogServiceDto``` with the ```BlogService``` and in the process apply any transformations, mappings and attribute processing transparently with only minimal changes to the invocation pattern - so from the original invocation pattern:

```cs
BlogService blogService = WindsorHelper.WindsorContainer.Resolve<BlogService>();
...
blogService.Add(searchTerm);
```

To a modified invocation pattern:

```
BlogService blogService = WindsorHelper.WindsorContainer.Resolve<BlogService>();
...
CustomBlogServiceInterceptor customServiceInterceptor = new CustomBlogServiceInterceptor(blogService);
ICustomBlogService customService = CastleHelper.ProxyGenerator.CreateInterfaceProxyWithoutTarget<ICustomBlogService>(customServiceInterceptor);

customService.Add(searchTerm);
```

The ```ICustomBlogService``` is an interface extracted from the ```BlogService``` class and modified slightly so that the types and possibly annotations match specific business requirements, in this case, ```CustomBlogServiceDto``` class should be used instead of ```Blog``` class. The rest should be pretty much automated, and this is the purpose of this sample.
