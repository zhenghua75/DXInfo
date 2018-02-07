using System;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Reflection;

namespace DXInfo.EFConventions
{
    public abstract class AttributeConfigurationConvention<TMemberInfo, TPropertyConfiguration, TAttribute>
        : IAttributeConvention
        where TMemberInfo : MemberInfo
        where TPropertyConfiguration : PrimitivePropertyConfiguration
        where TAttribute : Attribute
    {

        public void ApplyConfiguration(
            MemberInfo memberInfo, 
            PrimitivePropertyConfiguration propertyConfiguration, 
            Attribute attribute)
        {
            Apply((TMemberInfo)memberInfo, (TPropertyConfiguration)propertyConfiguration, (TAttribute)attribute);
        }

        protected abstract void Apply(
            TMemberInfo memberInfo, 
            TPropertyConfiguration propertyConfiguration, 
            TAttribute attrribute);


        public Type PropertyConfigurationType
        {
            get { return typeof(TPropertyConfiguration); }
        }

        public Type AttributeType
        {
            get { return typeof(TAttribute); }
        }

    }


  
}
