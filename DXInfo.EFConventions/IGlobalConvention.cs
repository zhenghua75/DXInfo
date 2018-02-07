using System;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;

namespace DXInfo.EFConventions
{
    public interface IGlobalConvention : IConvention
    {
        void ApplyConfiguration(
            MemberInfo memberInfo, 
            PrimitivePropertyConfiguration propertyConfiguration);

        Type PropertyConfigurationType { get; }
    }
}
