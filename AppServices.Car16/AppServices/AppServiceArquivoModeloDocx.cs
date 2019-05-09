using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AppServices.Car16.AppServices.Base;
using AppServices.Car16.Interfaces;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.enums;
using Domain.Car16.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Cadastros;
using Dto.Car16.Entities.Diversos;
using Domain.Car16.Entities.Diversas;
using Domain.Car16.Interfaces.DomainServices;
using Domain.Car16.DomainServices;

namespace AppServices.Car16.AppServices
{
    public class AppServiceArquivoModeloDocx : AppServiceCar16New<DtoArquivoModeloDocxModel, ArquivoModeloDocx>, IAppServiceArquivoModeloDocx
    {
        IArquivoModeloDocxDomainService _arquivoModeloDocxDomainService;

        public AppServiceArquivoModeloDocx(IUnitOfWorkDataBaseCar16New unitOfWorkCar16) : base(unitOfWorkCar16)
        {
            //
            _arquivoModeloDocxDomainService = new ArquivoModeloDocxDomainService(unitOfWorkCar16);
        }

        public void SalvarModelo(DtoArquivoModeloDocxModel dtoArq, string IdUsuario)
        {
            try
            {
                // Criando objeto do arquivo 
                ArquivoModeloDocx arquivoModelo = new ArquivoModeloDocx
                {
                    IdContaAcessoSistema = dtoArq.IdContaAcessoSistema,
                    Ativo = dtoArq.Ativo,
                    IdTipoAto = dtoArq.IdTipoAto,
                    IdUsuarioCadastro = IdUsuario,
                    //ArquivoBytes = dtoArq.ArquivoByte,
                    CaminhoEArquivo = dtoArq.Arquivo,
                    NomeModelo = dtoArq.NomeModelo,
                };

                // Registro de Log                
                LogArquivoModeloDocx logArquivoModeloDocx = new LogArquivoModeloDocx();
                logArquivoModeloDocx.IdArquivoModeloDocx = arquivoModelo.Id ?? 0;
                logArquivoModeloDocx.IdUsuario = IdUsuario;
                logArquivoModeloDocx.DataHora = DateTime.Now;
                logArquivoModeloDocx.IP = dtoArq.LogArquivo.IP;
                logArquivoModeloDocx.TipoLogArquivoModeloDocx = TipoLogArquivoModeloDocx.Upload;

                _arquivoModeloDocxDomainService.SalvarModelo(arquivoModelo, logArquivoModeloDocx, IdUsuario);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Desativa o Modelo
        /// </summary>
        /// <param name="Id">ID do modelo</param>
        /// <param name="IdSuario">ID do usuario</param>
        public int DesativarModelo(long Id/*, long IdSuari0*/)
        {
            try
            {
                // Criando objeto do arquivo 
                ArquivoModeloDocx arquivoModelo = this.DomainServices.GenericDomainService<ArquivoModeloDocx>().GetById(Id);
                if (arquivoModelo != null)
                {
                    arquivoModelo.Ativo = false;
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        
        public IEnumerable<DtoArquivoModeloDocxList> ListarArquivoModeloDocx(long? IdTipoAto = null)
        {
            IEnumerable<ArquivoModeloDocxList> listaDomain = this.DomainServices.ArquivoModeloDocxDomainService.ListarArquivoModeloDocx(IdTipoAto);
            IEnumerable <DtoArquivoModeloDocxList> listaDto = Mapper.Map<IEnumerable<ArquivoModeloDocxList>, IEnumerable<DtoArquivoModeloDocxList>>(listaDomain);
            return listaDto;
        }

        public IEnumerable<DtoArquivoModeloSimplificadoDocxList> ListarArquivoModeloSimplificado(long? IdTipoAto = null)
        {
            IEnumerable<ArquivoModeloSimplificadoDocxList> listaDomain = this.DomainServices.ArquivoModeloDocxDomainService.ListarArquivoModeloSimplificadoDocx(IdTipoAto);
            IEnumerable<DtoArquivoModeloSimplificadoDocxList> listaDto = Mapper.Map<IEnumerable<ArquivoModeloSimplificadoDocxList>, IEnumerable<DtoArquivoModeloSimplificadoDocxList>>(listaDomain);

            return listaDto;
        }
    }
}
