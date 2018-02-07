using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXInfo.EFConventions.Reflection
{
    internal class DbSetItemAttributeMetadata
    {
        private DbSetItemAttributeMetadata() { }

        internal DbSetItemAttributeMetadata(Attribute attribute)
        {
            Attribute = attribute;
        }
        internal Attribute Attribute { get; private set; }
    }
}
