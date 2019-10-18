using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Globalization;
using Domain.CartNew.Entities;
using Domain.CartNew.Enumerations;
using Domain.CartNew.Interfaces.UnitOfWork;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using AppServ.Core.AppServices;
using AppServices.Cartorio.Interfaces;

namespace AppServCart11RI.AppServices
{
    public class AppServiceModelosDoc : AppServiceCartorio<DtoModeloDoc, ModeloDoc>, IAppServiceModelosDoc
    {
        //private List<CamposArquivoModeloDocx> listaCamposArquivoModeloDocx = null;

        public AppServiceModelosDoc(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //
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
                    //ArquivoBytes = dtoArq.ArquivoByte,
                    CaminhoEArquivo = dtoModeloDoc.CaminhoEArquivo,
                    Descricao = dtoModeloDoc.DescricaoModelo,
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
                //Console.WriteLine(ex.Message);
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
                    //ArquivoBytes = dtoArq.ArquivoByte,
                    CaminhoEArquivo = dtoModeloDoc.CaminhoEArquivo,
                    Descricao = dtoModeloDoc.DescricaoModelo
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
        
        public IEnumerable<DtoModeloDocxList> GetListaModelosDocx(long? IdTipoAto = null)
        {
            IEnumerable<DtoModeloDocxList> listaDs = this.DsFactoryCartNew.ModeloDocxDs.GetListModelosDocx(IdTipoAto);
            return listaDs;
        }

    }
}
