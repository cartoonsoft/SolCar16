﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Car16.AppServices.Base;
using AppServices.Car16.Interfaces;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.enums;
using Domain.Car16.Interfaces.UnitOfWork;
using Domain.Core.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Cadastros;
using Dto.Car16.Entities.Diversos;
using Domain.Car16.Entities.Diversas;
using AutoMapper;

namespace AppServices.Car16.AppServices
{
    public class AppServiceArquivoModeloDocx : AppServiceCar16<DtoArquivoModeloDocxModel, ArquivoModeloDocx>, IAppServiceArquivoModeloDocx
    {
        public AppServiceArquivoModeloDocx(IUnitOfWorkCar16 unitOfWorkCar16) : base(unitOfWorkCar16)
        {
            //
        }

        public void SalvarModelo(DtoArquivoModeloDocxModel dtoArq, string IdSuario)
        {
            try
            {
                // Criando objeto do arquivo 
                ArquivoModeloDocx arquivoModelo = new ArquivoModeloDocx
                {
                    IdContaAcessoSistema = dtoArq.IdContaAcessoSistema,
                    Ativo = dtoArq.Ativo,
                    IdTipoAto = dtoArq.IdTipoAto,
                    ArquivoBytes = dtoArq.ArquivoByte,
                    CaminhoEArquivo = dtoArq.Arquivo,
                    NomeModelo = dtoArq.NomeModelo,
                };

                this.DomainServices.GenericDomainService<ArquivoModeloDocx>().Add(arquivoModelo);

                // Registro de Log                
                //this.DomainServices.GenericDomainService<LogArquivoModeloDocx>()
                //    .Add(new LogArquivoModeloDocx()
                //    {
                //        IdArquivoModeloDocx = arquivoModelo.Id??0,
                //        IdUsuario = IdSuario,
                //        DataHora = DateTime.Now,
                //        IP = dtoArq.LogArquivoModeloDocx.IP,
                //        TipoLogArquivoModeloDocx = TipoLogArquivoModeloDocx.Upload
                //    });
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

    }
}