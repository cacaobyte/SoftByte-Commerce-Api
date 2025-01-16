using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.EL
{
    public static class CommonExtension
    {
        #region IsNull
        public static bool IsNull(this bool? value)
        {
            return value == null;
        }

        public static bool IsNull(this DateTime? value)
        {
            return value == null;
        }

        public static bool IsNull(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) || value.Length == 0;
        }

        public static bool IsNull(this int? value)
        {
            return value == null;
        }

        public static bool IsNull(this decimal? value)
        {
            return value == null;
        }

        public static bool IsNull(this byte? value)
        {
            return value == null;
        }

        public static bool IsNull(this long? value)
        {
            return value == null;
        }

        public static bool IsNull(this List<byte?> value)
        {
            return value == null || value.Count == 0 || value.Count(x => x.IsNull()) > 0;
        }

        public static bool IsNull(this List<long?> value)
        {
            return value == null || value.Count == 0 || value.Count(x => x.IsNull()) > 0;
        }

        public static bool IsNull(this List<long> value)
        {
            return value == null || value.Count == 0;
        }

        public static bool IsNull(this List<string> value)
        {
            return value == null || value.Count == 0;
        }
        #endregion

        public static short? ToShort(this int? value)
        {
            if (value.IsNull())
                return null;

            return (short?)value;
        }

        public static bool IsMail(this string value)
        {
            try
            {
                MailAddress address = new MailAddress(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Corta una cadena dependiendo del limite indicado en el parametro
        /// </summary>
        /// <param name="value">valor del string</param>
        /// <param name="delimiter">cantidad maxima para recortar la cadena</param>
        /// <returns>Código de reporte en short</returns>
        public static string TrimString(string value, int delimiter)
        {
            if (value != null && value.Length > delimiter)
            {
                return value.Substring(0, delimiter);
            }
            return value ?? "";
        }
    }
}
