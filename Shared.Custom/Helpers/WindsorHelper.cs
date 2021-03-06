﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Shared.Blogs;

namespace Shared.Custom.Helpers
{
	public class WindsorHelper: IWindsorInstaller
    {
        public static WindsorContainer WindsorContainer = new WindsorContainer();

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For<BlogService>().LifestyleTransient());
		}
	}
}
