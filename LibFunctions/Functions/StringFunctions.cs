using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

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

        public static string OnlyNumbers(string strNumbers)
        {
            if (strNumbers == null)
                return null;

            List<char> numbers = new List<char>("0123456789");
            StringBuilder toReturn = new StringBuilder(strNumbers.Length);
            CharEnumerator enumerator = strNumbers.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (numbers.Contains(enumerator.Current))
                    toReturn.Append(enumerator.Current);
            }

            return toReturn.ToString();
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

        public static DateTime ConvertaParaDateTime(DateTime data, TimeSpan hora)
        {
            return new DateTime(data.Year, data.Month, data.Day, hora.Hours, hora.Minutes, hora.Seconds);
        }

        public static DateTime ConvertaParaDateTime(DateTime data)
        {
            return new DateTime(data.Year, data.Month, data.Day);
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

        public static string RemoveAcentos(this string text)
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

        public static string FormatarCpfCnpj(string strCpfCnpj)
        {
            if (strCpfCnpj.Length <= 11)
            {
                MaskedTextProvider mtpCpf = new MaskedTextProvider(@"000\.000\.000-00");
                mtpCpf.Set(ZerosEsquerda(strCpfCnpj, 11));
                return mtpCpf.ToString();
            }
            else
            {
                MaskedTextProvider mtpCnpj = new MaskedTextProvider(@"00\.000\.000/0000-00");
                mtpCnpj.Set(ZerosEsquerda(strCpfCnpj, 11));
                return mtpCnpj.ToString();
            }
        }

        public static bool TryParseDateTime(string text, out Nullable<DateTime> nDate)
        {
            DateTime date;
            bool isParsed = System.DateTime.TryParse(text, out date);
            if (isParsed)
                nDate = new Nullable<DateTime>(date);
            else
                nDate = new Nullable<DateTime>();
            return isParsed;
        }

        public static IEnumerable<string[]> AdicionePrefixo(IEnumerable<string[]> lista, string prefixo)
        {
            foreach (var item in lista)
            {
                item[1] = prefixo + item[1];
            }

            return lista;
        }

        /*
        public static IEnumerable<string[]> ValideLista<T>(this ICollection<T> lista, string prefixo) where T : class, IValidator
        {
            var validationResults = new List<string[]>();
            if (lista == null) return validationResults;

            var t = typeof(T);
            int i = 0;

            foreach (var item in lista)
            {
                validationResults.AddRange(AdicionePrefixo(item.Validate(), string.Format("{0}[{1}].", prefixo, i)));
                i++;
            }

            return validationResults;
        }
        */

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

        public static string GetDiaDaSemana(DateTime? data)
        {
            if (data.HasValue)
            {
                return Capitalize(new CultureInfo("pt-BR").DateTimeFormat.DayNames[(int)data.Value.DayOfWeek]);
            }

            return string.Empty;
        }

        public static string TraduzirDayOfWeek(DayOfWeek? day)
        {
            if (day.HasValue)
            {
                return Capitalize(new CultureInfo("pt-BR").DateTimeFormat.DayNames[(int)day.Value]);
            }

            return string.Empty;
        }

        public static decimal CalculaPorcentagem(decimal total, decimal valor)
        {
            if (total == 0)
                return 0;

            return valor / total * 100;
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

    }
}
