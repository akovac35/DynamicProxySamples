using AutoMapper;
using Shared.Blogs;
using Shared.Custom.CustomBlogService;
using System;
using System.Collections.Generic;

namespace Shared.Custom.Helpers
{
    public class AutoMapperHelper
    {
        public static MapperConfiguration MapperConfiguration;

        static AutoMapperHelper()
        {
            MapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Blog, CustomBlogServiceDto>());
        }

        public static object Map<TSource, TDestination>(object source)
        {
            if (source == null)
            {
                return source;
            }

            IMapper mapper = MapperConfiguration.CreateMapper();

            Type sourceType = source.GetType();
            if (sourceType == typeof(TSource))
            {
                return mapper.Map<TDestination>(source);
            }
            else if (
                sourceType == typeof(IEnumerable<TSource>)
                )
            {
                return mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>((IEnumerable<TSource>)source);
            }
            else if (
                sourceType == typeof(TSource[])
                )
            {
                return mapper.Map<TSource[], TDestination[]>((TSource[])source);
            }
            else if (
                sourceType == typeof(IList<TSource>)
                )
            {
                return mapper.Map<IList<TSource>, IList<TDestination>>((IList<TSource>)source);
            }
            else if (
                sourceType == typeof(List<TSource>)
                )
            {
                return mapper.Map<List<TSource>, List<TDestination>>((List<TSource>)source);
            }
            else if (
                sourceType == typeof(ICollection<TSource>)
                )
            {
                return mapper.Map<ICollection<TSource>, ICollection<TDestination>>((ICollection<TSource>)source);
            }
            else
            {
                return source;
            }
        }
    }
}
