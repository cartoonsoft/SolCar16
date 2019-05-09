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
            //Se ato > 1, então existe o ato inicial
            return this.UnitOfWorkCar16New.Repositories.RepositoryAto.ExisteAtoCadastrado(numeroMatricula);
        }

        /// <summary>
        /// Pega o numero da sequencia do ultimo ato, se NULL então é o primeiro ATO (N.° 1)
        /// </summary>
        /// <param name="modelo">Ato</param>
        /// <returns>Ultimo numero da sequencia ou NULL</returns>
        public long? GetNumSequenciaAto(long numeroMatricula)
        {
            return this.UnitOfWorkCar16New.Repositories.RepositoryAto.GetNumSequenciaAto(numeroMatricula);
        }
    }
}
