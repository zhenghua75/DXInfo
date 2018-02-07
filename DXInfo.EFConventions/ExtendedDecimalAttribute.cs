using System;

namespace DXInfo.EFConventions
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
  public class ExtendedDecimalAttribute : Attribute
  {
    public ExtendedDecimalAttribute()
    {

    }

    public ExtendedDecimalAttribute(
        byte precision = 18, byte scale = 4)
    {
      Precision = precision;
      Scale = scale;
    }

    public byte Precision { get; private set; }
    public byte Scale { get; private set; }
  }
}
