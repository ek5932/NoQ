using System.Collections.Generic;

namespace NoQ.Framework.Mapping
{
    public interface IObjectMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);
        TDestination[] Map(IEnumerable<TSource> source);
    }
}
