using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Entities;
using Domain.CartNew.Entities.Diversos;
using Domain.CartNew.Interfaces.UnitOfWork;
using DomainServ.CartNew.Base;
using DomainServ.CartNew.Interfaces;

namespace DomainServ.CartNew.Services
{
    public class PessoaCartNewDs : DomainServiceCartorioNew<PessoaCartNew>, IPessoaCartNewDs
    {
        //O - Outorgado, E - Outorgante
        private readonly string[] Relacoes = { "O", "E" };
   
        //private IEnumerable<CamposArquivoModeloDocx> listaCamposArquivoModeloDocx = null;

        public PessoaCartNewDs(IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(UfwCartNew)
        {
            //
        }

    }
}
