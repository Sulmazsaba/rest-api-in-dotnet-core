using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Services
{
   public interface IPropertyMappingService
   {
       Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource,TDestination>();
   }
}
