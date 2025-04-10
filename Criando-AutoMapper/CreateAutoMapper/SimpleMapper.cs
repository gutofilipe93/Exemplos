using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateAutoMapper
{
    public class SimpleMapper
    {
        private readonly Dictionary<string, Dictionary<string, PropertyMapping>> _mappings = new();

        public class PropertyMapping
        {
            public string SourceProperty { get; set; }
            public string DestinationProperty { get; set; }
        }

        public class MappingConfig
        {
            private readonly List<PropertyMapping> _propertyMappings = new();
            public string SourceType { get; }
            public string DestType { get; }

            public MappingConfig(string sourceType, string destType)
            {
                SourceType = sourceType;
                DestType = destType;
            }

            public void MapProperty(string sourceProperty, string destProperty)
            {
                _propertyMappings.Add(new PropertyMapping
                {
                    SourceProperty = sourceProperty,
                    DestinationProperty = destProperty
                });
            }

            public List<PropertyMapping> GetMappings() => _propertyMappings;
        }

        public void CreateMap<TSource, TDestination>(Action<MappingConfig> configAction)
        {
            var sourceType = typeof(TSource).FullName;
            var destType = typeof(TDestination).FullName;

            var key = $"{sourceType}_{destType}";

            if (!_mappings.ContainsKey(key))
            {
                _mappings[key] = new Dictionary<string, PropertyMapping>();
            }

            var config = new MappingConfig(sourceType, destType);
            configAction(config);

            foreach (var mapping in config.GetMappings())
            {
                _mappings[key][mapping.DestinationProperty] = mapping;
            }
        }

        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            if (source == null)
                return default;

            var sourceType = typeof(TSource).FullName;
            var destType = typeof(TDestination).FullName;
            var key = $"{sourceType}_{destType}";

            if (!_mappings.ContainsKey(key))
            {
                throw new InvalidOperationException($"No mapping defined from {sourceType} to {destType}");
            }

            var destination = new TDestination();
            var sourceProps = typeof(TSource).GetProperties();
            var destProps = typeof(TDestination).GetProperties();

            // First, try to map properties with the same name
            foreach (var destProp in destProps)
            {
                var sourceProp = Array.Find(sourceProps, p => p.Name == destProp.Name);
                if (sourceProp != null && sourceProp.PropertyType == destProp.PropertyType)
                {
                    var value = sourceProp.GetValue(source);
                    destProp.SetValue(destination, value);
                }
            }

            // Then apply custom mappings
            foreach (var mapping in _mappings[key].Values)
            {
                var sourceProp = typeof(TSource).GetProperty(mapping.SourceProperty);
                var destProp = typeof(TDestination).GetProperty(mapping.DestinationProperty);

                if (sourceProp != null && destProp != null)
                {
                    var value = sourceProp.GetValue(source);
                    destProp.SetValue(destination, value);
                }
            }

            return destination;
        }
    }
}