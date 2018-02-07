using System;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;

namespace DXInfo.EFConventions
{
    public interface IAttributeConvention : IConvention
    {
        void ApplyConfiguration(
            MemberInfo memberInfo, 
            PrimitivePropertyConfiguration propertyConfiguration, 
            Attribute attrribute);

        Type PropertyConfigurationType { get; }
        Type AttributeType { get; }
    }
}
