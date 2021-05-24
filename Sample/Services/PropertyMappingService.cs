using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Services
{
    public class PropertyMappingService :IPropertyMappingService
    {
        private Dictionary<string,PropertyMappingValue> companyPropertyValues=
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                {"Name",new PropertyMappingValue(new List<string>(){"Name"})},
                {"Id",new PropertyMappingValue(new List<string>{"Id"})},
                {"NumberOfStaff",new PropertyMappingValue(new List<string> {"NumberOfStaff"})},
                {"CompanyAge",new PropertyMappingValue(new List<string>{"DateTime"})}
            };
        private IList<IPropertyMapping> propertyMappings=new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            propertyMappings.Add(new PropertyMapping<>());
        }
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            return companyPropertyValues;
        }
    }
}
