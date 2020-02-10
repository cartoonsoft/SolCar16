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

        public long? NovoModelo(DtoModeloDoc dtoModeloDoc, string IdUsuario)
        {
            long? NovoId = null;

            try
            {
                // Criando objeto do arquivo 
                ModeloDoc arquivoModelo = new ModeloDoc
                {
                    Id = dtoModeloDoc.Id,
                    IdCtaAcessoSist = dtoModeloDoc.IdCtaAcessoSist,
                    IdTipoAto = dtoModeloDoc.IdTipoAto,
                    IdUsuarioCadastro = IdUsuario,
                    Descricao = dtoModeloDoc.Descricao,
                    Ativo = dtoModeloDoc.Ativo,
                };

                // Registro de Log                
                LogModeloDoc logModeloDocx = new LogModeloDoc()
                {
                    IdModeloDoc = arquivoModelo.Id ?? 0,
                    IdUsuario = IdUsuario,
                    DataHora = DateTime.Now,
                    UsuarioSistOperacional = dtoModeloDoc.UsuarioSistOperacional,
                    IP = dtoModeloDoc.IpLocal,
                    TipoLogModeloDoc = TipoLogModeloDoc.Upload
                };

                NovoId = this.DsFactoryCartNew.ModeloDocxDs.NovoModelo(arquivoModelo, logModeloDocx, IdUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return NovoId;
        }

        public void EditarModelo(DtoModeloDoc dtoModeloDoc, string IdUsuario)
        {
            try
            {
                ModeloDoc modeloDoc = new ModeloDoc
                {
                    Id = dtoModeloDoc.Id,
                    IdCtaAcessoSist = dtoModeloDoc.IdCtaAcessoSist,
                    Ativo = dtoModeloDoc.Ativo,
                    IdTipoAto = dtoModeloDoc.IdTipoAto,
                    IdUsuarioAlteracao = IdUsuario,
                    Descricao = dtoModeloDoc.Descricao
                };

                //HttpPostedFileBase arquivo = dtoArq.Files[0];

                // Salva o arquivo fisicamente
                //arquivo.SaveAs(dtoArq.CaminhoEArquivo);

                // Registro de Log                
                LogModeloDoc logModeloDocx = new LogModeloDoc()
                {
                    IdModeloDoc = dtoModeloDoc.Id??0,
                    IdUsuario = IdUsuario,
                    DataHora = DateTime.Now,
                    UsuarioSistOperacional = dtoModeloDoc.UsuarioSistOperacional,
                    IP = dtoModeloDoc.IpLocal,
                    TipoLogModeloDoc = TipoLogModeloDoc.Upload
                };

                //logArquivoModeloDocx.Id = this.DsFactoryCartNew.ArquivoModeloDocxDs.EditarModelo(arquivoModelo, logModeloDocx, IdUsuario);
            }
            catch (Exception)
            {
                //    
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
                Console.WriteLine(ex.Message);
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
