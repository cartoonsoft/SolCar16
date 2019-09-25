using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Entities;
using Domain.Core.Interfaces.Repositories;

namespace Domain.CartNew.Interfaces.Repositories
{
    public interface IRepositoryAto : IRepositoryBaseReadWrite<Ato>
    {
        /// <summary>
        /// Função que retorna se o ato está cadastrado no nosso banco
        /// Serve para controlar se criara novo arquivo ou abrira e continuará a escrever no qual que existe
        /// </summary>
        /// <param name="numMatricula">Número da Matricula</param>
        /// <returns>Se o ato existe no banco (true or false) </returns>
        bool ExisteAtoCadastrado(long numMatricula);

        long? GetNumSequenciaAto(long numeroMatricula);
    }
}
