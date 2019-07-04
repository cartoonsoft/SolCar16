using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Cartorio.Entities.CartorioNew;

namespace DomainServices.Interfaces
{
    public interface IAtoDomainService : IDomainServiceCartorioNew<Ato>
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
