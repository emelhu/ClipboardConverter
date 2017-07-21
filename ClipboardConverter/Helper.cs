using System;
using System.ComponentModel;
using System.Reflection;


namespace ClipboardConverter
{
  static public class Helper
  {
    public static string GetEnumDescription(Enum value)
    {
      FieldInfo fi = value.GetType().GetField(value.ToString());

      DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

      if (attributes != null && attributes.Length > 0)
      {
        return attributes[0].Description;
      }
      else
      {
        return value.ToString();
      }
    }

    public static ConversionType CheckTypeCode(char typeChar)
    {
      var types = Enum.GetValues(typeof(ConversionType));

      foreach (ConversionType type in types)
      {
        if (typeChar == (char)((int)'0' + (int)type))
        {
          return type;
        }
      }

      return (ConversionType)0;
    }
  }
}
