using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DXInfo.EFConventions.Reflection
{
    internal class DbSetMetadata
    {
        private DbSetMetadata() { }
        internal DbSetMetadata(Type itemType, PropertyInfo propertyInfo)
        {
            ItemType = itemType;
            PropertyInfo = propertyInfo;
            var itemProperties = new List<DbSetItemMetadata>();

            var itemTypeProperties =
                itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).ToList();
            itemTypeProperties.ForEach(one =>
            {
                itemProperties.Add(new DbSetItemMetadata(one));
            });

            DbSetItemProperties = itemProperties;
        }

        internal Type ItemType { get; private set; }
        internal PropertyInfo PropertyInfo { get; private set; }
        internal IEnumerable<DbSetItemMetadata> DbSetItemProperties { get; private set; }
    }
}
