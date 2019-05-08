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

namespace AppServices.Car16.AppServices
{
    public class AppServiceAto : AppServiceCar16New<DtoAto, Ato>, IAppServiceAto
    {
        public AppServiceAto(IUnitOfWorkDataBaseCar16New unitOfWorkCar16) : base(unitOfWorkCar16)
        {
        }
        /// <summary>
        /// Verifica se já existe ato cadastrado
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool ExisteAtoCadastrado(long numeroMatricula)
        {
            //Busca no banco se existe algum ato para aquela Ato

            int quantidadeAtos = this.UnitOfWorkCar16New.Repositories.GenericRepository<Ato>()
                .GetWhere(n => n.NumMatricula == numeroMatricula.ToString())
                .Select(i => i.Id)
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
    }
}
