﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cartorio.Entities.CartorioNew;
using Domain.Cartorio.Interfaces.UnitOfWork;
using Dto.Cartorio.Entities.Diversos;

namespace DomainServices.Interfaces
{
    public interface IPessoaDomainService : IDomainServiceCartorioNew<PessoaCartorioNew>
    {
        IEnumerable<DtoPessoaPesxPre> GetPessoasPorPrenotacao(long IdPrenotacao);
        IEnumerable<DtoPessoaPesxPre> GetListaOutorgadosOutorgantes(long[] listIdsPessoas, long? IdTipoAto, long IdPrenotacao);
    }
}
