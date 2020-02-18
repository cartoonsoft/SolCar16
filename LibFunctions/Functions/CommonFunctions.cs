using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace LibFunctions.Functions.CommonFunc
{
    public static class CommonFunctions
    {
        public static long StrtoLong(string valor)
        {
            long valorTmp = 0;
            long.TryParse(Regex.Match(valor, @"\d+").Value, out valorTmp);

            return valorTmp;
        }

        public static string SomenteNumeros(string valor)
        {
            string valorTmp = new String(valor.Where(Char.IsDigit).ToArray());

            return valorTmp;
        }

    }

}
