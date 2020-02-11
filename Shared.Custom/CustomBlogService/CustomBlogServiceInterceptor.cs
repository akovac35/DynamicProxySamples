using AutoMapper;
using Castle.DynamicProxy;
using Shared.Blogs;
using Shared.Custom.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Shared.Custom.CustomBlogService
{
    public class CustomBlogServiceInterceptor : IInterceptor
    {
        protected BlogService _blogService;

        public CustomBlogServiceInterceptor(BlogService blogService)
        {
            _blogService = blogService;
        }

        public void Intercept(IInvocation invocation)
        {
            List<Type> argumentTypes = new List<Type>();
            foreach (ParameterInfo parameter in invocation.Method.GetParameters())
            {
                argumentTypes.Add(parameter.ParameterType);
            }

            Type type = typeof(BlogService);
            MethodInfo methodInfo = type.GetMethod(invocation.Method.Name, argumentTypes.ToArray());

            Object result = methodInfo.Invoke(_blogService, invocation.Arguments);
            invocation.ReturnValue = AutoMapperHelper.Map<Blog, CustomBlogServiceDto>(result);
        }
    }
}
