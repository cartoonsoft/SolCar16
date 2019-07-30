using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using DomainServ.CartNew.Interfaces.Base;
using Domain.CartNew.Entities;

namespace DomainServ.CartNew.Interfaces
{
    public interface IAtoDs : IDomainServiceCartorioNew<Ato>
    {
        bool ExisteAtoCadastrado(long numMatricula);

        /// <summary>
        /// Retorda com o numero da ultima ficha gravada, caso não exista retorna : 0
        /// </summary>
        /// <param name="NumMatricula"></param>
        /// <returns></returns>
        Docx GetUltimaFichaGravada(string NumMatricula);

        short GetUltimoNumFicha(string NumMatricula);

    }
}
