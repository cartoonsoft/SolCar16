using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using DomainServ.CartNew.Interfaces.Base;
using Domain.CartNew.Entities;

namespace DomainServ.CartNew.Interfaces
{
    public interface IArquivoModeloDocxDs : IDomainServiceCartorioNew<ArquivoModeloDocx>
    {
        //IEnumerable<ArquivoModeloDocxList> ListarArquivoModeloDocx(long? IdTipoAto = null);

        //IEnumerable<ArquivoModeloSimplificadoDocxList> ListarArquivoModeloSimplificadoDocx(long? IdTipoAto = null);

        long? NovoModelo(ArquivoModeloDocx arquivoModeloDocx, LogArquivoModeloDocx logArquivoModeloDocx, string IdUsuario);
        long? EditarModelo(ArquivoModeloDocx arquivoModeloDocx, LogArquivoModeloDocx logArquivoModeloDocx, string IdUsuario);

        bool Desativar(long Id, string IdUsuario);
    }
}
