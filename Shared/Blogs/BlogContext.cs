﻿using Microsoft.EntityFrameworkCore;

namespace Shared.Blogs
{
    public class BlogContext: DbContext
    {
        public BlogContext()
        { }

        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        { }

        public DbSet<Blog> Blogs { get; set; }
    }
}
