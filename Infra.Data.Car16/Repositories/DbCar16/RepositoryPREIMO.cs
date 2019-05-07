using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities.Car16;
using Domain.Car16.Interfaces.Repositories;
using Infra.Data.Car16.Context;
using Infra.Data.Car16.Repositories.Base;

namespace Infra.Data.Car16.Repositories
{
    public class RepositoryPREIMO : RepositoryBaseRead<PREIMO>, IRepositoryPREIMO
    {
        private readonly ContextMainCar16 _contexRep;

        public RepositoryPREIMO(ContextMainCar16 contexRep) : base(contexRep)
        {
            _contexRep = contexRep;
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
