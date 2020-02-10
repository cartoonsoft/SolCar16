using System;
using System.Collections.Generic;
using Domain.CartNew.Entities;
using Domain.CartNew.Enumerations;
using Domain.CartNew.Interfaces.UnitOfWork;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using AppServices.Cartorio.Interfaces;
using AppServCart11RI.Base;
using AppServCart11RI.Cartorio;

namespace AppServCart11RI.AppServices
{
    public class AppServiceModelosDoc : AppServiceCartorio11RI<DtoModeloDoc, ModeloDoc>, IAppServiceModelosDoc
    {
        //private List<CamposArquivoModeloDocx> listaCamposArquivoModeloDocx = null;

        public AppServiceModelosDoc(IUnitOfWorkDataBaseCartNew UfwCartNew, long IdCtaAcessoSist) : base(UfwCartNew, IdCtaAcessoSist)
        {
            
        }

        public long? NovoModelo(DtoModeloDoc dtoModeloDoc)
        {
            long? NovoId = null;

            try
            {
                NovoId = this.DsFactoryCartNew.ModeloDocxDs.NovoModelo(dtoModeloDoc);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return NovoId;
        }

        public void EditarModelo(DtoModeloDoc dtoModeloDoc)
        {
            try
            {
                this.DsFactoryCartNew.ModeloDocxDs.EditarModelo(dtoModeloDoc);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Desativar(long Id, string IdUsuario)
        {
            bool resposta = false;

            try
            {
                resposta = this.DsFactoryCartNew.ModeloDocxDs.Desativar(Id, IdUsuario);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

            return resposta;
        }
        
        public IEnumerable<DtoModeloDocxList> GetListModelosDocx(long? IdTipoAto = null)
        {
            IEnumerable<DtoModeloDocxList> listaDs = this.DsFactoryCartNew.ModeloDocxDs.GetListModelosDocx(IdTipoAto);
            return listaDs;
        }

    }
}
