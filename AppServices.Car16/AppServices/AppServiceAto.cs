﻿using AppServices.Car16.AppServices.Base;
using AppServices.Car16.Interfaces;
using AutoMapper;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Car16.AppServices
{
    public class AppServiceAto : AppServiceCar16<DtoAto, Ato>, IAppServiceAto
    {
        public AppServiceAto(IUnitOfWorkCar16 unitOfWorkCar16) : base(unitOfWorkCar16)
        {
        }
        /// <summary>
        /// Verifica se já existe ato cadastrado
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool ExisteAtoCadastrado(Ato modelo)
        {
            //Busca no banco se existe algum ato para aquela Ato
            int quantidadeAtos = this.DomainServices.GenericDomainService<Ato>()
                .GetWhere(m => m.NumMatricula == modelo.NumMatricula)
                .Count();
            //Se ato > 1, então existe o ato inicial
            return quantidadeAtos > 0;
        }

    }
}
