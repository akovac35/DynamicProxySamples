using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shared.Blogs;
using Shared.Custom.CustomBlogService;
using Shared.Custom.Helpers;
using System.Linq;

namespace Shared.Tests.CustomService
{
    [TestFixture]
    public class CustomServiceTests
    {
        [Test]
        public void CustomServiceTest()
        {
            IBlogService blogService = WindsorHelper.WindsorContainer.Resolve<IBlogService>();
            string searchTerm = "https://example.com";

            try
            {
                BlogContext context = blogService.Context;

                // In-memory database exists only for the duration of an open connection
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                CustomBlogServiceInterceptor customServiceInterceptor = new CustomBlogServiceInterceptor(blogService);
                ICustomBlogService customService = CastleHelper.ProxyGenerator.CreateInterfaceProxyWithoutTarget<ICustomBlogService>(customServiceInterceptor);

                customService.MethodWithoutParams();
                customService.Add(searchTerm);

                // Will automatically use transaction
                context.SaveChanges();

                var searchResult = customService.Find(searchTerm);
                Assert.AreEqual(1, searchResult.Count());
                Assert.AreEqual(searchTerm, searchResult.First().Url);
            }
            finally
            {
                WindsorHelper.WindsorContainer.Release(blogService);
            }
        }
    }
}
