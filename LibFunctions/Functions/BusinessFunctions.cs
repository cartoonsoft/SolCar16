using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LibFunctions.Functions
{
    public static class BusinessFunctions
    {
        public static bool ValidarCPF(string cpf)
        {
            if (cpf.Length != 11)
                return false;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(
                    cpf[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }

        public static bool ValidarCNPJ(string cnpj)
        {
            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;
            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(cnpj.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig + 1, 1)));

                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);

                    if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                        CNPJOk[nrDig] = (digitos[12 + nrDig] == 0);
                    else
                        CNPJOk[nrDig] = (digitos[12 + nrDig] == (11 - resultado[nrDig]));
                }

                return (CNPJOk[0] && CNPJOk[1]);
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidaEmail(string email)
        {
            try
            {
                if (email != null && email.Contains(" "))
                    return false;

                MailAddress m = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidarCelular(string numero)
        {
            if (string.IsNullOrEmpty(numero))
                return false;

            if (numero.Length < 14 || numero.Length > 15)
                return false;

            var caracter = numero.Substring(5, 1);
            if ((caracter == "9") ||
                (caracter == "8") ||
                (caracter == "7"))
                return true;
            else
                return false;
        }


        public static IEnumerable<T> ObtenhaAlgunsItensEmOrdemAleatoria<T>(this IEnumerable<T> lista, int maxItens)
        {
            Random random = new Random(Environment.TickCount);
            Dictionary<double, T> randomSortTable = new Dictionary<double, T>();
            randomSortTable = lista.ToDictionary(x => random.NextDouble(), y => y);

            return randomSortTable.OrderBy(KVP => KVP.Key).Take(maxItens).Select(KVP => KVP.Value);
        }

        public static string GerarProtocolo(DateTime data)
        {
            int tamanho = 4;
            DateTime dateTimeNow = data;
            var ticks = dateTimeNow.Ticks.ToString();
            int startIndex = ticks.Length - tamanho;
            var lastTicks = ticks.Substring(startIndex, tamanho);

            //formato 1506054621 ano[2]-mes[2]-dia[2]-lastTicks[tamanho]
            var result = string.Format("{0:yy}{1:00}{2:00}{3}", dateTimeNow, dateTimeNow.Month, dateTimeNow.Day, lastTicks);
            return result;
        }

        public static int CalcularIdade(DateTime dataNascimento, DateTime database)
        {
            if (database.Date < dataNascimento.Date)
            {
                throw new Exception("Data de Nascimento deve ser menor ou igual a database.");

            }
            int years = database.Year - dataNascimento.Year;

            if ((dataNascimento.Month > database.Month) || (dataNascimento.Month == database.Month && dataNascimento.Day > database.Day))
                years--;

            return years;
        }

        public static bool LatitudeValida(string latitude)
        {
            if (string.IsNullOrWhiteSpace(latitude))
                return false;

            latitude = latitude.Replace(".", ",");
            var lati = Double.Parse(latitude);

            string number = lati.ToString().Replace(',', '.');
            if (number.Contains("."))
            {
                int length = number.Substring(number.IndexOf(".")).Length - 1;
                if (length > 6)
                {
                    return false;
                }
            }

            try
            {
                //new System.Device.Location.GeoCoordinate(lati, 1);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool LongitudeValida(string longitude)
        {
            if (string.IsNullOrWhiteSpace(longitude))
                return false;

            longitude = longitude.Replace(".", ",");
            var longi = Double.Parse(longitude);

            string number = longi.ToString().Replace(',', '.');
            if (number.Contains("."))
            {
                int length = number.Substring(number.IndexOf(".")).Length - 1;
                if (length > 6)
                {
                    return false;
                }
            }

            try
            {
                //new System.Device.Location.GeoCoordinate(1, longi);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
