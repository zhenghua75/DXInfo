using System;

namespace DXInfo.EFConventions
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ExtendedStringAttribute : Attribute
    {
        public ExtendedStringAttribute()
            : this(isUnicode: true)
        {

        }

        public ExtendedStringAttribute(
            int minLength = 0, int maxLength = int.MaxValue, bool isUnicode = true)
        {
            MinLength = minLength;
            MaxLength = maxLength;
            IsUnicode = isUnicode;
        }

        public int MinLength { get; private set; }
        public int MaxLength { get; private set; }
        public bool IsUnicode { get; private set; }
    }
}
