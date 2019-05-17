﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Car16.AppServices.Base;
using AppServices.Car16.Interfaces;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Cadastros;
using Dto.Car16.Entities.Diversos;

namespace AppServices.Car16.AppServices
{
    public class AppServiceAto : AppServiceCar16New<DtoAto, Ato>, IAppServiceAto
    {
        public AppServiceAto(IUnitOfWorkDataBaseCar16New unitOfWork) : base(unitOfWork)
        {
            //
        }

        public bool Editar(DtoCadastroDeAto modelo, string UsuarioAlteracao)
        {
            try
            {
                Ato ato = this.UnitOfWorkCar16New.Repositories.GenericRepository<Ato>().GetById(modelo.IdAto);
                ato.Ativo = true;
                ato.Bloqueado = false;
                ato.IdPrenotacao = modelo.PREIMO.SEQPRE;
                ato.IdTipoAto = modelo.IdTipoAto;
                ato.NomeArquivo = $"{ modelo.PREIMO.MATRI }.docx";
                ato.Observacao = "Cadastro de teste";
                ato.NumMatricula = modelo.PREIMO.MATRI.ToString();
                ato.IdUsuarioAlteracao = UsuarioAlteracao;
                ato.IdContaAcessoSistema = 1;
                ato.NumSequencia = Convert.ToInt64(modelo.NumSequencia);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// Verifica se já existe ato cadastrado
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool ExisteAtoCadastrado(long numeroMatricula)
        {                   
            //Se ato > 1, então existe o ato inicial
            return this.UnitOfWorkCar16New.Repositories.RepositoryAto.ExisteAtoCadastrado(numeroMatricula);
        }

        public bool FinalizarAto(long Id)
        {
            try
            {
                var ato = this.UnitOfWorkCar16New.Repositories.RepositoryAto.GetById(Id);
                if( ato == null )
                {
                    return false;
                }
                ato.Bloqueado = true;
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// Listar aot para o grid index
        /// </summary>
        /// <param name="IdTipoAto"></param>
        /// <param name="IdUsuario"></param>
        /// <returns></returns>
        public IEnumerable<DtoAtoList> ListarAtos(long? IdTipoAto = null, string IdUsuario = null)
        {
            var lista = from a in UnitOfWorkCar16New.Repositories.RepositoryAto.GetAll()
                where ((IdTipoAto == null) || (a.IdTipoAto == IdTipoAto)) && ((IdUsuario == null) || (a.IdUsuarioCadastro == IdUsuario))
                join t in UnitOfWorkCar16New.Repositories.GenericRepository<TipoAto>().GetAll() on a.IdTipoAto equals t.Id
                orderby a.NumMatricula ascending, a.NumSequencia ascending
                select new DtoAtoList {
                    Id = a.Id,
                    Ativo = a.Ativo,
                    Bloqueado = a.Bloqueado,
                    Codigo = GetCodigoAto(a.IdTipoAto, a.NumMatricula, a.NumSequencia.ToString()),
                    DataAlteracao = a.DataAlteracao,
                    DataCadastro = a.DataCadastro,
                    DescricaoTipoAto = t.Descricao,
                    IdContaAcessoSistema = a.IdContaAcessoSistema,
                    IdPrenotacao = a.IdPrenotacao,
                    IdTipoAto = a.IdTipoAto,
                    IdUsuarioAlteracao = a.IdUsuarioAlteracao,
                    IdUsuarioCadastro = a.IdUsuarioCadastro,
                    NomeArquivo = a.NomeArquivo,
                    NumMatricula = a.NumMatricula,
                    NumSequencia = a.NumSequencia,
                    Observacao = a.Observacao
                };

            return lista;
        }

        private string GetCodigoAto(long IdTipoAto, string NumMatricula, string NumSequencia)
        {
            string CodTmp = string.Empty;

            switch (IdTipoAto)
            {
                case 1:
                    CodTmp = "AV";
                    break;
                case 2:
                    CodTmp = "R";
                    break;
                case 3:
                    CodTmp = "R";
                    break;
                default:
                    break;
            }

            CodTmp += "-" + NumSequencia + "/" + NumMatricula;
            return CodTmp;
        }
    }
}
