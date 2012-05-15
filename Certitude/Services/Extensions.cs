using System;
using System.Text;

namespace Certitude.Services
{
    public static class Extensions
    {
        private static readonly UTF8Encoding Encoding = new UTF8Encoding();

        /// <summary>
        /// Converts byte arrays to strings
        /// </summary>
        /// <param name="array">string represented as a byte array</param>
        /// <returns>string representation of byte array</returns>
        public static string AsString(this byte[] array)
        {
            // if we don't have a response, fail
            if (array == null || array.Length == 0)
            {
                return string.Empty;
            }
            return Encoding.GetString(array.Trim());
        }

        // lifted from : http://stackoverflow.com/questions/240258/removing-trailing-nulls-from-byte-array-in-c-sharp
        public static byte[] Trim(this byte[] array)
        {
            int i = array.Length - 1;
            while (array[i] == 0)
                --i;

            byte[] result = new byte[i + 1];
            Array.Copy(array, result, i + 1);

            return result;
        }
    }
}