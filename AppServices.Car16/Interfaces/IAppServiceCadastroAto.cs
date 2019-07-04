using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Cartorio.Interfaces.Base;
using Domain.Cartorio.Entities.Diversas;
using Dto.Cartorio.Entities.Cadastros;

namespace AppServices.Cartorio.Interfaces
{
    public interface IAppServiceCadastroAto : IAppServiceCartorio<DtoCadastroDeAto,CadastroDeAto>
    {
        bool EscreverAtoNoWord(DtoCadastroDeAto modelo, string filePath, long numSequenciaAto);
    }
}
