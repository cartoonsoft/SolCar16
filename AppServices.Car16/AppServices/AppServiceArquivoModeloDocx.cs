using AppServices.Car16.AppServices.Base;
using AppServices.Car16.Interfaces;
using Domain.Car16.Entities;
using Domain.Car16.Enumeradores;
using Domain.Core.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Car16.AppServices
{
    public class AppServiceArquivoModeloDocx : AppServiceBase<DtoArquivoModeloDocxModel, ArquivoModeloDocx>, IAppServiceArquivoModeloDocx
    {
        public AppServiceArquivoModeloDocx(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            //
        }

        public int BuscarUltimaVersao(long IdArquivo)
        {
            return this.domainService<ArquivoModeloDocxHist>()
                .GetWhere(a => a.ArquivoModeloDocxId == IdArquivo)
                .Max(x => x.Versao);
        }

        public void SalvarModelo(DtoArquivoModeloDocxModel dtoArq)
        {
            try
            {
                // Criando objeto do arquivo 
                var arquivoModelo = new ArquivoModeloDocx
                {
                    ArquivoByte = dtoArq.ArquivoByte,
                    CaminhoArquivo = dtoArq.CaminhoArquivo,
                    ExtensaoArquivo = dtoArq.ExtensaoArquivo,
                    NomeArquivo = dtoArq.NomeArquivo,
                    NomeModelo = dtoArq.NomeModelo,
                    TipoArquivoModelo = dtoArq.TipoArquivoModelo
                };
                this.domainService<ArquivoModeloDocx>().Add(arquivoModelo);

                // Registro de Log                
                this.domainService<LogArquivoModeloDocx>()
                    .Add(new LogArquivoModeloDocx()
                    {
                        ArquivoID = arquivoModelo.Id,
                        DataHora = DateTime.Now,
                        IP = dtoArq.LogArquivoModeloDocx.IP,
                        NomeUsuario = dtoArq.LogArquivoModeloDocx.NomeUsuario,
                        TipoLogArquivoModeloDocx = TipoLogArquivoModeloDocx.Upload
                    });

                // Registro de Historico
                this.domainService<ArquivoModeloDocxHist>()
                    .Add(new ArquivoModeloDocxHist()
                    {
                        ArquivoModeloDocxId = arquivoModelo.Id,
                        Versao = BuscarUltimaVersao(arquivoModelo.Id) + 1
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            } 
        }


    }
}
