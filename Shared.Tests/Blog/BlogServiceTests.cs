using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shared.Blogs;
using Shared.Custom.Helpers;
using System.Linq;

namespace Shared.Tests.Blog
{
    [TestFixture]
    public class BlogServiceTests
    { 
        [Test]
        public void BlogInsertTest()
        {
            IBlogService blogService = WindsorHelper.WindsorContainer.Resolve<IBlogService>();
            string searchTerm = "https://example.com";

            try
            {
                BlogContext context = blogService.Context;

                // In-memory database exists only for the duration of an open connection
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                blogService.Add(searchTerm);

                // Will automatically use transaction
                context.SaveChanges();

                var searchResult = blogService.Find(searchTerm);
                Assert.AreEqual(1, context.Blogs.Count());
                Assert.AreEqual(searchTerm, searchResult.First().Url);
            }
            finally
            {
                WindsorHelper.WindsorContainer.Release(blogService);
            }
        }
    }
}