/*
---------1---------2---------3---------4---------5---------6---------7---------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Funções que auxiliam validações, formatações, etc. Para regras de negócio
by Ronaldo Moreira - ronaldo.poa.rs@gmail.com
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using LibFunctions.Functions.StringsFunc;

namespace LibFunctions.Functions.BusinessFuncs
{
    public static class BusinessFunctions
    {
        public static bool ValidarCPF(string cpf)
        {
            cpf = StringFunctions.SomenteNumeros(cpf);

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
            cnpj = StringFunctions.SomenteNumeros(cnpj);

            if (cnpj.Length != 14)
            {
                return false;
            }

            string[] cnpj_invalidos = { 
                "00000000000000", 
                "11111111111111", 
                "22222222222222", 
                "33333333333333", 
                "44444444444444", 
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999",
            };

            if (cnpj_invalidos.Contains(cnpj))
            {
                return false;
            }

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
            int tamanho = 5;
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

        /// <summary>
        /// Retorna um valor por extenso
        /// </summary>
        /// <param name="valor"> valor decimal</param>
        /// <returns></returns>
        static string ValorExtenso(decimal valor)
        {
            if (valor <= 0)
                return string.Empty;
            else
            {
                string montagem = string.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }
                string strValor = valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));

                if (a == 1) montagem += (b + c == 0) ? "Cem" : "Cento";
                else if (a == 2) montagem += "Duzentos";
                else if (a == 3) montagem += "Trezentos";
                else if (a == 4) montagem += "Quatrocentos";
                else if (a == 5) montagem += "Quinhentos";
                else if (a == 6) montagem += "Seiscentos";
                else if (a == 7) montagem += "Setecentos";
                else if (a == 8) montagem += "Oitocentos";
                else if (a == 9) montagem += "Novecentos";

                if (b == 1)
                {
                    if (c == 0) montagem += ((a > 0) ? " e " : string.Empty) + "Dez";
                    else if (c == 1) montagem += ((a > 0) ? " e " : string.Empty) + "Onze";
                    else if (c == 2) montagem += ((a > 0) ? " e " : string.Empty) + "Doze";
                    else if (c == 3) montagem += ((a > 0) ? " e " : string.Empty) + "Treze";
                    else if (c == 4) montagem += ((a > 0) ? " e " : string.Empty) + "Quatorze";
                    else if (c == 5) montagem += ((a > 0) ? " e " : string.Empty) + "Quinze";
                    else if (c == 6) montagem += ((a > 0) ? " e " : string.Empty) + "Dezesseis";
                    else if (c == 7) montagem += ((a > 0) ? " e " : string.Empty) + "Dezessete";
                    else if (c == 8) montagem += ((a > 0) ? " e " : string.Empty) + "Dezoito";
                    else if (c == 9) montagem += ((a > 0) ? " e " : string.Empty) + "Dezenove";
                } else if (b == 2) montagem += ((a > 0) ? " e " : string.Empty) + "Vinte";
                else if (b == 3) montagem += ((a > 0) ? " e " : string.Empty) + "Trinta";
                else if (b == 4) montagem += ((a > 0) ? " e " : string.Empty) + "Quarenta";
                else if (b == 5) montagem += ((a > 0) ? " e " : string.Empty) + "Cinquenta";
                else if (b == 6) montagem += ((a > 0) ? " e " : string.Empty) + "Sessenta";
                else if (b == 7) montagem += ((a > 0) ? " e " : string.Empty) + "Setenta";
                else if (b == 8) montagem += ((a > 0) ? " e " : string.Empty) + "Oitenta";
                else if (b == 9) montagem += ((a > 0) ? " e " : string.Empty) + "Noventa";

                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " e ";

                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) montagem += "Um";
                    else if (c == 2) montagem += "Dois";
                    else if (c == 3) montagem += "Três";
                    else if (c == 4) montagem += "Quatro";
                    else if (c == 5) montagem += "Cinco";
                    else if (c == 6) montagem += "Seis";
                    else if (c == 7) montagem += "Sete";
                    else if (c == 8) montagem += "Oito";
                    else if (c == 9) montagem += "Nove";

                return montagem;
            }
        }

        /// <summary>
        ///     Escreve valores numéricos por extenso.
        ///     O método EscreverValorFinanceiroExtenso recebe um valor do tipo decimal
        /// </summary>
        /// <param name="pValor">valor decimal</param>
        /// <param name="pValorFinanceiro">Se true = Mostra valor do modo financeiro (reais)</param>
        /// <returns></returns>
        public static string EscreverValorExtenso(decimal pValor, Boolean pValorFinanceiro = true)
        {
            if (pValor <= 0 | pValor >= 1000000000000000)
            {
                throw new OverflowException("Valor não suportado pelo sistema.");
            } else {
                string strValor = pValor.ToString("000000000000000.00");
                string valor_por_extenso = string.Empty;

                for (int i = 0; i <= 15; i += 3)
                {
                    valor_por_extenso += ValorExtenso(Convert.ToDecimal(strValor.Substring(i, 3)));
                    if (i == 0 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                            valor_por_extenso += " Trilhão" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                            valor_por_extenso += " Trilhões" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " e " : string.Empty);
                    } else if (i == 3 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                            valor_por_extenso += " Bilhão" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                            valor_por_extenso += " Bilhões" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " e " : string.Empty);
                    } else if (i == 6 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                            valor_por_extenso += " Milhão" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                            valor_por_extenso += " Milhões" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " e " : string.Empty);
                    } else if (i == 9 & valor_por_extenso != string.Empty)
                        if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                            valor_por_extenso += " Mil" + ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0) ? " e " : string.Empty);
                    if (i == 12)
                    {
                        if (pValorFinanceiro)
                        {
                            if (valor_por_extenso.Length > 8)
                                if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "Bilhão" || valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "Milhão")
                                    valor_por_extenso += " de";
                                else if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "Bilhões" || valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "Milhões" || valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "Trilhões")
                                    valor_por_extenso += " de";
                                else if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "Trilhões")
                                    valor_por_extenso += " de";
                            if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                                valor_por_extenso += " Real";
                            else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                                valor_por_extenso += " Reais";
                            if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != string.Empty)
                                valor_por_extenso += " e ";

                        }
                    }

                    if ((i == 15) && (pValorFinanceiro))
                    {
                        if (Convert.ToInt32(strValor.Substring(16, 2)) == 1)
                            valor_por_extenso += " centavo";
                        else if (Convert.ToInt32(strValor.Substring(16, 2)) > 1)
                            valor_por_extenso += " centavos";
                    }
                }
                return valor_por_extenso;
            }
        }

        public static string FormatarCpfCnpj(string strCpfCnpj)
        {
            if (strCpfCnpj.Length <= 11)
            {
                MaskedTextProvider mtpCpf = new MaskedTextProvider(@"000\.000\.000-00");
                mtpCpf.Set(StringFunctions.ZerosEsquerda(strCpfCnpj, 11));
                return mtpCpf.ToString();
            } else
            {
                MaskedTextProvider mtpCnpj = new MaskedTextProvider(@"00\.000\.000/0000-00");
                mtpCnpj.Set(StringFunctions.ZerosEsquerda(strCpfCnpj, 11));
                return mtpCnpj.ToString();
            }
        }

        public static string FormatarCNPJ(string pCnpj)
        {
            string resposta = Convert.ToUInt64(pCnpj).ToString(@"00\.000\.000\/0000\-00");

            return resposta;
        }

        public static string FormatarCPF(string pCpf)
        {
            string resposta = Convert.ToUInt64(pCpf).ToString(@"000\.000\.000\-00");

            return resposta;
        }

        public static List<int> ObtenhaUltimosCincoAnos(int anoReferencia)
        {
            List<int> anos = new List<int>();
            for (int ano = anoReferencia; ano >= (anoReferencia - 5); ano--)
            {
                anos.Add(ano);
            }

            return anos;
        }

        public static decimal CalculaPorcentagem(decimal total, decimal valor)
        {
            if (total == 0)
                return 0;

            return valor / total * 100;
        }

    }
}
