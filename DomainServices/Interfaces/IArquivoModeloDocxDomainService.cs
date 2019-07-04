using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Cartorio.Entities.CartorioNew;
using Domain.Cartorio.Entities.Diversas;

namespace DomainServices.Interfaces
{
    public interface IArquivoModeloDocxDomainService : IDomainServiceCartorioNew<ArquivoModeloDocx>
    {
        IEnumerable<ArquivoModeloDocxList> ListarArquivoModeloDocx(long? IdTipoAto = null);

        IEnumerable<ArquivoModeloSimplificadoDocxList> ListarArquivoModeloSimplificadoDocx(long? IdTipoAto = null);

        long? SalvarModelo(ArquivoModeloDocx arquivoModeloDocx, LogArquivoModeloDocx logArquivoModeloDocx, string IdUsuario);
        long? EditarModelo(LogArquivoModeloDocx logArquivoModeloDocx);
    }
}
