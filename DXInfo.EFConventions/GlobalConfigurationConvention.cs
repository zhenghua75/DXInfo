using System;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Reflection;

namespace DXInfo.EFConventions
{
    public abstract class GlobalConfigurationConvention<TMemberInfo, TPropertyConfiguration>
        : IGlobalConvention
        where TMemberInfo : MemberInfo
        where TPropertyConfiguration : PrimitivePropertyConfiguration
    {

        public void ApplyConfiguration(
            MemberInfo memberInfo, 
            PrimitivePropertyConfiguration propertyConfiguration)
        {
            Apply((TMemberInfo)memberInfo, (TPropertyConfiguration)propertyConfiguration);
        }

        protected abstract void Apply(
            TMemberInfo memberInfo, 
            TPropertyConfiguration propertyConfiguration);


        public Type PropertyConfigurationType
        {
            get { return typeof(TPropertyConfiguration); }
        }

    }


  
}
