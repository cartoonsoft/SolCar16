using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppServices.Cartorio.Interfaces.Base;
using Dto.Cartorio.Entities.Cadastros;
using Dto.Cartorio.Entities.Diversos;
using Domain.CartNew.Entities;
using Domain.Cart.Entities;

namespace AppServices.Cartorio.Interfaces
{
    public interface IAppServicePessoa : IAppServiceCartorio<DtoPessoaCartorio, PessoaCart>
    {
        IEnumerable<DtoPessoaPesxPre> GetPessoasPorPrenotacao(long IdPrenotacao);
    }
}
