using System;
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

        /// <summary>
        /// Verifica se já existe ato cadastrado
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool ExisteAtoCadastrado(long numeroMatricula)
        {
            //Busca no banco se existe algum ato para aquela Ato

            int quantidadeAtos = this.DomainServices.GenericDomainService<Ato>()
                .GetWhere(m => m.NumMatricula == numeroMatricula.ToString())
                .Select(p => p.Id)
                .Count();
            //Se ato > 1, então existe o ato inicial
            return quantidadeAtos > 0;
        }

        /// <summary>
        /// Pega o numero da sequencia do ultimo ato, se NULL então é o primeiro ATO (N.° 1)
        /// </summary>
        /// <param name="modelo">Ato</param>
        /// <returns>Ultimo numero da sequencia ou NULL</returns>
        public long? GetNumSequenciaAto(long numeroMatricula)
        {
            long? numSequencia =  (long?)this.DomainServices.GenericDomainService<Ato>()
                .GetWhere(m => m.NumMatricula == numeroMatricula.ToString())
                .Max(s => s.NumSequencia);
            return numSequencia;
        }

        public IEnumerable<DtoAtoList> ListarAtos(long? IdTipoAto = null, string IdUsuario = null)
        {
            List<DtoAtoList> lista = new List<DtoAtoList>();

            var listaAtos =
                from a in UnitOfWorkCar16New.Repositories.RepositoryAto.GetAll()
                where ((IdTipoAto == null) || (a.IdTipoAto == IdTipoAto)) && ((IdUsuario == null) || (a.IdUsuarioCadastro == IdUsuario))
                join t in UnitOfWorkCar16New.Repositories.GenericRepository<TipoAto>().GetAll() on a.IdTipoAto equals t.Id
                orderby a.NumMatricula ascending, a.NumSequencia ascending 
                select new {
                    a,
                    t.Descricao
                };

            foreach (var item in listaAtos.ToList())
            {
                lista.Add(new DtoAtoList
                {
                    Id = item.a.Id,
                    Ativo = item.a.Ativo,
                    Bloqueado = item.a.Bloqueado,
                    Codigo = GetCodigoAto(item.a.IdTipoAto, item.a.NumMatricula, item.a.NumSequencia.ToString()),
                    DataAlteracao = item.a.DataAlteracao,
                    DataCadastro = item.a.DataCadastro,
                    DescricaoTipoAto = item.Descricao,
                    IdContaAcessoSistema = item.a.IdContaAcessoSistema,
                    IdPrenotacao = item.a.IdPrenotacao,
                    IdTipoAto = item.a.IdTipoAto,
                    IdUsuarioAlteracao = item.a.IdUsuarioAlteracao,
                    IdUsuarioCadastro = item.a.IdUsuarioCadastro,
                    NomeArquivo = item.a.NomeArquivo,
                    NumMatricula = item.a.NumMatricula,
                    NumSequencia = item.a.NumSequencia,
                    Observacao = item.a.Observacao
                });
            }

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
