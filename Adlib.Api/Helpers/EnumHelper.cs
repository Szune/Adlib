using System;
using System.Collections.Generic;
using System.Linq;

namespace Adlib.Api.Helpers
{
    public static class EnumX
    {
        private static readonly Dictionary<Type, HashSet<string>> UpperCaseEnumValues = new Dictionary<Type, HashSet<string>>();
        /// <summary>
        /// <para>Returns whether parsing succeeded and the parsed value.</para>
        /// <para>Specifically only parses string values, ignoring case.</para>
        /// </summary>
        public static bool TryParse<T>(string s, out T val) where T : struct, Enum
        {
            // Enum.TryParse allows numbers in strings, and numbers and strings
            // Enum.IsDefined is case sensitive
            // Performance of this function should be checked against a combination of Enum.TryParse and Enum.IsDefined (in that order)
            var upper = s.ToUpperInvariant();
            if (UpperCaseEnumValues.TryGetValue(typeof(T), out var values))
            {
                if (values.Contains(upper))
                {
                    val = Enum.Parse<T>(s, true);
                    return true;
                }
            }
            else
            {
                var names = Enum.GetNames(typeof(T)).Select(n => n.ToUpperInvariant()).ToHashSet();
                UpperCaseEnumValues.Add(typeof(T), names);

                if (names.Contains(upper))
                {
                    val = Enum.Parse<T>(s, true);
                    return true;
                }
            }
            val = default;
            return false;
        }
    }
}