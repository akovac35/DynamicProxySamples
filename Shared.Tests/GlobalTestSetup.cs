﻿using Castle.MicroKernel.Registration;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shared.Blogs;
using Shared.Custom.Helpers;

[SetUpFixture]
public class GlobalTestSetup
{
    [OneTimeSetUp]
    public void Setup()
    {
        ContainerHelper.WindsorContainer.Install(new ContainerHelper());
        ContainerHelper.WindsorContainer.Register(Component.For<SqliteConnection>().DependsOn(new { connectionString = "DataSource=:memory:" }).LifestyleTransient());
        ContainerHelper.WindsorContainer.Register(Component.For<BlogContext>().UsingFactoryMethod(kernel =>
        {
            var connection = kernel.Resolve<SqliteConnection>();
            var options = new DbContextOptionsBuilder<BlogContext>()
            .UseSqlite(connection)
            .Options;
            var context = new BlogContext(options);
            return context;

        }, managedExternally: true).LifestyleTransient());
    }
}
