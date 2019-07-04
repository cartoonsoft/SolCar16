using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppServices.Cartorio.Interfaces.Base;
using Domain.Cartorio.Entities.CartorioNew;
using Dto.Cartorio.Entities.Cadastros;
using Dto.Cartorio.Entities.Diversos;
using System.Collections;

namespace AppServices.Cartorio.Interfaces
{
    public interface IAppServicePessoa : IAppServiceCartorio<DtoPessoaCartorioNew, PessoaCartorioNew>
    {
        IEnumerable<DtoPessoaPesxPre> GetPessoasPorPrenotacao(long IdPrenotacao);
    }
}
