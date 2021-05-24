using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Services
{
    public class PropertyMapping<TSource,TDestination> :IPropertyMapping
    {
        public Dictionary<string,IEnumerable<PropertyMappingValue>> _mappingDictionary { get; private set; }
        public PropertyMapping(Dictionary<string,IEnumerable<PropertyMappingValue>> dictionary)
        {
            _mappingDictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        }
    }
}
