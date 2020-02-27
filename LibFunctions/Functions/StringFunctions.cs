/*
---------1---------2---------3---------4---------5---------6---------7---------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Funções tratamento de strings
by Ronaldo Moreira - ronaldo.poa.rs@gmail.com
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace LibFunctions.Functions.StringsFunc
{
    public static class StringFunctions
    {
        public static string HtmlNewLine(this string value)
        {
            return value.Replace(Environment.NewLine, "<br/>");
        }

        public static string Capitalize(string value)
        {
            if (value == null)
                return string.Empty;

            if (value.Length == 0)
                return value;

            value = value.ToLower();

            StringBuilder result = new StringBuilder(value);
            result[0] = char.ToUpper(result[0]);
            for (int i = 1; i < result.Length; ++i)
            {
                if (char.IsWhiteSpace(result[i - 1]))
                    result[i] = char.ToUpper(result[i]);
            }

            result = result.Replace(" Da ", " da ");
            result = result.Replace(" Das ", " das ");
            result = result.Replace(" De ", " de ");
            result = result.Replace(" De, ", " de, ");
            result = result.Replace(" Do ", " do ");
            result = result.Replace(" Dos ", " dos ");
            result = result.Replace(" E ", " e ");
            result = result.Replace(" Ao ", " ao ");

            return result.ToString();
        }

        public static string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            var me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return me.Member.Name;
        }

        public static string GetPropertyName<T, KProperty>(Expression<Func<T, KProperty>> propertyLambda)
        {
            var me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return me.Member.Name;
        }

        public static string Upper(string value)
        {
            if (!string.IsNullOrEmpty(value))
                return value.ToUpper();
            else
                return null;
        }

        public static string Lower(string value)
        {
            if (!string.IsNullOrEmpty(value))
                return value.ToLower();
            else
                return null;
        }

        public static string AmericaName(string fullName)
        {
            return FirstName(fullName, false) + ", " + FirstName(fullName, true, true, false);
        }

        public static string FirstName(string fullName, bool firstName = true, bool middleName = false, bool lastName = true)
        {
            if (fullName == null)
                fullName = string.Empty;

            fullName = fullName.Trim();

            string[] separacao = fullName.Split(' ');
            string resultado = "";
            int i = firstName ? 0 : 1;
            int cont = lastName ? 1 : 2;
            bool meio = middleName;

            while (i <= separacao.Count() - cont)
            {
                if (!meio)
                {
                    if ((i == 0) || (i == separacao.Count() - 1))
                        resultado += " " + separacao[i];
                }
                else
                    resultado += " " + separacao[i];

                i++;
            }

            return resultado.Trim();
        }

        public static string EncodeUtf8(string value)
        {
            byte[] bytes = Encoding.Default.GetBytes(value);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string TrimNull(this string text)
        {
            if (text != null)
            {
                return text.Trim();
            }

            return null;
        }

        public static string ZerosEsquerda(string strString, int intTamanho)
        {
            string strResult = "";
            for (int intCont = 1; intCont <= (intTamanho - strString.Length); intCont++)
            {
                strResult += "0";
            }

            return strResult + strString;
        }

        public static IEnumerable<string[]> AdicionePrefixo(IEnumerable<string[]> lista, string prefixo)
        {
            foreach (var item in lista)
            {
                item[1] = prefixo + item[1];
            }

            return lista;
        }

        public static T ConvertaParaEnum<T>(int? valor)
        {
            if (valor.HasValue)
            {
                T result = (T)Enum.ToObject(typeof(T), valor.Value);
                return result;
            }

            return default(T);
        }

        public static string GetEnumDescription<TEnum>(this TEnum value)
        {
            if (value == null)
                return null;

            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = null;

            if (fi != null)
            {
                attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            }

            if ((attributes != null) && (attributes.Length > 0))
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string GetEnumDescription(string value, Type enumType)
        {
            if (value == null)
                return null;

            FieldInfo fi = enumType.GetField(value.ToString());
            DescriptionAttribute[] attributes = null;

            if (fi != null)
            {
                attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            }

            if ((attributes != null) && (attributes.Length > 0))
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static T GetAttributeFrom<T>(this object instance) where T : Attribute
        {
            return GetAttributeFrom<T>(instance.GetType());
        }

        public static T GetAttributeFrom<T>(this Type instance) where T : Attribute
        {
            return (T)Attribute.GetCustomAttribute(instance, typeof(T));
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static byte[] GetBytes(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static string RemoveAcentos(string strcomAcentos)
        {
            string strsemAcentos = strcomAcentos;
            if (!String.IsNullOrEmpty(strsemAcentos))
            {
                strsemAcentos = Regex.Replace(strsemAcentos, "[áàâãª]", "a");
                strsemAcentos = Regex.Replace(strsemAcentos, "[ÁÀÂÃ]", "A");
                strsemAcentos = Regex.Replace(strsemAcentos, "[éèêë]", "e");
                strsemAcentos = Regex.Replace(strsemAcentos, "[ÉÈÊË]", "E");
                strsemAcentos = Regex.Replace(strsemAcentos, "[íìî]", "i");
                strsemAcentos = Regex.Replace(strsemAcentos, "[ÍÌÎ]", "I");
                strsemAcentos = Regex.Replace(strsemAcentos, "[óòôõº]", "o");
                strsemAcentos = Regex.Replace(strsemAcentos, "[ÓÒÔÕ]", "O");
                strsemAcentos = Regex.Replace(strsemAcentos, "[úùû]", "u");
                strsemAcentos = Regex.Replace(strsemAcentos, "[ÚÙÛ]", "U");
                strsemAcentos = Regex.Replace(strsemAcentos, "[ç]", "c");
                strsemAcentos = Regex.Replace(strsemAcentos, "[Ç]", "C");
            }

            return strsemAcentos;
        }

        public static string RemoveAcentos2(this string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }

        public static string SomenteNumeros(string valor)
        {
            string valorTmp = new String(valor.Where(Char.IsDigit).ToArray());

            return valorTmp;
        }

        public static string SomenteNumeros2(string valor)
        {
            if (valor == null) return "";
            return Regex.Replace(valor, "[^0-9]", "");
        }

    }

}
