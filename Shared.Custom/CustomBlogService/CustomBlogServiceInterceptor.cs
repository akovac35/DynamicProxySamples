using AutoMapper;
using Castle.DynamicProxy;
using Shared.Blogs;
using Shared.Custom.Helper;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Shared.Custom.CustomBlogService
{
    public class CustomBlogServiceInterceptor : IInterceptor
    {
        protected IBlogService _blogService;
        protected static MapperConfiguration _mapperConfiguration;
        protected IMapper _mapper;

        static CustomBlogServiceInterceptor()
        {
            _mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Blog, CustomBlogServiceDto>());
        }

        public CustomBlogServiceInterceptor(IBlogService blogService)
        {
            _blogService = blogService;
            _mapper = _mapperConfiguration.CreateMapper();
        }

        public void Intercept(IInvocation invocation)
        {
            List<Type> argumentTypes = new List<Type>();
            foreach (ParameterInfo parameter in invocation.Method.GetParameters())
            {
                argumentTypes.Add(parameter.ParameterType);
            }

            Type type = typeof(IBlogService);
            MethodInfo methodInfo = type.GetMethod(invocation.Method.Name, argumentTypes.ToArray());

            Object result = methodInfo.Invoke(_blogService, invocation.Arguments);
            invocation.ReturnValue = MapperHelper.Map<Blog, CustomBlogServiceDto>(result, _mapper);
        }
    }
}
