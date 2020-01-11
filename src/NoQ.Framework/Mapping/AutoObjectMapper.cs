using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NoQ.Framework.Extensions;

namespace NoQ.Framework.Mapping
{
    public abstract class AutoObjectMapper<TSource, TDestination> : IObjectMapper<TSource, TDestination>
    {
        protected readonly IMapper _mapper;

        public AutoObjectMapper()
        {
            IConfigurationProvider config = ConfigureMap();
            config.AssertConfigurationIsValid();
            _mapper = config.CreateMapper();
        }

        public TDestination Map(TSource source) => _mapper.Map<TDestination>(source);

        public TDestination[] Map(IEnumerable<TSource> source)
        {
            source.VerifyNotNull(nameof(source));
            return source.Select(Map)
                         .ToArray();
        }

        protected virtual IConfigurationProvider ConfigureMap() => new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
    }
}
