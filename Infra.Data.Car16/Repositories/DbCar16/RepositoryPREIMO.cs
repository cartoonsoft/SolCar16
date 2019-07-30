using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cart.Entities;
using Domain.Cart.Interfaces.Repositories;
using Infra.Data.Cartorio.Context;
using Infra.Data.Cartorio.Repositories.Base;

namespace Infra.Data.Cartorio.Repositories
{
    public class RepositoryPREIMO : RepositoryBaseReadWrite<PREIMO>, IRepositoryPREIMO
    {
        private readonly ContextMainCartorio _contexRepository;

        public RepositoryPREIMO(ContextMainCartorio contexRepository) : base(contexRepository)
        {
            _contexRepository = contexRepository;
        }

        public PREIMO BuscaDadosImovel(long? numeroPrenotacao = null, long? numeroMatricula = null)
        {
            IEnumerable<PREIMO> listaImoveis = new List<PREIMO>();

            if (numeroPrenotacao != null && numeroMatricula != null)
            {
                listaImoveis = this.GetWhereOrderBy(mp => mp.MATRI == numeroMatricula && mp.SEQPRE == numeroPrenotacao, mp => mp.SEQPRE, false);

            }
            else if (numeroMatricula != null)
            {
                listaImoveis = this.GetWhereOrderBy(m => m.MATRI == numeroMatricula, mp => mp.SEQPRE, false);
            }
            else if (numeroPrenotacao != null)
            {
                listaImoveis = this.GetWhereOrderBy(p => p.SEQPRE == numeroPrenotacao, mp => mp.SEQPRE, false);
            }
            else
            {
                listaImoveis = this.GetAll().OrderByDescending(mp => mp.SEQPRE);
            }


            return listaImoveis.FirstOrDefault();
        }

    }
}
