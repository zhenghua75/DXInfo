using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DXInfo.EFConventions.Reflection
{
    internal class DbSetItemMetadata
    {
        private DbSetItemMetadata() { }

        internal DbSetItemMetadata(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
            var attributes = new List<DbSetItemAttributeMetadata>();
            var propertyAttributes = propertyInfo.GetCustomAttributes(true).ToList();
            propertyAttributes.ForEach(one =>
            {
                attributes.Add(new DbSetItemAttributeMetadata((Attribute)one));
            });
            DbSetItemAttributes = attributes;
        }

        internal PropertyInfo PropertyInfo { get; private set; }

        internal IEnumerable<DbSetItemAttributeMetadata> DbSetItemAttributes { get; private set; }
    }
}
