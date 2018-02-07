using System.Data.Entity.ModelConfiguration.Configuration;
using System.Reflection;
using DXInfo.EFConventions;

namespace DXInfo.EFConventions
{
  public class PercentConvention :
    GlobalConfigurationConvention<MemberInfo, DecimalPropertyConfiguration>
  {
    protected override void Apply(
      MemberInfo memberInfo,
      DecimalPropertyConfiguration propertyConfiguration)
    {
      if (memberInfo.Name.ToUpper().Contains("PERCENT"))
      {
        propertyConfiguration.HasPrecision(4, 2);
      }
    }
  }
}
