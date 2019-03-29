using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Interfaces.Repositories
{
    public interface IRepositoryFeriadoHelper: IRepositoryBaseReadWrite<Feriado>
    {
        IEnumerable<Feriado> FeriadosDoAno(int Ano);

        /// <summary>
        /// Lista de Datas que São Feriados par uma localidade
        /// </summary>
        /// <param name="DataInicial"></param>
        /// <param name="DataFinal"></param>
        /// <returns></returns>
        IEnumerable<Feriado> FeriadosIntervalo(DateTime DataInicial,  DateTime DataFinal, long? IdMunicipio = null);

        /// <summary>
        /// Data Correspondente a x dias úteis a partir de uma data
        /// </summary>
        /// <param name="DataInicial"></param>
        /// <param name="PrazoEmDiasUteis"></param>
        /// <returns></returns>
        DateTime DiaUtil(DateTime DataInicial, int PrazoEmDiasUteis); 

    }
}
