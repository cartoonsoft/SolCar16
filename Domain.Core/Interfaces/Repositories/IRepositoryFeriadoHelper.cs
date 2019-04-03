/*----------------------------------------------------------------------------
  _____            _                    _____        __ _   
/  __ \          | |                  /  ___|      / _| |  
| /  \/ __ _ _ __| |_ ___   ___  _ __ \ `--.  ___ | |_| |_ 
| |    / _` | '__| __/ _ \ / _ \| '_ \ `--. \/ _ \|  _| __|
| \__/\ (_| | |  | || (_) | (_) | | | /\__/ / (_) | | | |_ 
 \____/\__,_|_|   \__\___/ \___/|_| |_\____/ \___/|_|  \__|
Todos os direitos reservados ®                       
-----------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities;

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
