using AutoMapper;
using System;
using System.Collections.Generic;

namespace Shared.Custom.Helper
{
    public class MapperHelper
    {
        public static object Map<TSource, TDestination>(object source, IMapper mapper)
        {
            if (source == null)
            {
                return source;
            }

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
