using Domain.Car16.Entities.Car16;
using Domain.Car16.Interfaces.Repositories;
using Domain.Core.Interfaces.Data;
using Infra.Data.Car16.Context;
using Infra.Data.Car16.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Car16.Repositories
{
    public class RepositoryPREIMO : RepositoryBaseRead<PREIMO>, IRepositoryPREIMO
    {
        private readonly ContextMainCar16 _contexRep;

        public RepositoryPREIMO(ContextMainCar16 context) : base(context)
        {
            _contexRep = context;
        }
        /// <summary>
        /// Função que busca os dados do imóvel a partir da matricula ou prenotação
        /// </summary>
        /// <param name="numeroPrenotacao">Número da prenotação</param>
        /// <param name="numeroMatricula">Número da matrícula</param>
        /// <returns>Retorna uma linha de registro</returns>
        public PREIMO BuscaDadosImovel(long? numeroPrenotacao = null, long? numeroMatricula = null)
        {
            IEnumerable<PREIMO> listaImoveis = new List<PREIMO>();


            if (numeroPrenotacao != null && numeroMatricula != null)
            {
                listaImoveis = _contexRep.DbPREIMO
                    .Where(mp => mp.SEQIMO == numeroMatricula && mp.SEQPRE == numeroPrenotacao)
                    .OrderByDescending(mp => mp.SEQPRE);


            }
            else if (numeroMatricula != null)
            {
                listaImoveis = _contexRep.DbPREIMO
                    .Where(mp => mp.SEQIMO == numeroMatricula)
                    .OrderByDescending(mp => mp.SEQPRE);               
            }
            else if (numeroPrenotacao != null)
            {
                listaImoveis = _contexRep.DbPREIMO
                       .Where(mp => mp.SEQPRE == numeroPrenotacao)
                       .OrderByDescending(mp => mp.SEQPRE);
            }
            else
            {
                listaImoveis = _contexRep.DbPREIMO.OrderByDescending(mp => mp.SEQPRE);
            }

            return listaImoveis.FirstOrDefault();
        }
    }
}
