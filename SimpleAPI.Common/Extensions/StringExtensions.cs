using System;

namespace SimpleAPI.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// DTO to Entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string MappedFromEnum<T>(this string val) where T : struct, Enum
        {
            if (Enum.TryParse<T>(val, out T res))
                return res.ToString();
            return val;
        }

        /// <summary>
        /// ENtity to Dto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static T MappedToEnum<T>(this string val) where T : struct, Enum
        {
            if (Enum.TryParse<T>(val, out T res))
                return res;
            return default(T);
        }

        /// <summary>
        /// Retrieve the Id for the enum
        /// </summary>
        /// <param name="myEnum"></param>
        /// <returns></returns>
        public static string GetId(this Enum myEnum)
        {
            return Convert.ToInt32(myEnum).ToString();
        }
    }
}