using System.Data.Entity.ModelConfiguration.Configuration;
using System.Reflection;
using DXInfo.EFConventions;

namespace DXInfo.EFConventions
{
  public class ExtendedDecimalConvention : 
        AttributeConfigurationConvention<MemberInfo, DecimalPropertyConfiguration, ExtendedDecimalAttribute>
    {

        protected override void Apply(
            MemberInfo memberInfo,
            DecimalPropertyConfiguration propertyConfiguration,
            ExtendedDecimalAttribute attrribute)
        {
          propertyConfiguration.HasPrecision(attrribute.Precision, attrribute.Scale);
        }
    }
}
