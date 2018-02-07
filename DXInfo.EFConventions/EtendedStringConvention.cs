using System.Data.Entity.ModelConfiguration.Configuration;
using System.Reflection;
using DXInfo.EFConventions;

namespace DXInfo.EFConventions
{
    public class EtendedStringConvention : 
        AttributeConfigurationConvention<MemberInfo, StringPropertyConfiguration, ExtendedStringAttribute>
    {

        protected override void Apply(
            MemberInfo memberInfo, 
            StringPropertyConfiguration propertyConfiguration, 
            ExtendedStringAttribute attrribute)
        {
            propertyConfiguration.IsUnicode(attrribute.IsUnicode);
            if (attrribute.MaxLength == int.MaxValue || attrribute.MaxLength == -1)
            {
                propertyConfiguration.IsMaxLength();
            }
            else if (attrribute.MaxLength == attrribute.MinLength && attrribute.MinLength > 0)
            {
                propertyConfiguration.IsMaxLength();
                propertyConfiguration.IsFixedLength();
                propertyConfiguration.HasMaxLength(attrribute.MaxLength);
            }
            else
            {
                propertyConfiguration.HasMaxLength(attrribute.MaxLength);
            }
        }
    }
}
