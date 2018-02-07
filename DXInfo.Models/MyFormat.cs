using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace DXInfo.Models
{
    public class MyFormat:ICustomFormatter, IFormatProvider
    {
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                if (arg is IFormattable)
                    return ((IFormattable)arg).ToString(format, formatProvider);
                return arg.ToString();
            }
            else
            {
                if (format == "DelZero"&& arg is decimal)
                {
                    string strd = arg.ToString();
                    if (strd.IndexOf('.') > 0)
                    {
                        strd = arg.ToString().TrimEnd('0').TrimEnd('.');
                        if (string.IsNullOrEmpty(strd))
                        {
                            strd = "0";
                        }
                    }
                    return strd;
                }
                else if (format == "NameStar" && arg is string)
                {
                    string str = arg.ToString();
                    string str1 = str.Substring(0, 1);
                    int length = str.Length;
                    string str2 = str1.PadRight(length, '*');
                    return str2;
                }
                else
                {
                    if (arg is IFormattable)
                        return ((IFormattable)arg).ToString(format, formatProvider);
                    return arg.ToString();
                }
            }
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            return null;
        }
    }
}
