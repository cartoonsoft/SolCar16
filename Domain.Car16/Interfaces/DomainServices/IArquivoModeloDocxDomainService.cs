using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.Entities.Diversas;

namespace Domain.Car16.Interfaces.DomainServices
{
    public interface IArquivoModeloDocxDomainService : IDomainServiceCar16New<ArquivoModeloDocx>
    {
        IEnumerable<ArquivoModeloDocxList> ListarArquivoModeloDocx(long? IdTipoAto = null);

        IEnumerable<ArquivoModeloSimplificadoDocxList> ListarArquivoModeloSimplificadoDocx(long? IdTipoAto = null);

        long? SalvarModelo(ArquivoModeloDocx arquivoModeloDocx, LogArquivoModeloDocx logArquivoModeloDocx, string IdUsuario);
        long? EditarModelo(LogArquivoModeloDocx logArquivoModeloDocx);
    }
}
