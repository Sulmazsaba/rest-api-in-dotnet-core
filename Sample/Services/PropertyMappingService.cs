using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Services
{
    public class PropertyMappingService<TSource,TDestination> :IPropertyMappingService
    {

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource1, TDestination1>()
        {
            throw new NotImplementedException();
        }
    }
}
