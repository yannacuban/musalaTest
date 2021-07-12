using System;
using System.Collections.Generic;
using System.Text;

namespace GateWays.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Checks whatever given collection object is null or has no item.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// Gets a substring of a string from beginning of the string.
        /// </summary>
        /// <exception cref="T:System.ArgumentNullException">Thrown if <paramref name="str" /> is null</exception>
        /// <exception cref="T:System.ArgumentException">Thrown if <paramref name="len" /> is bigger that string's length</exception>
        public static string Left(this string str, int len)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (str.Length < len)
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            return str.Substring(0, len);
        }

        /// <summary>
        /// Removes first occurrence of the given postfixes from end of the given string.
        /// Ordering is important. If one of the postFixes is matched, others will not be tested.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="postFixes">one or more postfix.</param>
        /// <returns>Modified string or the same string if it has not any of given postfixes</returns>
        public static string RemovePostFix(this string str, params string[] postFixes)
        {
            if (str == null)
                return (string)null;
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            if (((ICollection<string>)postFixes).IsNullOrEmpty<string>())
                return str;
            foreach (string postFix in postFixes)
            {
                if (str.EndsWith(postFix))
                    return str.Left(str.Length - postFix.Length);
            }
            return str;
        }
    }
}
