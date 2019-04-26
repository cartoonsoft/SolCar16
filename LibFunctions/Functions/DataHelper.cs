using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibFunctions.Functions
{
    public static class DataHelper
    {

        /// <summary>
        /// Retorna a data de hoje por extenso
        /// </summary>
        /// <returns>DD de MM de YYYY</returns>
        public static string GetDataPorExtenso()
        {
            var date = DateTime.Now.ToLongDateString().Split(',');

            return date[1].Trim();
        }

    }
}
