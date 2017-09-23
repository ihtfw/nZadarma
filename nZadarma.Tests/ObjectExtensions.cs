using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nZadarma.Tests
{
    public static class ObjectExtensions
    {
        public static string ToStringDebug(this object obj, int offsetCount = 0)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            string offset = new string('\t', offsetCount);
            var type = obj.GetType();
            var propertyInfos = type.GetProperties();
            var sb = new StringBuilder(offset + type.Name).AppendLine(":");
            sb.AppendLine(offset + "{");
            foreach (var propertyInfo in propertyInfos)
            {
                try
                {
                    var value = propertyInfo.GetValue(obj, null);
                    if (value == null)
                    {
                        sb.AppendFormat(offset + "\t{0} = '{1}'", propertyInfo.Name, null);
                        continue;
                    }

                    var valueEn = value as IEnumerable;
                    if (valueEn != null && !(value is string))
                    {
                        sb.AppendFormat("{0} = '[", propertyInfo.Name);
                        foreach (var item in valueEn)
                        {
                            sb.Append(item.ToStringDebug(offsetCount + 1));
                        }
                        sb.AppendLine(offset + "]'");
                        continue;
                    }
                    
                    sb.AppendFormat("\t{0} = '{1}'", propertyInfo.Name, value);
                }
                catch (Exception e)
                {
                    sb.AppendFormat("\t{0} = '{1}'", propertyInfo.Name, e.Message);
                }
                sb.AppendLine();
            }
            sb.AppendLine(offset + "}");
            return sb.ToString();
        }

    }
}