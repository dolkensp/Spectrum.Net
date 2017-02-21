using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public static class EnumUtils
    {
        private static Dictionary<Type, Dictionary<String, Object>> _mapTextToEnum = new Dictionary<Type, Dictionary<String, Object>> { };
        private static Dictionary<Type, Dictionary<Object, String>> _mapEnumToText = new Dictionary<Type, Dictionary<Object, String>> { };

        private static void GenerateMap(Type enumType)
        {
            EnumUtils._mapTextToEnum[enumType] = new Dictionary<String, Object> { };
            EnumUtils._mapEnumToText[enumType] = new Dictionary<Object, String> { };

            foreach (var value in Enum.GetValues(enumType))
            {
                EnumUtils._mapEnumToText[enumType][value] = $"{value}";

                var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(EnumUtils._mapEnumToText[enumType][value]).GetCustomAttributes(typeof(EnumMemberAttribute), true)).FirstOrDefault();

                if (enumMemberAttribute != null)
                {
                    EnumUtils._mapTextToEnum[enumType][enumMemberAttribute.Value] = value;
                    EnumUtils._mapEnumToText[enumType][value] = enumMemberAttribute.Value;
                }
            }
        }

        public static String ToString<TEnum>(TEnum value)
        {
            var enumType = typeof(TEnum);

            if (!EnumUtils._mapEnumToText.ContainsKey(enumType)) EnumUtils.GenerateMap(enumType);

            return EnumUtils._mapEnumToText[enumType][value];

        }

        public static TEnum ToEnum<TEnum>(String str, TEnum @default = default(TEnum)) where TEnum : struct
        {
            var enumType = typeof(TEnum);

            if (!EnumUtils._mapTextToEnum.ContainsKey(enumType)) EnumUtils.GenerateMap(enumType);

            if (!EnumUtils._mapTextToEnum[enumType].ContainsKey(str))
            {
                return str.ToEnum<TEnum>(@default);
            }

            return (TEnum)EnumUtils._mapTextToEnum[enumType][str];
        }

        public static TEnum? ToEnum<TEnum>(String str) where TEnum : struct
        {
            var enumType = typeof(TEnum);

            if (!EnumUtils._mapTextToEnum.ContainsKey(enumType)) EnumUtils.GenerateMap(enumType);

            if (!EnumUtils._mapTextToEnum[enumType].ContainsKey(str))
            {
                return str.ToEnum<TEnum>();
            }

            return (TEnum)EnumUtils._mapTextToEnum[enumType][str];
        }
    }
}
