using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Cartorio.Entities.Diversas;
using DomainServices.Interfaces.Base;
using Domain.Car16.Entities.Car16New;

namespace DomainServices.Interfaces
{
    public interface IArquivoModeloDocxDs : IDomainServiceCartorioNew<ArquivoModeloDocx>
    {
        IEnumerable<ArquivoModeloDocxList> ListarArquivoModeloDocx(long? IdTipoAto = null);

        IEnumerable<ArquivoModeloSimplificadoDocxList> ListarArquivoModeloSimplificadoDocx(long? IdTipoAto = null);

        long? NovoModelo(ArquivoModeloDocx arquivoModeloDocx, LogArquivoModeloDocx logArquivoModeloDocx, string IdUsuario);
        long? EditarModelo(ArquivoModeloDocx arquivoModeloDocx, LogArquivoModeloDocx logArquivoModeloDocx, string IdUsuario);

        bool Desativar(long Id, string IdUsuario);
    }
}
