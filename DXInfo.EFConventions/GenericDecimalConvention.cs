using System;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Reflection;
using DXInfo.EFConventions;

namespace DXInfo.EFConventions
{
  public class GenericDecimalConvention :
    GlobalConfigurationConvention<MemberInfo, DecimalPropertyConfiguration>
  {
    protected override void Apply(
      MemberInfo memberInfo,
      DecimalPropertyConfiguration propertyConfiguration)
    {
      propertyConfiguration.HasPrecision(9, 5);
    }
  }
}
