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
    public class AppServiceModelosDocx : AppServiceCartorio<DtoModeloDocx, ModeloDocx>, IAppServiceModelosDocx
    {
        //private List<CamposArquivoModeloDocx> listaCamposArquivoModeloDocx = null;

        public AppServiceModelosDocx(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //
        }

        public long? NovoModelo(DtoModeloDocx dtoArq, string IdUsuario)
        {
            long? NovoId = null;

            try
            {
                // Criando objeto do arquivo 
                ModeloDocx arquivoModelo = new ModeloDocx
                {
                    Id = dtoArq.Id,
                    IdCtaAcessoSist = dtoArq.IdCtaAcessoSist,
                    IdTipoAto = dtoArq.IdTipoAto,
                    IdUsuarioCadastro = IdUsuario,
                    //ArquivoBytes = dtoArq.ArquivoByte,
                    CaminhoEArquivo = dtoArq.CaminhoEArquivo,
                    DescricaoModelo = dtoArq.DescricaoModelo,
                    Ativo = dtoArq.Ativo,
                };

                // Registro de Log                
                LogModeloDocx logModeloDocx = new LogModeloDocx()
                {
                    IdModeloDocx = arquivoModelo.Id ?? 0,
                    IdUsuario = IdUsuario,
                    DataHora = DateTime.Now,
                    UsuarioSistOperacional = dtoArq.UsuarioSistOperacional,
                    IP = dtoArq.IpLocal,
                    TipoLogModeloDocx = TipoLogModeloDocx.Upload
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

        public void EditarModelo(DtoModeloDocx dtoArq, string IdUsuario)
        {
            try
            {
                ModeloDocx arquivoModelo = new ModeloDocx
                {
                    Id = dtoArq.Id,
                    IdCtaAcessoSist = dtoArq.IdCtaAcessoSist,
                    Ativo = dtoArq.Ativo,
                    IdTipoAto = dtoArq.IdTipoAto,
                    IdUsuarioAlteracao = IdUsuario,
                    //ArquivoBytes = dtoArq.ArquivoByte,
                    CaminhoEArquivo = dtoArq.CaminhoEArquivo,
                    DescricaoModelo = dtoArq.DescricaoModelo
                };

                //HttpPostedFileBase arquivo = dtoArq.Files[0];

                // Salva o arquivo fisicamente
                //arquivo.SaveAs(dtoArq.CaminhoEArquivo);

                // Registro de Log                
                LogModeloDocx logModeloDocx = new LogModeloDocx()
                {
                    IdModeloDocx = dtoArq.Id??0,
                    IdUsuario = IdUsuario,
                    DataHora = DateTime.Now,
                    UsuarioSistOperacional = dtoArq.UsuarioSistOperacional,
                    IP = dtoArq.IpLocal,
                    TipoLogModeloDocx = TipoLogModeloDocx.Upload
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
                //resposta = this.DsFactoryCartNew.ArquivoModeloDocxDs.Desativar(Id, IdUsuario);
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
            IEnumerable<DtoModeloDocxList> listaDs = this.DsFactoryCartNew.ModeloDocxDs.GetListaModelosDocx(IdTipoAto);
            return listaDs;
        }

    }
}
