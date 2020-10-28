using System;
using System.Collections.Generic;

namespace Sample.Services
{
    public class PropertyMappingValue
    {
        public bool Revert { get; private set; }
        public IEnumerable<string> DestinationProperties { get; private set; }

        public PropertyMappingValue(IEnumerable<string> destinationProperties, bool revert=false)
        {
            DestinationProperties = destinationProperties?? 
                throw new ArgumentNullException(nameof(DestinationProperties));
            Revert = revert;
        }
    }
}