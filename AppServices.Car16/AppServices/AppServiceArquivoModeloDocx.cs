﻿using System;
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
using System.Web;

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

        public long? SalvarModelo(DtoArquivoModeloDocxModel dtoArq, string IdUsuario)
        {
            long? NovoId = null;

            try
            {
                // Criando objeto do arquivo 
                ArquivoModeloDocx arquivoModelo = new ArquivoModeloDocx
                {
                    Id = dtoArq.Id,
                    IdContaAcessoSistema = dtoArq.IdContaAcessoSistema,
                    Ativo = dtoArq.Ativo,
                    IdTipoAto = dtoArq.IdTipoAto,
                    IdUsuarioCadastro = IdUsuario,
                    //ArquivoBytes = dtoArq.ArquivoByte,
                    CaminhoEArquivo = dtoArq.CaminhoEArquivo,
                    NomeModelo = dtoArq.NomeModelo,
                };

                // Registro de Log                
                LogArquivoModeloDocx logArquivoModeloDocx = new LogArquivoModeloDocx()
                {
                    IdArquivoModeloDocx = arquivoModelo.Id ?? 0,
                    IdUsuario = IdUsuario,
                    DataHora = DateTime.Now,
                    UsuarioSistOperacional = dtoArq.LogArquivo.UsuarioSistOperacional,
                    IP = dtoArq.LogArquivo.IP,
                    TipoLogArquivoModeloDocx = TipoLogArquivoModeloDocx.Upload
                };

                NovoId = _arquivoModeloDocxDomainService.SalvarModelo(arquivoModelo, logArquivoModeloDocx, IdUsuario);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

            return NovoId;
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

        public void EditarModelo(DtoArquivoModeloDocxModel dtoArq, string IdUsuario)
        {
            try
            {
                HttpPostedFileBase arquivo = dtoArq.Files[0];
                #region | Gravacao do arquivo fisicamente |
                // Salva o arquivo fisicamente
                arquivo.SaveAs(dtoArq.CaminhoEArquivo);
                #endregion

                // Registro de Log                
                LogArquivoModeloDocx logArquivoModeloDocx = new LogArquivoModeloDocx()
                {
                    IdArquivoModeloDocx = Convert.ToInt64(dtoArq.Id),
                    IdUsuario = IdUsuario,
                    DataHora = DateTime.Now,
                    UsuarioSistOperacional = dtoArq.LogArquivo.UsuarioSistOperacional,
                    IP = dtoArq.LogArquivo.IP,
                    TipoLogArquivoModeloDocx = TipoLogArquivoModeloDocx.Upload                    
                };

                logArquivoModeloDocx.Id = this._arquivoModeloDocxDomainService.EditarModelo(logArquivoModeloDocx);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
